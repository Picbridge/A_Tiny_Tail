using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_CollisionCheck : MonoBehaviour
{
    Catapult_ShootCharacter parentScript;

    // Use this for initialization
    void Start ()
    {
        parentScript = transform.parent.GetComponent<Catapult_ShootCharacter>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            parentScript.isButtonPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            parentScript.isButtonPressed = false;
        }
    }
}
