using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBonus : MonoBehaviour
{
    private InputAction _throwItem;
    public bool haveBonus = false;
    public GameObject bonusName;

    public void FixedUpdate()
    {
        if (_throwItem is null)
        {
            Debug.Log("Not binded yet");
            return;
        }

        if (_throwItem.IsPressed() && haveBonus)
        {
            // Do something
            haveBonus = false;
        }
    }

    public void OnBonusCollected(GameObject bonus)
    {
        haveBonus = true;
        bonusName = bonus;
    }

    public void OnBonusUsed()
    {
        if (!haveBonus) return;

        haveBonus = false;
    }

    public void Initialize(InputAction throwItem)
    {
        Debug.Log("Initializing throwItem");
        _throwItem = throwItem;
    }
}
