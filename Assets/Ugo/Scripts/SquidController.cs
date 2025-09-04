using System;
using System.Collections;
using UnityEngine;

public class SquidController : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField]  private float _speed;
    [SerializeField] private float _damage;
    [HideInInspector] public int playerIndexMaster;
    private GameObject target;

    private void Start()
    {
        StartCoroutine(SquidLife());
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
            {
                PlayerController playerController = g.GetComponent<PlayerController>();
                if (playerController is not null && playerController.PlayerIndex != playerIndexMaster)
                    target = g;
                
            }
        }

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
            if (other.gameObject.GetComponent<PlayerController>().PlayerIndex == playerIndexMaster)
                return;

            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.TakeDamage(_damage);
            Destroy(this.gameObject);
        }
    }


}
