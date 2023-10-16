using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    public static Cheats Instance { get; private set; }

    private bool ImmortalityON = false;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) 
        {
            this.ImmortalityON = !this.ImmortalityON;
            Debug.LogWarning("[CHEATS] Immortality " + ((ImmortalityON) ? "activated" : "deactivated" + "."));
        }
    }

    public bool isImmortal() { return this.ImmortalityON; }
}
