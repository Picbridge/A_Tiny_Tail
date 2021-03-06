using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermesShoes : MonoBehaviour {

    GameObject item;
    GameObject shoesSprite;

    [HideInInspector] public float respawnTime = 0;
    bool respawnEffectDone = true;

    Coroutine hermesCoroutine;

	// Use this for initialization
	void Start ()
    {
        item = transform.GetChild(0).gameObject;
        shoesSprite = item.transform.GetChild(0).gameObject;
        StartCoroutine(floating());
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (respawnTime != 0)
        {
            respawnTime -= Time.deltaTime;

            if (respawnTime < 0)
            {
                respawnTime = 0;
               // item.SetActive(true);

               if (hermesCoroutine == null)
               {                   
                   hermesCoroutine = StartCoroutine(ItemEffect(false));
               }
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (respawnTime == 0 && respawnEffectDone == true)
            {
                //item.SetActive(false);

                if (hermesCoroutine == null)
                {                    
                    hermesCoroutine = StartCoroutine(ItemEffect(true));
                }

                other.GetComponent<Character>().speedCoolDown = 5f;

                respawnTime = 10f;
                respawnEffectDone = false;            
            }
        }
    }

    IEnumerator ItemEffect(bool vanishing)
    {
        int value = vanishing ? -1 : 1;        

        if(vanishing == false)            
            item.SetActive(true);

        if(vanishing == true)
            shoesSprite.SetActive(false);

        Color itemColor = item.GetComponent<MeshRenderer>().material.color;
        Color color = itemColor;
        while (true)
        {
            color.a += Time.deltaTime * value;
            item.GetComponent<MeshRenderer>().material.color = color;

            if (color.a >= 0.517f)
            {
                respawnEffectDone = true;
                break;
            }
            if (color.a <= 0)
            {
                item.SetActive(false);
                break;
            }

            yield return null;
        }

        if (vanishing == false)
            shoesSprite.SetActive(true);

        hermesCoroutine = null;

        
    }

    IEnumerator floating()
    {
        float height = item.transform.position.y;
        float time = 2.0f;
        float value = 0.05f;

        while (true)
        {
            time -= Time.deltaTime;
            height += value * Time.deltaTime;

            item.transform.position = new Vector3(item.transform.position.x, height, item.transform.position.z);

            if (time < 0)
            {
                time = 3.0f;
                value = -value;
            }

            yield return null;
        }
    }
}
