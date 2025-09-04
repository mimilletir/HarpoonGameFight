using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBonus : MonoBehaviour
{
    private InputAction _throwItem;
    [SerializeField] private GameObject _bouncingBall;
    [SerializeField] private GameObject _squid;
    [SerializeField] private PowerUpUI _powerUpUI;
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
                    Squid.GetComponent<SquidController>().playerIndexMaster = _playerController.PlayerIndex;
                    break;
                case BonusEnum.SwapPlace:
                    PlayerController otherPlayer = GameManager.Instance.GetPlayerFromIndex(_playerController.PlayerIndex == 0 ? 1 : 0);
                    otherPlayer.ResetHook();
                    _playerController.ResetHook();
                    (otherPlayer.transform.position, transform.position) = (transform.position, otherPlayer.transform.position);
                    break;
            }

            haveBonus = false;
            _powerUpUI.UpdateUI(BonusEnum.None);
        }
    }

    public void OnBonusCollected(BonusEnum bonus)
    {
        haveBonus = true;
        _bonus = bonus;
        _powerUpUI.UpdateUI(bonus);
    }

    public void Initialize(InputAction throwItem)
    {
        Debug.Log("Initializing throwItem");
        _throwItem = throwItem;
    }

    public enum BonusEnum
    {
        None = 0,
        BouncingBall,
        SpeedBoost,
        HealthUp,
        HarpoonRebonce,
        SpeedDeBoost,
        Squid,
        SwapPlace
    };
}
