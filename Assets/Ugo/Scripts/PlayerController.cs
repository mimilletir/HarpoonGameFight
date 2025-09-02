using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputActionReference _aim;
    [SerializeField] InputActionReference _shoot;
    [SerializeField] private GameObject _hook;
    [SerializeField] private float _hookSpeed;
    private Vector2 a;
    private float h = 0.0f;
    private Rigidbody2D _rbHook;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rbHook =  _hook?.GetComponent<Rigidbody2D>();
        _rb = _rbHook.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        #region Aim
        a = _aim.action.ReadValue<Vector2>();
        if (a != Vector2.zero)
        {
            h = (Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg) - 90;
            
            _hook.transform.rotation = Quaternion.Euler(0, 0, h);
            
        }
        #endregion

        #region Shoot

        if (_shoot.action.IsPressed())
        {
            Debug.Log("Been here");
            _rbHook.AddForce(_hookSpeed * a.normalized, ForceMode2D.Force);
            //_rbHook.linearVelocity = _hook.transform.forward * _hookSpeed;
        }

        #endregion
        
    }

    public void MoveTowards(Vector2 direction)
    {
        this._rb.MovePosition(this._rb.position + direction);
    }
}
