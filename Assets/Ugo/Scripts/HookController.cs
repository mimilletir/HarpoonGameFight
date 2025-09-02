using System;
using UnityEditor.UI;
using UnityEngine;

public class HookController : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            _rb.linearVelocity = Vector2.zero;
            _player.MoveTowards(this.transform.position);
        }
    }
}
