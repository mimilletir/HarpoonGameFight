using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = System.Random;
using URandom = UnityEngine.Random;

public class PlayerController : MonoBehaviour, IInputInitialize
{
    InputAction _aim;
    InputAction _shoot;
    InputAction _throwItem;
    [SerializeField] private GameObject _hook;
    [SerializeField] private Slider _healthBar;
    [SerializeField] float _playerSpeed;
    [SerializeField] private float _maxLife;
    [SerializeField] private Camera _camera;

    public float MaxLife
    {
        get { return _maxLife; }
    }
    
    private float life;
    private HookController _hookController;
    private PlayerBonus _playerBonus;
    private Vector2 a;
    private float h = 0.0f;
    private Rigidbody2D _rbHook;
    private Rigidbody2D _rb;
    private Vector3 _offset;

    #region LocalMultiplayer

    //Player Index (Used for Local Multiplayer)
    [SerializeField] private int _playerIndex;


    public int PlayerIndex => _playerIndex;

    public void Initialize(PlayerInput playerInput)
    {
        _aim = playerInput.actions["Aim"];
        _shoot = playerInput.actions["Shoot"];
        _throwItem = playerInput.actions["ThrowItem"];
        if (_hookController != null)
        {
            _hookController.Initialize(_aim, _shoot);
        }
        if (_playerBonus != null)
        {
            _playerBonus.Initialize(_throwItem);
        }
    }
    
    #endregion
    
    private void Start()
    {
        _rbHook = _hook?.GetComponent<Rigidbody2D>();
        _rb = this.GetComponent<Rigidbody2D>();

        _hookController = _hook?.GetComponent<HookController>();
        _playerBonus = GetComponent<PlayerBonus>();
        
        
        life = _maxLife;
        TakeDamage(0);
        if (_hookController != null && _aim != null)
        {
            _hookController.Initialize(_aim, _shoot);
        }
        _hookController.PlayerIndex =  _playerIndex;
        ResetHook();
        if (_playerBonus != null)
        {
            _playerBonus.Initialize(_throwItem);
        }
    }

    private void FixedUpdate()
    {
        if (_aim == null) return;
        
        #region Aim

        a = _aim.ReadValue<Vector2>();
        if (a != Vector2.zero && _rbHook.linearVelocity == Vector2.zero && Vector2.Distance(_rbHook.position, this.transform.position) < 1f)
        {
            Debug.Log(transform.parent.name);
            h = (Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg) - 90;
            this.transform.rotation = Quaternion.Euler(0, 0, h);
            //_hook.transform.rotation = Quaternion.Euler(0, 0, h);

        }

        #endregion

        #region Move

        if (Vector2.Distance(_rbHook.position, this.transform.position) > 1f && _rbHook.linearVelocity == Vector2.zero
            && _rb.linearVelocity ==  Vector2.zero)
        {

            _rb.AddForce((_hook.transform.position - this.transform.position), ForceMode2D.Impulse);
            //this.transform.position = Vector2.MoveTowards(this.transform.position, _rbHook.position, _playerSpeed / 100f);
        }

        #endregion

        if (Vector2.Distance(_rbHook.position, this.transform.position) < 1f)
        {
            
        }
    }

    public void ResetHook()
    {
        Debug.Log("Reset Hook");
        _rb.linearVelocity = Vector2.zero;
        
        _hook.transform.SetParent(this.transform);
        _hook.transform.localPosition = new Vector3(0, 3.6f, 0);
        _hook.transform.localRotation = Quaternion.Euler(0, 0, 0);
        _hook.GetComponent<Collider2D>().isTrigger = true;
    }

    [SerializeField] private Image _shatteredGlass;

    public void TakeDamage(float damage)
    {
        if (damage > 0) // La fonction est aussi appelé lorsque on se soigne(valeur négative) donc on veut pas d'effet
        {
            StartCoroutine(ShatteredGlass());
            StartCoroutine(CameraShake());
        }
        
        life -= damage;
        _healthBar.value = life / _maxLife;
        if (life <= 0)
        {
            OnDie();
        }
    }

    private IEnumerator ShatteredGlass()
    {
        float t = 0f;

        while (t < 2f)
        {
            float a = Mathf.Lerp(1f, 0f, t / 2f);
            _shatteredGlass.color = new Color(1f, 1f, 1f, a);
            t += Time.deltaTime;
            yield return null;
        }

        _shatteredGlass.color = new Color(1f, 1f, 1f, 0f);
    }

    private IEnumerator CameraShake()
    {
        Vector3 startposition = _camera.transform.position;
        float t = 0f;

        while (t < 1f)
        {
            Vector3 shakeOffset = URandom.insideUnitSphere * 0.1f;
            _camera.transform.position = startposition + shakeOffset;
            t += Time.deltaTime;
            yield return null;
        }

        _camera.transform.position = startposition;
    }

    private void OnDie()
    {
        Debug.Log("Player has been killed");
        GameManager.Instance.OnGameWon(_playerIndex == 0 ? 1 : 0);
        Destroy(_hook);
        Destroy(gameObject);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            Rigidbody2D rbOther = other.gameObject.GetComponent<Rigidbody2D>();
            if (_rb.linearVelocity.magnitude > rbOther.linearVelocity.magnitude)
            {
                Debug.Log(_rb.linearVelocity.normalized.magnitude);
                player.TakeDamage(_rb.linearVelocity.magnitude /* player.MaxLife*/);
            } else if (_rb.linearVelocity.magnitude < rbOther.linearVelocity.magnitude)
            {
                Debug.Log("3");
                TakeDamage(rbOther.linearVelocity.magnitude);
            }
            else
            {
                Random rnd = new Random();
                switch (rnd.Next(0,2))
                {
                    case 0:
                        Debug.Log("0");
                        player.TakeDamage(_rb.linearVelocity.normalized.magnitude * player.MaxLife);
                        break;
                    case 1:
                        Debug.Log("1");
                        TakeDamage(rbOther.linearVelocity.magnitude * _maxLife);
                        break;
                }
            }
        }
    }
}
