using UnityEngine;

public class BouncingBall : MonoBehaviour
{
    [SerializeField] public float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private int _maxNBounce;
    private int _nBounce = 0;
    private Rigidbody2D _rb;
    private Vector3 lastVelocity;
    private bool stop = false;
    [HideInInspector] public int playerIndexMaster;
    [HideInInspector] public Vector2 _a;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!stop)
        {
            _rb.linearVelocity = _a * _speed;
            stop = true;
        }  

        lastVelocity = _rb.linearVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Environnement"))
            return;

        if (_nBounce > _maxNBounce)
            Destroy(gameObject);

        _nBounce++;

        var direction = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        _rb.linearVelocity = direction * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerController>().PlayerIndex == playerIndexMaster)
                return;

            collision.gameObject.GetComponent<PlayerController>().TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
