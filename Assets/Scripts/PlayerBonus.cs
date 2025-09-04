using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBonus : MonoBehaviour
{
    private InputAction _throwItem;
    [SerializeField] private GameObject _bouncingBall;
    [SerializeField] private GameObject _squid;
    private PlayerController _playerController;

    public bool haveBonus = false;
    public BonusEnum _bonus;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    public void FixedUpdate()
    {
        if (_throwItem is null)
        {
            Debug.Log("Not binded yet");
            return;
        }

        if (_throwItem.IsPressed() && haveBonus)
        {
            switch (_bonus)
            {
                case BonusEnum.Sun:
                    break;
                case BonusEnum.BouncingBall:
                    GameObject BouncingBall = Instantiate(_bouncingBall, transform.position, Quaternion.identity);
                    break;
                case BonusEnum.SpeedBoost:
                    break;
                case BonusEnum.HealthUp:
                    _playerController.TakeDamage(-25f);
                    break;
                case BonusEnum.HarpoonRebonce:
                    break;
                case BonusEnum.SpeedDeBoost:
                    break;
                case BonusEnum.Squid:
                    GameObject Squid = Instantiate(_squid, transform.position, Quaternion.identity);
                    break;
                case BonusEnum.SwapPlace:
                    break;
            }

            haveBonus = false;
        }
    }

    public void OnBonusCollected(BonusEnum bonus)
    {
        haveBonus = true;
        _bonus = bonus;
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

    public enum BonusEnum
    {
        Sun,
        BouncingBall,
        SpeedBoost,
        HealthUp,
        HarpoonRebonce,
        SpeedDeBoost,
        Squid,
        SwapPlace
    };
}
