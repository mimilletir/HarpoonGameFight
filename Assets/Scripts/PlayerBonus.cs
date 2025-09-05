using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBonus : MonoBehaviour
{
    private InputAction _throwItem;
    [SerializeField] private GameObject _bouncingBall;
    [SerializeField] private GameObject _squid;
    [SerializeField] private PowerUpUI _powerUpUI;
    [SerializeField] private float _addedSpeedMultiplierValue = 0.2f;
    [SerializeField] private Vector2 _a;
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
            haveBonus = false;

            switch (_bonus)
            {
                case BonusEnum.BouncingBall:
                    GameObject BouncingBall = Instantiate(_bouncingBall, transform.position, _playerController.transform.rotation);
                    BouncingBall.GetComponent<BouncingBall>()._a =_playerController.GetComponent<PlayerController>().a;
                    BouncingBall.GetComponent<BouncingBall>().playerIndexMaster = _playerController.PlayerIndex;
                    break;
                case BonusEnum.SpeedBoost:
                    _playerController.AddSpeedMultiplier(_addedSpeedMultiplierValue);
                    break;
                case BonusEnum.HealthUp:
                    _playerController.TakeDamage(-25f);
                    break;
                case BonusEnum.HarpoonRebonce:
                    _playerController.NextHarpoonWillBounce();
                    break;
                case BonusEnum.SpeedDeBoost:
                    PlayerController otherPlayer = GameManager.Instance.GetPlayerFromIndex(_playerController.PlayerIndex == 0 ? 1 : 0);
                    otherPlayer.AddSpeedMultiplier(_addedSpeedMultiplierValue * -1.0f);
                    break;
                case BonusEnum.Squid:
                    GameObject Squid = Instantiate(_squid, transform.position, Quaternion.identity);
                    Squid.GetComponent<SquidController>().playerIndexMaster = _playerController.PlayerIndex;
                    break;
                /*case BonusEnum.SwapPlace:
                    PlayerController otherPlayer = GameManager.Instance.GetPlayerFromIndex(_playerController.PlayerIndex == 0 ? 1 : 0);
                    otherPlayer.ResetHook();
                    _playerController.ResetHook();
                    (otherPlayer.transform.position, transform.position) = (transform.position, otherPlayer.transform.position);
                    otherPlayer.ResetHook();
                    _playerController.ResetHook();
                    break;*/
            }

            _powerUpUI.UpdateUI(BonusEnum.None);
        }
    }

    public void OnBonusCollected(BonusEnum bonus)
    {
        haveBonus = true;
        _bonus = bonus;
        _powerUpUI.UpdateUI(bonus);
    }

    public void Initialize(InputAction throwItem, Vector2 a)
    {
        Debug.Log("Initializing throwItem");
        _throwItem = throwItem;
        _a = a;
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
        //SwapPlace
    };
}
