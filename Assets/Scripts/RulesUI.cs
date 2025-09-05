using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RulesUI : MonoBehaviour
{
    private InputAction _scrollInput;
    [SerializeField] private RectTransform _rulesAsset;
    [SerializeField] private float _maxYScroll;
    [SerializeField] private float _minYScroll;
    [SerializeField] private float _scrollSpeed;

    public void Open(PlayerInput playerInput)
    {
        _scrollInput = playerInput.actions["MoveSelection"];
    }

    public void Close(PlayerInput playerInput)
    {
        _scrollInput = playerInput.actions["MoveSelection"];
    }
/*
    public void Update()
    {
        if (_scrollInput == null) return;
        Vector2 move = _scrollInput.ReadValue<Vector2>();
        if (Mathf.Abs(move.y) < 0.01f) return;
        _rulesAsset.position = new Vector3(_rulesAsset.position.x, Mathf.Clamp(_rulesAsset.position.y + Mathf.Sign(move.y) * _scrollSpeed * -1, _minYScroll, _maxYScroll), _rulesAsset.position.z);
    }*/
}
