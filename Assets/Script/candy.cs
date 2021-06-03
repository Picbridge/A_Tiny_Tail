using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class candy : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f,
            Random.Range(0.1f, 1.0f),
            0.0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            RankingManager.instance.ObtainObject();
            gameObject.SetActive(false);
        }
    }
}
