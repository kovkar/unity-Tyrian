using TMPro;
using UnityEngine;

/// <summary>
/// Class for storing and managing <c>Score</c> and <c>Credits</c>. (Singleton)
/// </summary>
public class ScoreManager : MonoBehaviour
{
    /***************************** FIELDS ************************************/

    /// <value>Singleton instance.</value>
    public static ScoreManager Instance { get; private set; }

    /// <value>Coeficients for score calculation.</value>
    private readonly int[] SCORE_COEFS = { 1, 5, 10 };
    /// <value>Coeficients for credits calculation.</value>
    private readonly int[] CREDITS_COEFS = { 1, 1, 2 };

    /// <value>Current score of a player.</value>
    private int _score = 0;
    /// <value>Current amount of player's credits.</value>
    private int _credits = 0;
    /// <value>Threshold for adavncing to next level</value>
    private int threshold;

    [Header("Refferecnes")]
    /// <value>Refference to <c>TextMeshProUGUI</c> displaying <c>_score</c></value>
    [SerializeField] private TextMeshProUGUI scoreText;
    /// <value>Refference to <c>TextMeshProUGUI</c> displaying <c>_credits</c></value>
    [SerializeField] private TextMeshProUGUI creditsText;
    /// <value>Refference to <c>TextMeshProUGUI</c> displaying score threshold</value>
    [SerializeField] private TextMeshProUGUI scoreThreshold;


    /***************************** ENUMS ************************************/

    /// <value>Type of player actor collision.</value>
    private enum COLLISION_TYPE
    {
        CRASH, HIT, KILL
    }


    /***************************** PUBLIC METHODS ****************************/

    /// <summary>
    /// Updates <c>_score</c> and <c>_credist</c> after collision.
    /// </summary>
    /// <param name="enemy">enemy involved in collision</param>
    /// <param name="collision">collision data</param>
    public void ProcessCollision(Actor enemy, Collision collision)
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

    /// <param name="value">Sets score threshold.</param>
    public void SetScoreThreshold(int value)
    {
        threshold = value;
        scoreThreshold.text = $"/{value}";
    }


    /***************************** PRIVATE METHODS ***************************/

    private void IncrementScore(int value) 
    {
        _score += value;
        scoreText.text = _score.ToString();
        if (_score >= threshold)
        {
            LevelManager.Instance.Next();
        }
    }

    private void IncrementCredits(int value)
    {
        _credits += value;
        creditsText.text = (_score + value).ToString();
    }

    public void Awake()
    {
        // singleton
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        DontDestroyOnLoad(this);
    }
}
