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
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.life -= _damage;
            if (playerController.life <= 0)
            {
                playerController.OnDie();
            }
        }
    }


}
