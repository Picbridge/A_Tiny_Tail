using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instruction_Point : MonoBehaviour {

    float speed = 0.05f;
    RectTransform rect;
    bool isMovingForward = true;

	// Use this for initialization
	void Start ()
    {
        rect = GetComponent<RectTransform>();
	}

    // Update is called once per frame
    void Update()
    {
        if (rect.position.x > Screen.width * 0.83)
            isMovingForward = false;
        else if (rect.position.x < Screen.width * 0.8)
            isMovingForward = true;

        if (isMovingForward)
            rect.position = new Vector3(rect.position.x + Time.deltaTime * speed * Screen.width, rect.position.y, rect.position.z);
        else
            rect.position = new Vector3(rect.position.x - Time.deltaTime * speed * Screen.width, rect.position.y, rect.position.z);
    }
}
