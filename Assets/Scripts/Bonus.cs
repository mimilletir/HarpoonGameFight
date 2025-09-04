using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [HideInInspector] public List<GameObject> bonusList;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null) {
            if (collision.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerBonus>().OnBonusCollected(bonusList[Random.Range(0, bonusList.Count)]);
                Destroy(gameObject);
            }
        }
    }
}
