using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class OptionButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string _content;

    public UnityAction OnInteracted;

    private void Awake()
    {
        _text.text = _content;
    }

    public void OnInteract()
    {
        Debug.Log("OnInteract" + gameObject.name);
        OnInteracted?.Invoke();
    }

    public void UpdateSelection(bool bIsSelected)
    {
        _text.text = bIsSelected ? "> " + _content + " <" : _content;
    }
}
