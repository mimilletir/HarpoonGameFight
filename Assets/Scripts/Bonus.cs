using System;
using UnityEngine;
using static PlayerBonus;

public class Bonus : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null) {
            if (collision.CompareTag("Player"))
            {
                var values = Enum.GetValues(typeof(BonusEnum));
                int random = UnityEngine.Random.Range(0, values.Length);
                collision.gameObject.GetComponent<PlayerBonus>().OnBonusCollected((BonusEnum)values.GetValue(random));
            }
        }
    }
}
