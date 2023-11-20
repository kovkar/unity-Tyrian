using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class SettingsMenuView : AView
{
    [SerializeField] private Button backButton;
    [SerializeField] private TextMeshProUGUI randomNumberText;
    public override void Initialize()
    {
        backButton.onClick.AddListener(() =>
        {
            DoHide();
            UIManager.ShowLast();
        });
    }
    public override void DoShow(object args)
    {
        base.DoShow(args);
        if (args is SettingsViewArgs viewArgs)
        {
            randomNumberText.text = viewArgs.randomizedNumber.ToString();
        }
        else
        {
            Debug.LogError("Incorrect window args!");
        }
    }
}
public struct SettingsViewArgs
{
    public int randomizedNumber;
    public SettingsViewArgs(int number)
    {
        randomizedNumber = number;
    }
}