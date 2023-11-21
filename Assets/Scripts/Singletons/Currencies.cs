using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;

/// <summary>
/// Class for storing and managing <c>Score</c> and <c>Credits</c>. (following Singleton pattern)
/// </summary>
public class Currencies : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI creditsText;

    private readonly int[] SCORE_COEFS = { 1, 5, 10 };

    private readonly int[] CREDITS_COEFS = { 1, 1, 2 };

    private const int CRASH = 0, HIT = 1, KILL = 2;

    /// <value>Attribute <c>Score</c> represents current score of a player.</value>
    private int Score = 0;

    /// <value>Attribute <c>Credits</c> represents current amount of player's credits.</value>
    private int Credits = 0;

    /// <value>Public static reference to this object. (Singleton)</value>
    public static Currencies Instance { get; private set; }

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
    }

    public void OnCollisionCurrencies(int healthTaken, bool crash, bool kill) 
    {
        int i = (crash) ? CRASH : (kill) ? KILL : HIT;
        int scoreIncrement =   SCORE_COEFS[i] * healthTaken;
        int creditsIncreemnt = CREDITS_COEFS[i] * healthTaken;
        Score += scoreIncrement;
        Credits += creditsIncreemnt;

        string collisionType = (crash) ? "CRASH" : (kill) ? "KILL" : "HIT";
        incrementScore(scoreIncrement);
        incrementCredits(creditsIncreemnt);
        // Debug.Log(collisionType + " - score: " + Score + "(+" + scoreIncrement + "), " + "credits: " + Credits + "(+" + creditsIncreemnt + ")");
    }

    public void incrementScore(float value) 
    {
        scoreText.text = (Score + value).ToString();
    }

    public void incrementCredits(float value)
    {
        creditsText.text = (Score + value).ToString();
    }
}
