using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_H_TutorialManager : MonoBehaviour
{

    public GameObject movement;
    public GameObject splitInst;
    public GameObject info;
    public GameObject goal;

    bool isMovementOn;
    bool isSplitOn;
    bool isInfoOn;
    bool isGoalOn;
    // Use this for initialization
    void Start()
    {
        isMovementOn = false;
        isSplitOn = false;
        isInfoOn = false;
        isGoalOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMovementOn)
            movement.SetActive(true);
        else
            movement.SetActive(false);

        if (isSplitOn)
            splitInst.SetActive(true);
        else
            splitInst.SetActive(false);

        if (isInfoOn)
            info.SetActive(true);
        else
            info.SetActive(false);

        if (isGoalOn)
            goal.SetActive(true);
        else
            goal.SetActive(false);
    }

    public void OnMovement()
    {
        isMovementOn = !isMovementOn;
        isSplitOn = false;
        isInfoOn = false;
        isGoalOn = false;
    }

    public void OnSplitInst()
    {
        isSplitOn = !isSplitOn;
        isMovementOn = false;
        isInfoOn = false;
        isGoalOn = false;
    }

    public void OnInfo()
    {
        isInfoOn = !isInfoOn;
        isMovementOn = false;
        isSplitOn = false;
        isGoalOn = false;
    }

    public void OnGoal()
    {
        isGoalOn = !isGoalOn;
        isMovementOn = false;
        isSplitOn = false;
        isInfoOn = false;
    }
}