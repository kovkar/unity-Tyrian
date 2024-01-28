using TMPro;
using UnityEngine;

public class FinalTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI finalTimeText;

    void Start()
    {
        var time = GameManager.Instance.getTimer().time;
        finalTimeText.text = finalTimeText.text + $" {time / 60}:{time % 60}";
    }
}
