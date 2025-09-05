using System;
using UnityEngine;

public class RotatingBonus : MonoBehaviour
{
    private void FixedUpdate()
    {
        Debug.Log("been here");
        this.transform.parent.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 500f);
    }
}
