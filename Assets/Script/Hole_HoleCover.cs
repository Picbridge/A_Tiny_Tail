using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole_HoleCover : MonoBehaviour {

    Transform childObject;
    Collider childObjectCollider;
    SpriteRenderer childObjectRenderer;
    GameObject childeObj;

    // Use this for initialization
    void Start () {
        childObject = transform.GetChild(0);
        childObjectCollider = childObject.GetComponent<Collider>();
        childObjectRenderer = childObject.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            childObjectCollider.enabled = true;
            childObjectRenderer.color = new Color(childObjectRenderer.color.r, childObjectRenderer.color.g, childObjectRenderer.color.b, 0);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
