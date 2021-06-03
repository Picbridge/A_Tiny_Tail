using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_L_TutorialManager : MonoBehaviour
{

    public GameObject lever;

    bool isLeverOn;
    // Use this for initialization
    void Start()
    {
        isLeverOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLeverOn)
            lever.SetActive(true);
        else
            lever.SetActive(false);
    }

    public void OnLever()
    {
        isLeverOn = !isLeverOn;
    }
}