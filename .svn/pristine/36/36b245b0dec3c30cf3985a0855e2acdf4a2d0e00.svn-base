using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingWall : MonoBehaviour
{
    public bool isWall = true;

    Coroutine floatCoroutine = null;
    float Range_Y = 0;
    float Range_MY = 0;

    Vector3 position;

	void Start ()
    {
        position = transform.position;

        if (isWall == true)
        {
            Range_Y = 0.7f;
            Range_MY = 10.0f;
        }
        else
        {
            Range_MY = Range_Y = 0.13f;            
        }

        if (floatCoroutine == null)
        {
            floatCoroutine = StartCoroutine(Floating());
        }
    }
	
	
	void Update ()
    {

	}

    IEnumerator Floating()
    {
        float value = 0;

        while (value == 0)        
            value = Random.Range(-Range_Y, Range_Y);
        

        while (true)
        {
            transform.position = new Vector3(position.x, transform.position.y + value * Time.deltaTime, position.z);

            if (Mathf.Abs(position.y - transform.position.y) > Range_MY)
            {
                value = -value;
            }

            yield return null;
        }
    }
}
