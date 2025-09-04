using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class OptionButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string _content;
    [SerializeField] private PauseMenuInteractions _pauseMenuInteraction = PauseMenuInteractions.None;

    private void Awake()
    {
        _text.text = _content;
    }

    public PauseMenuInteractions OnInteract()
    {
        Debug.Log("Interact with button : " + _pauseMenuInteraction);
        return _pauseMenuInteraction;
    }

    public void UpdateSelection(bool bIsSelected)
    {
        _text.text = bIsSelected ? "> " + _content + " <" : _content;
    }
}
