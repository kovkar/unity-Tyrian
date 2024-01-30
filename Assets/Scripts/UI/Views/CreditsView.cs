using UnityEngine;
using UnityEngine.UI;
public class CreditsView : AView
{
    [SerializeField] private Button backButton;
    public override void Initialize()
    {
        backButton.onClick.AddListener(() => UIManager.ShowLast());
    }
}