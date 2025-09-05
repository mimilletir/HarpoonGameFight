using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class OptionButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string _content;
    [SerializeField] private PauseMenuInteractions _pauseMenuInteraction = PauseMenuInteractions.None;
    private string _value;

    private void Awake()
    {
        _value = _pauseMenuInteraction == PauseMenuInteractions.DisableScreenShake ? 
            (GameManager.Instance.Settings.DisableCameraShaking ? "Yes" : "No") : "";
        _text.text = _content + _value;
    }

    public PauseMenuInteractions OnInteract()
    {
        Debug.Log("Interact with button : " + _pauseMenuInteraction);
        if (_pauseMenuInteraction == PauseMenuInteractions.DisableScreenShake)
        {
            _value = GameManager.Instance.Settings.DisableCameraShaking ? "No" : "Yes";
            _text.text = _content + _value;
        }
        return _pauseMenuInteraction;
    }

    public void UpdateSelection(bool bIsSelected)
    {
        _text.text = bIsSelected ? "> " + _content + _value + " <" : _content + _value;
    }
}
