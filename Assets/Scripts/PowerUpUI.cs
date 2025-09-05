using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
struct PowerUpIcons
{
    public PlayerBonus.BonusEnum Bonus;
    public Sprite Icon;
}
public class PowerUpUI : MonoBehaviour
{
    [SerializeField] private Image _powerUpImage;
    [SerializeField] private PowerUpIcons[] _powerUpIcons;

    private void Awake()
    {
        UpdateUI(PlayerBonus.BonusEnum.None);
    }

    public void UpdateUI(PlayerBonus.BonusEnum bonus)
    {
        _powerUpImage.enabled = true;
        if (bonus == PlayerBonus.BonusEnum.None)
        {
            _powerUpImage.enabled = false;
            return;
        }
        _powerUpImage.sprite = GetIcon(bonus);
    }

    private Sprite GetIcon(PlayerBonus.BonusEnum bonus)
    {
        foreach (PowerUpIcons powerUpIcon in _powerUpIcons)
        {
            if (powerUpIcon.Bonus == bonus)
            {
                return powerUpIcon.Icon;
            }
        }
        return _powerUpIcons[0].Icon;
    }
}
