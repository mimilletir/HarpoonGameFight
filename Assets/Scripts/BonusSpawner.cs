using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bonusPrefab;
    [SerializeField] private List<GameObject> bonusList;
    [SerializeField] private List<Vector2> spawnPoint;
    [SerializeField] private float spawnInterval = 15;

    void Start()
    {
        InvokeRepeating(nameof(SpawnBonus), spawnInterval, spawnInterval);
    }

    private void SpawnBonus()
    {
        Vector2 spawnPos = spawnPoint[Random.Range(0, spawnPoint.Count)];

        GameObject newBonus = Instantiate(bonusPrefab, spawnPos, Quaternion.identity);
        newBonus.GetComponent<Bonus>().bonusList = bonusList;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach (Vector2 spawnPos in spawnPoint)
        {
            Gizmos.DrawWireSphere(spawnPos, 0.2f);
        }
    }
}
