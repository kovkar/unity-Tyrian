using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelSelectionView : AView
{
    [SerializeField] private Button loadLevel1;
    [SerializeField] private Button loadLevel2;
    [SerializeField] private Button loadLevel3;
    [SerializeField] private Button backButton;
    public override void Initialize()
    {
        loadLevel1.onClick.AddListener(() => GameManager.Instance.StartGame(1));
        loadLevel2.onClick.AddListener(() => GameManager.Instance.StartGame(2));
        loadLevel3.onClick.AddListener(() => GameManager.Instance.StartGame(3));
        backButton.onClick.AddListener(() => UIManager.ShowLast());
    }

    public override void DoShow(object args)
    {
        base.DoShow(args);
    }

    public override void DoHide()
    {
        base.DoHide();
    }
}