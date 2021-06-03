using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_CharacterBounce : MonoBehaviour {

    [SerializeField]
    private float jumpForce;
    private bool isFalling = false;
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isFalling == false)
        {
            rb.velocity = transform.up * jumpForce;
            isFalling = true;
        }
	}
    private void OnCollisionStay()
    {
        isFalling = false;
    }
}
