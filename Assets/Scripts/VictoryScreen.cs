using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
    private int _wonPlayerIndex = -1;
    private GameManager _gameManager;
    [SerializeField] private TextMeshProUGUI _victoryText;
    [SerializeField] private Image _playerImage;
    [SerializeField] private Sprite[] _playerIcons;
    private void Start()
    {
        _gameManager = GameManager.Instance;
        _wonPlayerIndex = _gameManager == null ? -1 : _gameManager.WonPlayer;
        _victoryText.text = "Congratulations Player " + (_wonPlayerIndex == 0 ? "Yellow" : "Red") + "\n You Won!";
        _playerImage.sprite = _playerIcons[_wonPlayerIndex];
        SoundManager.Instance.UseSound(3); //Victory
    }
}
