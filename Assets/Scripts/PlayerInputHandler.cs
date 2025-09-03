using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    
    private PlayerInput _playerInput;
    public void Awake()
    {
        DontDestroyOnLoad(this);
        _playerInput = GetComponent<PlayerInput>();
        BindPlayerObjectInScene();
    }

    //Find and bind to Player in Scene, only call on beginning of Scene
    private void BindPlayerObjectInScene()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerObject in playerObjects)
        {
            PlayerController playerControllerScript = playerObject.GetComponent<PlayerController>();
            if (playerControllerScript != null && playerControllerScript.PlayerIndex == _playerInput.playerIndex)
            {
                Debug.Log(playerObject.name + "" + _playerInput.playerIndex);
                InputAction aimInput = _playerInput.actions["Aim"];
                InputAction shootInput = _playerInput.actions["Shoot"];
                playerControllerScript.Initialize(aimInput, shootInput);
            }
        }
    }
}
