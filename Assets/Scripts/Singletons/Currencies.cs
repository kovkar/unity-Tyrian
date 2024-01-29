using TMPro;
using UnityEngine;

/// <summary>
/// Class for storing and managing <c>Score</c> and <c>Credits</c>. (following Singleton pattern)
/// </summary>
public class Currencies : MonoBehaviour
{
    /// <value>Attribute <c>Score</c> represents current score of a player.</value>
    private int Score = 0;

    /// <value>Attribute <c>Credits</c> represents current amount of player's credits.</value>
    private int Credits = 0;

    /// <value>Public static reference to this object. (Singleton)</value>
    public static Currencies Instance { get; private set; }

    [Header("Refferecnes")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI scoreThreshold;
    [SerializeField] private TextMeshProUGUI creditsText;

    private readonly int[] SCORE_COEFS = { 1, 5, 10 };
    private readonly int[] CREDITS_COEFS = { 1, 1, 2 };

    private int threshold;

    private enum COLLISION_TYPE
    {
        CRASH, HIT, KILL
    }


    // **************** UNITY METHODS **************** //

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


    // **************** PUBLIC METHODS **************** //

    public void ProcessCollision(EnemyActor enemy, Collision collision)
    {
        var other = collision.gameObject.GetComponent<Actor>();
        if (other is null)
        {
            Debug.LogWarning($"{other.name} is not an Actor, but collision with {gameObject.name} is allowed by matrix.");
            return;
        }

        var type = COLLISION_TYPE.HIT;
        type = other.gameObject.layer == LayerMask.NameToLayer("Player") ? COLLISION_TYPE.CRASH : type;
        type = other.getDamage() >= enemy.getHealth() ? COLLISION_TYPE.KILL : type;

        int scoreIncrement = SCORE_COEFS[(int) type] * other.getDamage();
        int creditsIncreemnt = CREDITS_COEFS[(int) type] * other.getDamage();

        IncrementScore(scoreIncrement);
        IncrementCredits(creditsIncreemnt);
    }

    public void IncrementScore(int value) 
    {
        Score += value;
        scoreText.text = Score.ToString();
        if (Score >= threshold)
        {
            GameManager.Instance.LoadNextLevel();
        }
    }

    public void IncrementCredits(int value)
    {
        Credits += value;
        creditsText.text = (Score + value).ToString();
    }

    public void SetScoreThreshold(int value)
    {
        threshold = value;
        scoreThreshold.text = $"/{value}";
    }
}
