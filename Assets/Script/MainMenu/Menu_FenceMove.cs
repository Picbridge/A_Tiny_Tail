using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_FenceMove : MonoBehaviour
{
    float rightLimit = -34.58622f;
    float leftLimit = 26.60401f;
	// Use this for initialization
	void Start ()
    {
        //-8.56 -> gets out of sight 
        //2.144 ->start
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(0.0f , 0.0f, -5 * Time.deltaTime);
        if(transform.position.z <= rightLimit)
        {
            transform.Translate(0.0f, 0.0f, leftLimit - rightLimit);
        }
    }
}
