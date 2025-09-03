using UnityEngine;

public class BouncingBall : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    private Rigidbody2D _rb;
    private Vector3 lastVelocity;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(new Vector2(9.8f * _speed, 9.8f * _speed));
    }

    void Update()
    {
        lastVelocity = _rb.linearVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            Destroy(gameObject);

        var direction = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        _rb.linearVelocity = direction * _speed;
    }
}
