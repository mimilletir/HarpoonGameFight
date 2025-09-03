using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInputHandler : MonoBehaviour
{
    
    private PlayerInput _playerInput;
    public void Awake()
    {
        DontDestroyOnLoad(this);
        _playerInput = GetComponent<PlayerInput>();
        BindPlayerObjectInScene();
        SceneManager.sceneLoaded += OnSceneChanged;
    }

    private void OnSceneChanged(Scene arg0, LoadSceneMode arg1)
    {
        BindPlayerObjectInScene();
        _playerInput.SwitchCurrentActionMap(_playerInput.defaultActionMap);
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
