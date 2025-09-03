using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInputHandler : MonoBehaviour
{
    
    private PlayerInput _playerInput;
    private GameManager _gameManager;
    private InputAction _interactInput;
    private InputActionMap _menuInputMap;

    public void Awake()
    {
        DontDestroyOnLoad(this);
        _playerInput = GetComponent<PlayerInput>();
        BindPlayerObjectInScene();
        _interactInput = _playerInput.actions["Interact"];
        _menuInputMap = _interactInput.actionMap;
    }

    public void Start()
    {
        _gameManager = GameManager.Instance;
        if (_gameManager != null)
        {
            _gameManager.OnGamePhaseChanged += OnGameChanged;
        }
    }

    private void OnGameChanged(GamePhase gamePhase)
    {
        _interactInput.performed -= RestartGame;
        switch (gamePhase)
        {
            case GamePhase.MainMenu:
                BindPlayerObjectInScene();
                _playerInput.SwitchCurrentActionMap(_menuInputMap.name);
                break;
            case GamePhase.Gameplay:
                BindPlayerObjectInScene();
                _playerInput.SwitchCurrentActionMap(_playerInput.defaultActionMap);
                break;
            case GamePhase.VictoryScreen:
                _playerInput.SwitchCurrentActionMap(_menuInputMap.name);
                _interactInput.performed += RestartGame;
                break;
            default:
                BindPlayerObjectInScene();
                break;
        }
    }

    private void RestartGame(InputAction.CallbackContext obj)
    {
        _gameManager.GoBackToMainMenu();
    }

    //Find and bind to Player in Scene, only call on beginning of Scene
    private void BindPlayerObjectInScene()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerObject in playerObjects)
        {
            IInputInitialize inputInitializer = playerObject.GetComponent<IInputInitialize>();
            if (inputInitializer != null && inputInitializer.PlayerIndex == _playerInput.playerIndex)
            {
                inputInitializer.Initialize(_playerInput);
            }
        }
    }
}
