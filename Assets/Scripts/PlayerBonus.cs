using UnityEngine;

public class PlayerBonus : MonoBehaviour
{
    public bool haveBonus = false;
    public string bonusName;

    public void OnBonusCollected(string bonus)
    {
        haveBonus = true;
        bonusName = bonus;
    }

    public void OnBonusUsed()
    {
        if (!haveBonus) return;

        haveBonus = false;
    }
}
