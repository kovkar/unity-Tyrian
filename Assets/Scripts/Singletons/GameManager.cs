using System;
using System.Net.WebSockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private string[] levelNames;
    [SerializeField] private int[] scoreThresholds;

    [Header("Refferences")]
    [SerializeField] private GameObject playerHUD;
    [SerializeField] private GameObject bossHUD;

    public Bar playerHealthbar;
    public Bar bossHealthbar;
    /// <value><c>Bar</c> showing cooldown until power attack ready</value>
    public Bar powerCannonCooldownBar;
    /// <value><c>Bar</c> showing temperature of <c>longCannon</c></value>
    public Bar longCannonTemperatureBar;




    private Canvas HUD;
    private Timer timer;
    private const int BOSS_LEVEL = 4; 
    private int _level = 10;

    public void Awake()
    {
        // Check, if we do not have any instance yet.
        if (Instance == null)
        {
            // 'this' is the first instance created => save it.
            Instance = this;
        }
        else if (Instance != this)
        {
            // Destroy 'this' object as there exist another instance
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this);
        playerHealthbar = playerHUD.GetComponentInChildren<Bar>();
        timer = GetComponentInChildren<Timer>();
        HUD = GetComponentInChildren<Canvas>();
        HUD.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.N)) 
        {
            LoadNextLevel(); 
        }
    }

    public void StartGame(int level)
    {
        timer.StartTimer();
        HUD.gameObject.SetActive(true);
        LoadLevel(level);
    }

    public void EndGame(bool victory)
    {
        timer.StopTimer();
        if (victory)
        {
            SceneManager.LoadScene("Victory");
        }
        else
        {
            SceneManager.LoadScene("Loss");
        }
        Destroy(timer.gameObject);
        HUD.gameObject.SetActive(false);
    }

    private void LoadLevel(int level)
    {
        _level = level;
        Currencies.Instance.SetScoreThreshold(scoreThresholds[level-1]);
        SceneManager.LoadScene(levelNames[level - 1]);
        playerHUD?.SetActive(true);
        bossHUD?.SetActive(level == BOSS_LEVEL);
    }

    public void LoadNextLevel()
    {
        if (_level > levelNames.Length - 1)
        {
            Debug.LogWarning("No more levels!");
        }
        else
        {
            LoadLevel(_level + 1);
        }
    }

    public Timer getTimer() { return timer; }
}
