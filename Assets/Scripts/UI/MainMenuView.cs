using UnityEngine;
using UnityEngine.UI;
public class MainMenuView : AView
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;
    public override void Initialize()
    {
        startButton.onClick.AddListener(() => UIManager.Show<LevelSelectionView>(null, true));
        creditsButton.onClick.AddListener(() => UIManager.Show<CreditsView>(null, true));
        exitButton.onClick.AddListener(() => Application.Quit());
    }
}