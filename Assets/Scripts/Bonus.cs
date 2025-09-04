using UnityEngine;

public class Bonus : MonoBehaviour
{
    public string bonusName;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null) {
            if (collision.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerBonus>().OnBonusCollected(bonusName);
                Destroy(gameObject);
            }
        }
    }
}
