using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PauseMenuInteractions
{
    None,
    ResumeGame,
    MainMenu,
    DisableScreenShake,
    Rules,
}
public class PauseMenu : MonoBehaviour
{
    private PlayerInput _playerInput;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private RulesUI _rules;
    private int _selectedButton = 0;
    [SerializeField] private float _yThreshold;
    [SerializeField] private OptionButton[] _buttons;
    
    private void Start()
    {
        GameManager.Instance.PauseMenu = this;
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
        interactAction.started += OnInteract;
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
        interactAction.started -= OnInteract;
        moveSelection.started -= OnMoveSelection;
        closeMenu.performed -= UnpauseActionPressed;
        _pauseMenu.SetActive(false);
        _rules.gameObject.SetActive(false);
        _rules.Close(_playerInput);
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
        if (_rules.gameObject.activeSelf)
        {
            _rules.gameObject.SetActive(false);
            return;
        }
        PauseMenuInteractions interaction = _buttons[_selectedButton].OnInteract();
        switch (interaction)
        {
            case PauseMenuInteractions.None:
                break;
            case PauseMenuInteractions.ResumeGame:
                UnpauseGame();
                break;
            case PauseMenuInteractions.MainMenu:
                UnpauseGame();
                GameManager.Instance.GoBackToMainMenu();
                return;
            case PauseMenuInteractions.Rules:
                _rules.gameObject.SetActive(true);
                _rules.Open(_playerInput);
                break;
            case PauseMenuInteractions.DisableScreenShake:
                GameSettings settings = GameManager.Instance.Settings;
                settings.DisableCameraShaking = !settings.DisableCameraShaking;
                GameManager.Instance.Settings = settings;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
