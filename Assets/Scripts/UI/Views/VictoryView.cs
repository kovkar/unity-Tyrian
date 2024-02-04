using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class VictoryView : AView
{
    [SerializeField] private Button menuButton;
    [SerializeField] private TextMeshProUGUI timeText;
    public override void Initialize()
    {
        menuButton.onClick.AddListener(() => GameManager.Instance.ResetGame());
    }

    public override void DoShow(object args)
    {
        var time = GameManager.Instance.GameTimer.time;
        var minText = $"{time / 60}" + " minute" + ((time / 60 != 1) ? "s" : "");
        var secText = $"{time % 60}" + " second" + ((time % 60 != 1) ? "s" : "");
        timeText.text = $"Your final time is {minText} and {secText}.";
        base.DoShow(args);
    }
}