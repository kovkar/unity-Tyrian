using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private String[] listOfScenesNames;

    public void Awake()
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

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.N)) { loadNextLevel(); }
    }

    private void loadNextLevel() 
    {
        int currentLevelIndex = Array.IndexOf(listOfScenesNames, SceneManager.GetActiveScene().name);
        if (currentLevelIndex + 1 >= listOfScenesNames.Length) { Debug.LogWarning("No more levels!"); }
        else { SceneManager.LoadScene(listOfScenesNames[currentLevelIndex + 1]); }
    }

    public void endGame()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
