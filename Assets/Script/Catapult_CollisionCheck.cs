using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult_CollisionCheck : MonoBehaviour
{
    Catapult_ShootCharacter parentScript;
    private int capturedTimes = 0; // For unlock character condition

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
        if(other.tag == "Player")
        {
            Character collidedCharacter = other.GetComponent<Character>();
            if (!parentScript.capturedCharacter)
            {
                parentScript.capturedCharacter = collidedCharacter;
                ++capturedTimes;

                if (capturedTimes >= 2)
                    CharacterSelectionManager.instance.UnlockCharacter(7); // Unlock Bomb
            }            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Character collidedCharacter = other.GetComponent<Character>();
            if (collidedCharacter.isDivisible && parentScript.capturedCharacter)
            {
                if (parentScript.capturedCharacter.isDivisible)
                {
                    parentScript.capturedCharacter = null;
                    parentScript.isOccupied = false;
                }
            }
        }
    }
}
