using TMPro;
using UnityEngine;

public class FinalTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalTimeText;

    void Start()
    {
        var time = GameManager.Instance.GameTimer.time;
        var minText = $"{time / 60}" + " minute" + ((time / 60 != 1) ? "s" : "");
        var secText = $"{time % 60}" + " second" + ((time % 60 != 1) ? "s" : "");
        finalTimeText.text = $"Your final time is {minText} and {secText}.";
    }
}
