using System;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 0.5f;

    private void Awake()
    {
        Destroy(gameObject, _lifeTime);
    }
}
