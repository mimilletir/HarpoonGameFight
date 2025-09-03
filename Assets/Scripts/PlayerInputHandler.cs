using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private InputActionReference _aim;
    [SerializeField] private InputActionReference _shoot;
    private PlayerInput _playerInput;
    public void Awake()
    {
        DontDestroyOnLoad(this);
        _playerInput = GetComponent<PlayerInput>();
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
                playerControllerScript.Initialize(_aim,_shoot);
            }
        }
    }
}
