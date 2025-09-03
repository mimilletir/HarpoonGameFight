using UnityEngine;

public class BouncingBall : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private int _maxNBounce;
    private int _nBounce = 0;
    private Rigidbody2D _rb;
    private Vector3 lastVelocity;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(new Vector2(_speed, _speed));
    }

    void Update()
    {
        lastVelocity = _rb.linearVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_nBounce > _maxNBounce)
            Destroy(gameObject);

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().OnTakeDamage(_damage);
            Destroy(gameObject);
        }

        _nBounce++;

        var direction = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        _rb.linearVelocity = direction * _speed;
    }
}
