using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum GamePhase
{
    MainMenu,
    Gameplay,
    VictoryScreen,
}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; set => _instance = value; }
    [Header("Game Scenes")]
    [SerializeField] private string _gameplaySceneName;
    [SerializeField] private string _mainMenuSceneName;
    [SerializeField] private string _victorySceneName;
    private GamePhase _currentGamePhase = GamePhase.MainMenu;

    private bool bIsLoadingScene = false;
    
    //Called when Scene is changed 
    public UnityAction<GamePhase> OnGamePhaseChanged;

    //Winning
    private int _wonPlayer = -1;
    public int WonPlayer => _wonPlayer;

    //PauseMenu
    private int _lastPausePlayer = -1;
    public PauseMenu PauseMenu { get; set; }
    
    
    public void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this);
    }

    public void OnGameWon(int wonPlayer)
    {
        _wonPlayer = wonPlayer;
        StartCoroutine(LoadPhase(_victorySceneName, GamePhase.VictoryScreen));
    }


    public void StartGame()
    {
        _wonPlayer = -1;
        StartCoroutine(LoadPhase(_gameplaySceneName, GamePhase.Gameplay));
    }

    public void GoBackToMainMenu()
    {
        StartCoroutine(LoadPhase(_mainMenuSceneName, GamePhase.MainMenu));
    }
    private IEnumerator LoadPhase(string sceneName, GamePhase gamePhase)
    {
        if (!bIsLoadingScene)
        {
            bIsLoadingScene = true;
            Debug.Log("Loading Game Phase: " + gamePhase);
            yield return SceneManager.LoadSceneAsync(sceneName);
            _currentGamePhase = gamePhase;
            OnGamePhaseChanged?.Invoke(gamePhase);
            bIsLoadingScene = false;
            /*if (gamePhase == GamePhase.Gameplay)
            {
                yield return new WaitForSeconds(3f);
                OnGameWon(0);
            }*/
        }
    }

    public void PauseGame(PlayerInput playerInput)
    {
        if (_currentGamePhase == GamePhase.Gameplay)
        {
            if (Time.timeScale == 0)
            {
                if (_lastPausePlayer != playerInput.playerIndex) return;
                //Unpause Game
                Time.timeScale = 1;
                playerInput.SwitchCurrentActionMap(playerInput.defaultActionMap);
            }
            else
            {
                Debug.Log("Paused Game");
                //Pause Game
                _lastPausePlayer = playerInput.playerIndex;
                PauseMenu.PauseGame(playerInput);
                Time.timeScale = 0;
            }
        }
    }
}
