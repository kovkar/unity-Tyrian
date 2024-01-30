using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for managing <c>AView</c>s (UI panels).
/// </summary>
public class UIManager : MonoBehaviour
{
    /// <value>Singleton intstance.</value>
    public static UIManager Instance { get; private set; }
    /// <value>Initial <c>AView</c> shown after game laounch.</value>
    [SerializeField] private AView startingView;
    [SerializeField] private List<AView> views;

    private AView _currentView;
    private readonly Stack<AView> _history = new Stack<AView>();

    private void Awake()
    {
        // Check, if we do not have any instance yet.    
        if (Instance == null)
        {
            // 'this' is the first instance created => save it.
            Instance = this;
        }
        else if (Instance != this)
        {
            // Destroy 'this' object as there exist another instance
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        foreach (var view in views)
        {
            view.Initialize();
            view.DoHide();
        }

        if (startingView == null) return;
        Show(startingView, true);
    }

    public static T GetView<T>() where T : AView
    {
        foreach (var view in Instance.views)
        {
            if (view is not T tView) continue;
            return tView;
        }

        return null;
    }

    public static void Show<T>(object args = null, bool remember = true) where T : AView
    {
        foreach (var view in Instance.views)
        {
            if (view is T tView) // The view type matches
            {
                if (Instance._currentView != null)
                {
                    Instance._history.Push(Instance._currentView);
                    Instance._currentView.DoHide();
                }
                view.DoShow(args);
                Instance._currentView = view;
            }
        }
    }

    public static void Show(AView view, object args = null, bool remember = true)
    {
        if (Instance._currentView != null)
        {
            if (remember)
            {
                Instance._history.Push(Instance._currentView);
            }

            Instance._currentView.DoHide();
        }

        view.DoShow(args);
        Instance._currentView = view;
    }

    public static void ShowLast()
    {
        if (Instance._history.Count <= 0) return;
        // NOTE: Will not pass args again.
        // May or may not be desirable to add this functionality depending on your use-case
        Show(Instance._history.Pop(), false);
    }

    // Auto initialization in any scene. Requires a UI Manager prefab placed inside the Resources folder
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void AutoCreateManager()
    {
        if (UIManager.Instance != null) return;
        var uiManager = Resources.Load("UIManager") as GameObject;
        if (uiManager == null) return;
        var instance = Instantiate(uiManager);
    }
}
