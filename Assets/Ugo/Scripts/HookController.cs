using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HookController : MonoBehaviour
{ 
    private InputAction _shoot;
    private InputAction _aim;
    [SerializeField] private PlayerController _player;
    public float _hookSpeed;
    private Rigidbody2D _rb;
    private Rigidbody2D _rbPlayer;
    private float _playerWidth;
    private Collider2D _collider;
    private List<Collider2D> overlappingColliders = new List<Collider2D>();
    private bool CanShoot = false;
    private int _playerIndex;

    private bool _bWillBounce = false;
    private Vector2 _velocityOnShoot;
    public void WillBounce() => _bWillBounce = true;

    public int PlayerIndex
    {
        get => _playerIndex;
        set => _playerIndex = value;
    }


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rbPlayer = _player.gameObject.GetComponent<Rigidbody2D>();
        _playerWidth = _player.gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        _collider = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        #region Shoot

        if (_shoot is null || _aim is null)
        {
            Debug.Log("Not binded yet");
            return;
        }
        if (_rb.linearVelocity != Vector2.zero)
        {
            //Rotates towards Motion
            Vector2 a = _rb.linearVelocity.normalized;
            float h = (Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg) - 90;
            transform.rotation = Quaternion.Euler(0, 0, h);
        }
        
        if (_shoot.IsPressed() && Vector2.Distance(_player.transform.position, this.transform.position) < 1f)
        {
            CanShoot = true;
            foreach (Collider2D collider in overlappingColliders)
            {
                if (collider.gameObject.layer == LayerMask.NameToLayer("Environnement"))
                {
                    CanShoot =  false;
                }
            }
            if (CanShoot)
            {
                this.transform.parent = null;
                _velocityOnShoot = _aim.ReadValue<Vector2>() * (_hookSpeed * Time.fixedDeltaTime);
                _rb.linearVelocity = _velocityOnShoot;
                _collider.isTrigger = false;
                SoundManager.Instance.UseSound(4); //HarpoonShot
            }
        }

        #endregion
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            if (!_bWillBounce)
            {
                _rb.linearVelocity = Vector2.zero;
            }
            else
            {
                _rb.linearVelocity = Vector2.Reflect(_velocityOnShoot, other.contacts[0].normal);
                _player.SetBounceTarget(transform.position);
                _bWillBounce = false;
            }
        }
    }

    public void Initialize(InputAction aimInput, InputAction hookInput)
    {
        Debug.Log("Initializing hook");
        _aim = aimInput;
        _shoot = hookInput;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hook triggered");
            if (other.gameObject.GetComponent<PlayerController>().PlayerIndex == _playerIndex)
            {
                other.gameObject.GetComponent<PlayerController>().ResetHook();
            }
        }
        if(!overlappingColliders.Contains(other)) {
            overlappingColliders.Add(other);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if(overlappingColliders.Contains(other)) {
            overlappingColliders.Remove(other);
        }
    }
}
