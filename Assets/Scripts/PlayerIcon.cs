using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerIcon : MonoBehaviour, IInputInitialize
{
    [SerializeField] private int _playerIndex;
    public int PlayerIndex => _playerIndex;

    [SerializeField] private UnityEvent<PlayerInput> _onPlayerConnected;

    public void Initialize(PlayerInput playerInput)
    {
        GetComponent<Image>().color = new Color(1, 1, 1, 1);
        _onPlayerConnected?.Invoke(playerInput);
    }
}
