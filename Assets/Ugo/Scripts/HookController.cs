using System;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class HookController : MonoBehaviour
{
    [SerializeField] private InputActionReference _shoot;
    [SerializeField] private InputActionReference _aim;
    [SerializeField] private PlayerController _player;
    public float _hookSpeed;
    private Rigidbody2D _rb;
    private Rigidbody2D _rbPlayer;
    private float _playerWidth;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rbPlayer = _player.gameObject.GetComponent<Rigidbody2D>();
        _playerWidth = _player.gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }

    private void FixedUpdate()
    {
        #region Shoot

        if (_shoot.action.IsPressed() && Vector2.Distance(_player.transform.position, this.transform.position) < _playerWidth)
        {
            _rb.AddForce(_hookSpeed * _aim.action.ReadValue<Vector2>().normalized, ForceMode2D.Force);
        }

        #endregion
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            _rb.linearVelocity = Vector2.zero;
        }
    }
}
