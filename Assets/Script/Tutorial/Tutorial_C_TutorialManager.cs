using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_C_TutorialManager : MonoBehaviour
{

    public GameObject cannon;

    bool isCannonOn;
    // Use this for initialization
    void Start()
    {
        isCannonOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCannonOn)
            cannon.SetActive(true);
        else
            cannon.SetActive(false);
    }

    public void OnCannon()
    {
        isCannonOn = !isCannonOn;
    }
}