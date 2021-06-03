using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult_InvisibleWallCollisionCheck : MonoBehaviour {

    Catapult_ShootCharacter parentScript;

    // Use this for initialization
    void Start () {
        parentScript = transform.parent.GetComponent<Catapult_ShootCharacter>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Character collidedCharacter = collision.gameObject.GetComponent<Character>();
            if (!collidedCharacter.isDivisible && !parentScript.capturedCharacter)
                Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }
}
