using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int _maxPlayerNumber = 2;
    [SerializeField] private GameObject _startText;
    private int currJoinedPlayers = 0;

    public void OnPlayerJoining(PlayerInput playerInput)
    {
        InputAction startInput = playerInput.actions["Join"];
        if (startInput != null)
        {
            startInput.performed += TryStartGame;
            playerInput.currentActionMap = startInput.actionMap;
            Debug.Log("Has Binded");
        }
        currJoinedPlayers++;
        if (currJoinedPlayers == _maxPlayerNumber)
        {
            _startText.SetActive(true);
        }
    }

    private void TryStartGame(InputAction.CallbackContext callbackContext)
    {
        if (currJoinedPlayers >= _maxPlayerNumber)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
