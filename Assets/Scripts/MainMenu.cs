using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int _maxPlayerNumber = 2;
    [SerializeField] private GameObject _startText;
    private int currJoinedPlayers = 0;
    private List<InputAction> _startInputs = new List<InputAction>();

    public void OnPlayerJoining(PlayerInput playerInput)
    {
        InputAction startInput = playerInput.actions["Interact"];
        if (startInput != null)
        {
            startInput.performed += TryStartGame;
            playerInput.currentActionMap = startInput.actionMap;
            _startInputs.Add(startInput);
            Debug.Log("Has Binded");
        }
        currJoinedPlayers++;
        Debug.Log("Loading... OnPlayerJoining" + currJoinedPlayers);
        if (currJoinedPlayers == _maxPlayerNumber)
        {
            _startText.SetActive(true);
        }
    }

    private void TryStartGame(InputAction.CallbackContext callbackContext)
    {
        foreach (InputAction startInput in _startInputs)
        {
            startInput.performed -= TryStartGame;
        }
        if (currJoinedPlayers >= _maxPlayerNumber)
        {
            Debug.Log("Loading... Starting Game aaaa");
            GameManager.Instance.StartGame();
        }
    }
}
