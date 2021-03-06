using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_GroundMove : MonoBehaviour {

    float rightLimit = -69f;
    float leftLimit = 0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0.0f, 0.0f, -5 * Time.deltaTime);
        if (transform.position.z <= rightLimit)
        {
            transform.Translate(0.0f, 0.0f, leftLimit - rightLimit);
        }
    }
}
