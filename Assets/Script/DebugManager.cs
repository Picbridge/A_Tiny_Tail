using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour {

    #region Singleton

    public static DebugManager instance; // variable to store the actual instance for the class object

    void Awake()
    {
        /* if object is already created */
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of DebugManager found!");
            return;
        }
        instance = this;
    }

    #endregion

    public static bool isDebugOn { get; private set; }
    float time = 0;
    public GameObject debugCanvas;
    
    void Update ()
    {
        if (Input.GetKey(KeyCode.Slash))
        {
            time += Time.deltaTime;

            if (time > 1.5f)
                isDebugOn = true;
        }
        else
            time = 0;

        if (isDebugOn)
            debugCanvas.SetActive(true);
    }

    void ToggleDebug()
    {
        isDebugOn = !isDebugOn;
    }

    public void OnCloseDebug()
    {
        isDebugOn = false;
        debugCanvas.SetActive(false);
    }
}
