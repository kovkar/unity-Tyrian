using TMPro;
using UnityEngine;

/// <summary>
/// Class managing HUD visibility and providing HUD refferences (Singleton).
/// </summary>
public class HUDManager : MonoBehaviour
{
    
    /***************************** FIELDS ************************************/

    public static HUDManager Instance;

    [Header("HUD Layers")]
    /// <value>List of HUD elements belonging to <c>Layer.PLAYER</c>.</value>
    [SerializeField] private GameObject[] playerHUD;
    /// <value>List of HUD elements belonging to <c>Layer.BOSS</c>.</value>
    [SerializeField] private GameObject[] bossHUD;
    /// <value>List of HUD elements belonging to <c>Layer.LEVEL</c>.</value>
    [SerializeField] private GameObject[] levelHUD;

    [Header("HUD Refferences")]
    /// <value>Backfield for <c>PlayerHealthBar</c> property.</value>
    [SerializeField] private Bar playerHealthBar;
    /// <value>Backfield for <c>BossHealthBar</c> property.</value>v
    [SerializeField] private Bar bossHealthBar;
    /// <value>Backfield for <c>BossCannonTemperatureBar</c> property.</value>
    [SerializeField] private Bar bossCannonTemperatureBar;
    /// <value>Backfield for <c>BossPowerCooldownBar</c> property.</value>
    [SerializeField] private Bar bossPowerCooldownBar;
    /// <value>Backfield for <c>LevelNameText</c> property.</value>
    [SerializeField] private TextMeshProUGUI levelNameText;


    /***************************** ENUMS ************************************/

    /// <summary>
    /// HUD layer type
    /// </summary>
    public enum Layer
    {
        ALL,
        PLAYER,
        BOSS,
        LEVEL
    }


    /***************************** PROPERTIES ********************************/

    /// <value>Refference to <c>Bar</c> showing player health.</value>
    public Bar PlayerHealthBar { get => playerHealthBar; set { } }
    /// <value>Refference to <c>Bar</c> showing boss health.</value>
    public Bar BossHealthBar { get => bossHealthBar; set { } }
    /// <value>Refference to <c>Bar</c> showing boss cannon temperature.</value>
    public Bar BossCannonTemperatureBar { get => bossCannonTemperatureBar; set { } }
    /// <value>Refference to <c>Bar</c> showing boss power attack cooldown.</value>
    public Bar BossPowerCooldownBar { get => bossPowerCooldownBar; set { } }
    /// <value>
    public TextMeshProUGUI LevelNameText {  get => levelNameText; set { } }


    /***************************** PUBLIC METHODS ****************************/

    public void Show(Layer category = Layer.ALL)
    {
        SetVisibility(category, true);
    }

    public void Hide(Layer category = Layer.ALL)
    {
        SetVisibility(category, false);
    }


    /***************************** PRIVATE METHODS ***************************/

    private void SetVisibility(Layer category, bool value)
    {
        GameObject[] toChange = {};
        switch (category)
        {
            case Layer.PLAYER:
                toChange = playerHUD; 
                break;
            case Layer.BOSS:
                toChange = bossHUD; 
                break;
            case Layer.LEVEL:
                toChange = levelHUD;
                break;
            default: // Layer.ALL
                SetVisibility(Layer.PLAYER, value);
                SetVisibility(Layer.BOSS, value);
                SetVisibility(Layer.LEVEL, value);
                return;
        }
        foreach (GameObject element in toChange) 
        { 
            element.SetActive(value);
        }
    }

    private void Awake()
    {
        // singleton
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        // hide all at start by default
        Hide(Layer.ALL);
    }
}
