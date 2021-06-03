using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Path : MonoBehaviour
{
    public Vector3[] path;
    int counter = 0;

    Coroutine batCoroutine;

    private void Update()
    {
        if (counter == path.Length)
            counter = 0;

        if (batCoroutine == null)
            batCoroutine = StartCoroutine(PathFind());

        
       
    }

    IEnumerator PathFind()
    {        
        Vector3 start = transform.position;
        Vector3 goal = path[counter];
                
        counter++;

        float dist = Vector3.Distance(transform.position, goal);
        Vector3 direction = goal - start;
        
        transform.LookAt(direction);
        

        while (dist > 0.7f)
        {            
            transform.position = Vector3.Lerp(transform.position, goal, 0.4f * Time.deltaTime);

           // if (dist < 0.3f && first == true)
           // {
           //     first = false;
           //     yield return new WaitForSeconds(1.0f);
           // }

            dist = Vector3.Distance(transform.position, goal);            

            yield return null;
        }

        batCoroutine = null;
    }
}
