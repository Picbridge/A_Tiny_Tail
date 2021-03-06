using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever_MultipleRotate : MonoBehaviour {

    [System.Serializable]
    public class FenceAxis
    {
        
        public GameObject FenceAxices;
        public bool clockWise = false;
        public bool isKeepRotating = false;
        //fence rotating axis
        public float yAxis;
        public float dy;
        public bool isSwitchChanged = true;
    }

    [SerializeField]
    private GameObject switchLeverAxis = null;
    [SerializeField]
    private float rotationSpeed = 3.0f;

    [SerializeField]
    public FenceAxis[] FenceInfo = null;

    //lever rotating axis
    private float zAxis = 25f;

    private bool isSwitchOn = false;
    
    [HideInInspector] public static int switchCount = 0; // For unlock character condition

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < FenceInfo.Length; i++)
        {
             FenceInfo[i].yAxis = 90.0f;
            FenceInfo[i].dy = 0;
            FenceInfo[i].isSwitchChanged = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (isSwitchOn == true)
        {
            if (zAxis > -25)
            {
                zAxis -= rotationSpeed;
            }
        }
        else
        {
            if (zAxis < 25)
            {
                zAxis += rotationSpeed;
            }
        }
        switchLeverAxis.transform.localRotation = Quaternion.Euler(0, 0, switchLeverAxis.transform.localRotation.z + zAxis);
  

        for(int i = 0;i < FenceInfo.Length;i++)
        {
            if (!FenceInfo[i].clockWise)
            {
                if(FenceInfo[i].isKeepRotating)
                {
                    if (FenceInfo[i].isSwitchChanged ==false)
                    {
                        if (FenceInfo[i].dy < 90f)
                        {
                            FenceInfo[i].dy += rotationSpeed;
                            FenceInfo[i].yAxis += rotationSpeed;
                            if(FenceInfo[i].yAxis>360)
                            {
                                FenceInfo[i].yAxis -= 360;
                            }
                        }
                        else
                        {
                            FenceInfo[i].isSwitchChanged = true;
                            FenceInfo[i].dy = 0;
                        }
                    }
                }
                else
                {
                    if (isSwitchOn == true)
                    {
                        if (FenceInfo[i].yAxis > 0f)
                        {
                            FenceInfo[i].yAxis -= rotationSpeed;
                        }
                    }
                    else
                    {
                        if (FenceInfo[i].yAxis < 90f)
                        {
                            FenceInfo[i].yAxis += rotationSpeed;
                        }
                    }
                }
            }
            else
            {
                if (FenceInfo[i].isKeepRotating)
                {
                    if (FenceInfo[i].isSwitchChanged == false)
                    {
                        if (FenceInfo[i].dy < 90f)
                        {
                            FenceInfo[i].dy += rotationSpeed;
                            FenceInfo[i].yAxis -= rotationSpeed;
                            if (FenceInfo[i].yAxis > 360)
                            {
                                FenceInfo[i].yAxis -= 360;
                            }
                        }
                        else
                        {
                            FenceInfo[i].isSwitchChanged = true;
                            FenceInfo[i].dy = 0;
                        }
                    }
                }
                else
                {
                    if (isSwitchOn == false)
                    {
                        if (FenceInfo[i].yAxis < 90f)
                        {
                            FenceInfo[i].yAxis += rotationSpeed;
                        }
                    }
                    else
                    {
                        if (FenceInfo[i].yAxis > 0f)
                        {
                            FenceInfo[i].yAxis -= rotationSpeed;
                        }
                    }
                }

            }
            FenceInfo[i].FenceAxices.transform.localRotation = Quaternion.Euler(0, FenceInfo[i].FenceAxices.transform.localRotation.y + FenceInfo[i].yAxis, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ++switchCount;

            isSwitchOn = !isSwitchOn;
            for (int i = 0; i < FenceInfo.Length; i++)
            {
                if (FenceInfo[i].isSwitchChanged == true)
                    FenceInfo[i].isSwitchChanged = false;
            }
        }
    }
}
