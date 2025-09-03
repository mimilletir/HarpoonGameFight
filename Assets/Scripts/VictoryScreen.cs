using System;
using TMPro;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    private int _wonPlayerIndex = -1;
    private GameManager _gameManager;
    [SerializeField] private TextMeshProUGUI _victoryText;
    private void Start()
    {
        _gameManager = GameManager.Instance;
        _wonPlayerIndex = _gameManager != null ? -1 : _gameManager.WonPlayer;
        if (_wonPlayerIndex == -1) Debug.Log("wonPlayerIndex is -1");
        _victoryText.text = "Congratulations Player " + _wonPlayerIndex.ToString() + "/n You Won!";
    }
}
