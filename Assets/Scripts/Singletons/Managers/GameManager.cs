using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /***************************** FIELDS ************************************/

    /// <value>Singleton instance.</value>
    public static GameManager Instance { get; private set; }

    [Header("Refferences")]
    [SerializeField] private Animation shipIntro;


    /***************************** PROPERTIES ********************************/

    public Timer GameTimer { get; private set; }

    /***************************** PUBLIC METHODS ****************************/

    /// <summary>
    /// Starts game from given level.
    /// </summary>
    /// <param name="startingLevel">starting level</param>
    public async void StartGame(int startingLevel)
    {
        UIManager.Show<EmptyView>();
        HUDManager.Instance.Hide(HUDManager.Layer.ALL);
        HUDManager.Instance.Show(HUDManager.Layer.PLAYER);
        var animLength = shipIntro.clip.length;
        shipIntro?.Play();
        await Task.Delay((int)(animLength * 1000));
        LevelManager.Instance.Load(startingLevel - 1);
        GameTimer.StartTimer();
    }

    /// <summary>
    /// Ends game and switches to endgame scene.
    /// </summary>
    /// <param name="victory"><true> if player wins.</param>
    public void EndGame(bool victory)
    {
        GameTimer.StopTimer();
        if (victory)
        {
            SceneManager.LoadScene("Victory");
            UIManager.Show<VictoryView>();
        }
        else
        {
            SceneManager.LoadScene("Loss");
            UIManager.Show<DefeatView>();
        }
        HUDManager.Instance.Hide(HUDManager.Layer.ALL);
        GameTimer.ResetTimer();
    }

    /// <summary>
    /// Resets game.
    /// </summary>
    public void ResetGame()
    {
        // destroy dont destroy on load objects
        Destroy(HUDManager.Instance.gameObject);
        Destroy(LevelManager.Instance.gameObject);
        Destroy(ScoreManager.Instance.gameObject);
        Destroy(UIManager.Instance.gameObject);
        Destroy(this.gameObject);
        // load entry scene
        SceneManager.LoadScene("Menus");
    }


    /***************************** PRIVATE METHODS ***************************/

    public void Awake()
    {
        // singleton
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this.gameObject);
    }

    void Start()
    {
        GameTimer = GetComponent<Timer>();
        DontDestroyOnLoad(this);
    }
}
