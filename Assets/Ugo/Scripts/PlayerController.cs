using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IInputInitialize
{
    InputAction _aim;
    InputAction _shoot;
    [SerializeField] private GameObject _hook;
    public GameObject Hook
    {
        get => _hook;
    }

    private float _playerSpeed;
    private HookController _hookController;
    private Vector2 a;
    private float h = 0.0f;
    private Rigidbody2D _rbHook;
    private Rigidbody2D _rb;

    #region LocalMultiplayer
    public float life = 100f;

    //Player Index (Used for Local Multiplayer)
    [SerializeField] private int _playerIndex;
    public int PlayerIndex => _playerIndex;

    public void Initialize(PlayerInput playerInput)
    {
        _aim = playerInput.actions["Aim"];
        _shoot = playerInput.actions["Shoot"];
        if (_hookController != null)
        {
            _hookController.Initialize(_aim, _shoot);
        }
    }
    
    #endregion
    
    private void Start()
    {
        _rbHook = _hook?.GetComponent<Rigidbody2D>();
        _rb = this.GetComponent<Rigidbody2D>();

        _hookController = _hook?.GetComponent<HookController>();
        if (_hookController != null && _aim != null)
        {
            _hookController.Initialize(_aim, _shoot);
        }
    }

    private void FixedUpdate()
    {
        if (_aim == null) return;
        #region Aim

        a = _aim.ReadValue<Vector2>();
        if (a != Vector2.zero && _rbHook.linearVelocity == Vector2.zero)
        {
            h = (Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg) - 90;

            _hook.transform.rotation = Quaternion.Euler(0, 0, h);

        }

        #endregion

        #region Move

        if (Vector2.Distance(_rbHook.position, this.transform.position) > 0.2f && _rbHook.linearVelocity == Vector2.zero)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, _rbHook.position, _playerSpeed / 100f);
        }

        #endregion
    }
    public void OnDie()
    {
        Debug.Log("Player has been killed");
        Destroy(_hook);
        Destroy(gameObject);
        
    }
}
