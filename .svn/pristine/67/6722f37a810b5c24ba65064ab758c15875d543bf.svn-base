using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_CharacterMove : MonoBehaviour {

    enum STATE { SPEED_UP, WAIT, SPEED_DOWN }

    [SerializeField]
    private float forwardSpeed;
    [SerializeField]
    private float backwardSpeed;
    [SerializeField]
    private float offset;
    Vector3 originalPos;
    [SerializeField]
    STATE state = STATE.SPEED_UP;
    Coroutine waitSecondsCoroutine;

    // Use this for initialization
    void Start () {
        originalPos = transform.position;
        waitSecondsCoroutine = null;
    }
	
	// Update is called once per frame
	void Update () {
        if (state == STATE.SPEED_DOWN && transform.position.z < originalPos.z - offset)
        {
            if (waitSecondsCoroutine != null)
                StopCoroutine(waitSecondsCoroutine);
            waitSecondsCoroutine = StartCoroutine(WaitSeconds(STATE.SPEED_UP));
        }
        if (state == STATE.SPEED_UP && transform.position.z > originalPos.z + offset)
        {
            if (waitSecondsCoroutine != null)
                StopCoroutine(waitSecondsCoroutine);
            waitSecondsCoroutine = StartCoroutine(WaitSeconds(STATE.SPEED_DOWN));
        }

        if (state == STATE.SPEED_UP)
            transform.Translate(0, 0, forwardSpeed / 100 * Time.timeScale);
        else if (state == STATE.SPEED_DOWN)
            transform.Translate(0, 0, -backwardSpeed / 100 * Time.timeScale);
    }

    IEnumerator WaitSeconds(STATE changeState)
    {
        state = STATE.WAIT;
        yield return new WaitForSeconds(1.0f);
        state = changeState;
    }
}
