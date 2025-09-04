using System;
using System.Collections;
using UnityEngine;

public class SquidController : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField]  private float _speed;
    [SerializeField] private float _damage;
    private GameObject target;
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SquidLife());
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, _speed *  Time.fixedDeltaTime);
    }

    private IEnumerator SquidLife()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.TakeDamage(_damage);
            Destroy(this.gameObject);
        }
    }


}
