using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PauseMenu : MonoBehaviour
{
    private PlayerInput _playerInput;
    [SerializeField] private GameObject _pauseMenu;
    private int _selectedButton = 0;
    [SerializeField] private float _yThreshold;
    [SerializeField] private OptionButton[] _buttons;
    [SerializeField] private OptionButton _resumeButton;
    
    private void Start()
    {
        GameManager.Instance.PauseMenu = this;
        _resumeButton.OnInteracted += UnpauseGame;
    }

    public void PauseGame(PlayerInput playerInput)
    {
        Debug.Log("PauseMenu Opened");
        _pauseMenu.SetActive(true);
        _playerInput = playerInput;
        InputAction interactAction = playerInput.actions["Interact"];
        InputAction moveSelection = playerInput.actions["MoveSelection"];
        InputAction closeMenu = playerInput.actions["CloseMenu"];
        playerInput.SwitchCurrentActionMap(interactAction.actionMap.name);
        interactAction.performed += OnInteract;
        moveSelection.started += OnMoveSelection;
        closeMenu.performed += UnpauseActionPressed;
        foreach (OptionButton optionButton in _buttons)
        {
            optionButton.UpdateSelection(false);
        }
        _selectedButton = 0;
        _buttons[_selectedButton].UpdateSelection(true);

    }

    public void UnpauseGame()
    {
        InputAction interactAction = _playerInput.actions["Interact"];
        InputAction moveSelection = _playerInput.actions["MoveSelection"];
        InputAction closeMenu = _playerInput.actions["CloseMenu"];
        interactAction.performed -= OnInteract;
        moveSelection.started -= OnMoveSelection;
        closeMenu.performed -= UnpauseActionPressed;
        _pauseMenu.SetActive(false);
        GameManager.Instance.PauseGame(_playerInput); //Unpause Game
    }

    public void UnpauseActionPressed(InputAction.CallbackContext obj)
    {
        UnpauseGame();
    }

    private void OnMoveSelection(InputAction.CallbackContext obj)
    {
        Vector2 move = obj.ReadValue<Vector2>();
        Debug.Log(move);
        if (move.y < _yThreshold)
        {
            _buttons[_selectedButton].UpdateSelection(false);
            _selectedButton++;
            if (_selectedButton > _buttons.Length - 1) _selectedButton = 0;
            _buttons[_selectedButton].UpdateSelection(true);
        }
        else if (move.y > _yThreshold)
        {
            _buttons[_selectedButton].UpdateSelection(false);
            _selectedButton--;
            if (_selectedButton < 0) _selectedButton = _buttons.Length - 1;
            _buttons[_selectedButton].UpdateSelection(true);
        }
    }

    private void OnInteract(InputAction.CallbackContext obj)
    {
        _buttons[_selectedButton].OnInteract();
    }
}
