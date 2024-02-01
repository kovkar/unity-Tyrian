using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DefeatView : AView
{
    [SerializeField] private Button menuButton;
    public override void Initialize()
    {
        menuButton.onClick.AddListener(() => GameManager.Instance.ResetGame());
    }
}