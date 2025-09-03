using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction _aim;
    [SerializeField] private GameObject _hook;
    [SerializeField] private float _playerSpeed;
    private HookController _hookController;
    private Vector2 a;
    private float h = 0.0f;
    private Rigidbody2D _rbHook;
    private Rigidbody2D _rb;

    #region LocalMultiplayer

    //Player Index (Used for Local Multiplayer)
    [SerializeField] private int _playerIndex;
    public int PlayerIndex => _playerIndex;

    public void Initialize(InputAction aimInput, InputAction hookInput)
    {
        _aim = aimInput;
        if (_hookController != null)
        {
            _hookController.Initialize(aimInput, hookInput);
        }
        else
        {
            Debug.LogError("No HookController found");
        }
    }
    
    #endregion
    
    private void Start()
    {
        _rbHook = _hook?.GetComponent<Rigidbody2D>();
        _rb = this.GetComponent<Rigidbody2D>();

        _hookController = _hook?.GetComponent<HookController>();
    }

    private void FixedUpdate()
    {
        #region Aim

        a = _aim.ReadValue<Vector2>();
        if (a != Vector2.zero)
        {
            h = (Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg) - 90;

            _hook.transform.rotation = Quaternion.Euler(0, 0, h);

        }

        #endregion

        #region Move

        if (Vector2.Distance(_rbHook.position, this.transform.position) > 0.1f &&
            _rbHook.linearVelocity == Vector2.zero)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, _rbHook.position, _playerSpeed / 100f);
        }

        #endregion
    }
}
