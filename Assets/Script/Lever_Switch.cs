using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever_Switch : MonoBehaviour {

    [SerializeField]
    private GameObject switchFenceAxis = null;
    [SerializeField]
    private GameObject switchLeverAxis = null;
    [SerializeField]
    private float rotationSpeed = 3.0f;
    [SerializeField]
    private bool clockWise = false;

    //fence rotating axis
    private float yAxis;
    //lever rotating axis
    private float zAxis = 25f;

    private bool isSwitchOn = false;

    // Use this for initialization
    void Start()
    {
        if (!clockWise)
            yAxis = 90.0f;
        else
            yAxis = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!clockWise)
        {
            if (isSwitchOn == true)
            {
                if (yAxis > 0f)
                {
                    yAxis -= rotationSpeed;
                }
                if (zAxis > -25)
                {
                    zAxis -= rotationSpeed;
                }
            }
            else
            {
                if (yAxis < 90f)
                {
                    yAxis += rotationSpeed;
                }
                if (zAxis < 25)
                {
                    zAxis += rotationSpeed;
                }
            }
        }
        else
        {
            if (isSwitchOn == true)
            {
                if(yAxis < 90f)
                {
                    yAxis += rotationSpeed;
                }
                if (zAxis > -25)
                {
                    zAxis -= rotationSpeed;
                }
            }
            else
            {
                if (yAxis > 0f)
                {
                    yAxis -= rotationSpeed;
                }
                if (zAxis < 25)
                {
                    zAxis += rotationSpeed;
                }
            }
        }

        switchLeverAxis.transform.localRotation = Quaternion.Euler(0, 0, switchLeverAxis.transform.localRotation.z+zAxis);
        switchFenceAxis.transform.localRotation = Quaternion.Euler(0, switchFenceAxis.transform.rotation.y + yAxis, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isSwitchOn = !isSwitchOn;
        }
    }
}
