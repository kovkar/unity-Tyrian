using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class for switching levels.
/// </summary>
public class LevelManager : MonoBehaviour
{
    /***************************** FIELDS ************************************/

    /// <value>Singleton instance.</value>
    public static LevelManager Instance;

    /// <value>Index of currently loaded level (scene).</value>
    private int _current = -1;
    /// <value><c>true</c> if <c>Load</c> function is running.</value>
    private bool isLevelLoading = true;

    [Header("Refferences")]
    /// <value>Animations to be played when switching levels.</value>
    [SerializeField] private Animation elcipseAnimations;
    /// <value>Ordered list of levels (scenes) names.</value>
    [SerializeField] private string[] scenesNames;
    /// <value>Ordered list of score thresholds for advancing to necty level.</value>
    [SerializeField] private int[] scoreThresholds;



    /***************************** PUBLIC METHODS ****************************/

    /// <summary>
    /// Loads level (scene) with given index.
    /// </summary>
    /// <param name="levelIndex">Index of level in <c>scenesNames</c>.</param>
    public async void Load(int levelIndex)
    {
        isLevelLoading = true;
        // check index
        if (levelIndex < 0 || scenesNames.Length - 1 < levelIndex)
        {
            Debug.Log("Level index out of range.");
            return;
        }

        // wait for eclipse
        var animLength = elcipseAnimations["eclipse"].clip.length;
        elcipseAnimations.Play("eclipse");
        await Task.Delay((int) (animLength * 1000));

        // in eclipse
        _current = levelIndex;
        SceneManager.LoadScene(scenesNames[levelIndex]);
        ScoreManager.Instance.SetScoreThreshold(scoreThresholds[levelIndex]);

        HUDManager.Instance.Show(HUDManager.Layer.LEVEL);
        HUDManager.Instance.LevelNameText.text = scenesNames[levelIndex];
        await Task.Delay(1000);
        HUDManager.Instance.Hide(HUDManager.Layer.LEVEL);

        if (levelIndex == scenesNames.Length - 1)
        {
            HUDManager.Instance.Show(HUDManager.Layer.BOSS);
        }

        // start dawn
        elcipseAnimations.Play("dawn");
        animLength = elcipseAnimations["dawn"].clip.length;
        await Task.Delay((int) (animLength * 1000));
        isLevelLoading = false;
    }

    /// <summary>
    /// Loads next level (scene) from <c>scenesNames</c>.
    /// </summary>
    public void Next()
    {
        // check if next level available
        if (_current + 1 >= scenesNames.Length)
        {
            Debug.LogWarning("No more levels.");
            return;
        }
        // load next
        Load(_current + 1);
    }


    /***************************** PRIVATE METHODS ***************************/

    private void Awake()
    {
        // singleton
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this.gameObject);
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()

    {
        if (!isLevelLoading && Input.GetKeyDown(KeyCode.N)) Next();
    }
}
