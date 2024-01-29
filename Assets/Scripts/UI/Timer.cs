using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("Refferences")]
    [SerializeField] TextMeshProUGUI minutes;
    [SerializeField] TextMeshProUGUI seconds;

    private bool paused = true;

    public int time { get; private set; } = 0;

    private void Awake()
    {
        if (minutes is null || seconds is null) 
            Debug.LogError($"{gameObject.name} (Timer) is missing text refference!");
    }

    public void StartTimer()
    {
        paused = false;
        _ = run();
    }

    public void StopTimer()
    {
        paused = true;
    }
    
    public void ResetTimer()
    {
        paused = true;
        minutes.text = "0";
        seconds.text = "00";
    }

    private async Task run()
    {
        while (!paused)
        {
            await Task.Delay(1000);
            time += 1;
            minutes.text = $"{time / 60}";
            var s = time % 60;
            seconds.text = (s < 10) ? $"0{s}" : $"{s}";
        }
    }
}
