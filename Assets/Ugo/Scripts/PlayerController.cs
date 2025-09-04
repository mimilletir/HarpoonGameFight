using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IInputInitialize
{
    InputAction _aim;
    InputAction _shoot;
    [SerializeField] private GameObject _hook;
    [SerializeField] private Slider _healthBar;
    [SerializeField] Text _hpText;
    [SerializeField] float _playerSpeed;
    [SerializeField] private float _maxLife;
    
    private float life;
    private HookController _hookController;
    private Vector2 a;
    private float h = 0.0f;
    private Rigidbody2D _rbHook;
    private Rigidbody2D _rb;

    #region LocalMultiplayer

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
        
        life = _maxLife;
        TakeDamage(0);
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

        if (Vector2.Distance(_rbHook.position, this.transform.position) > 0.5f && _rbHook.linearVelocity == Vector2.zero)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, _rbHook.position, _playerSpeed / 100f);
        }

        #endregion
    }

    public void TakeDamage(float damage)
    {
        life -= damage;
        _healthBar.value = life / _maxLife;
        _hpText.text = life.ToString();
        Debug.Log(life);
        if (life <= 0)
        {
            OnDie();
        }
    }
    
    private void OnDie()
    {
        Debug.Log("Player has been killed");
        Destroy(_hook);
        Destroy(gameObject);
        
    }
}
