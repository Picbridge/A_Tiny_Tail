using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult_ShootCharacter : MonoBehaviour
{
    Transform catapult;
    Transform destination;
    Transform pivot;
    Transform lockPoint;

    public float SHOOTING_ANGLE = 45.0f;
    public float GRAVITY = 9.8f;

    float targetDistance;
    float projectileSpeed;
    Vector2 delta;
    float flightDuration;
    float elapseTime;

    Vector3 originalScale;
    Quaternion originalRotation;
    Quaternion originalCatapultRotation;
    Vector3 exactDestinationPoint;
    Coroutine projectileSimulationCoroutine;

    bool isCharacterProjectileStarted = false;
    bool isCharacterProjectileProcessing = false;

    [HideInInspector] public bool isOccupied;
    [HideInInspector] public Character capturedCharacter;
    [HideInInspector] public bool isButtonPressed;

    // Use this for initialization
    void Start ()
    {
        catapult = transform.GetChild(0);
        destination = transform.GetChild(1);
        pivot = catapult.GetChild(0);
        lockPoint = pivot.GetChild(0).GetChild(0);

        isOccupied = false;
        isButtonPressed = false;

        projectileSimulationCoroutine = null;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!isOccupied && capturedCharacter)
        {
            isOccupied = true;

            if(!capturedCharacter.isDivisible)
            {
                SoundManager.instance.PlaySound("CannonFillSound");

                exactDestinationPoint = new Vector3(destination.position.x,
                                                     capturedCharacter.transform.position.y,
                                                     destination.position.z);

                /* Rotate cannon to face the destination */
                originalCatapultRotation = catapult.rotation;
                catapult.rotation = Quaternion.LookRotation(exactDestinationPoint - capturedCharacter.transform.position);
                pivot.Rotate(transform.right, -SHOOTING_ANGLE);
                
                originalScale = capturedCharacter.transform.localScale;
                originalRotation = capturedCharacter.transform.rotation;           

                capturedCharacter.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                capturedCharacter.characterCollision.enabled = false;
                capturedCharacter.rb.useGravity = false;
                capturedCharacter.rb.isKinematic = true;
                capturedCharacter.isCaptured = true;
                capturedCharacter.transform.position = lockPoint.position;
            }            
        }

        if (isOccupied && isButtonPressed && !isCharacterProjectileStarted)
        {
            if (!capturedCharacter.isDivisible)
                isCharacterProjectileStarted = true;                
        }

        if (isCharacterProjectileStarted && !isCharacterProjectileProcessing)
        {
            isCharacterProjectileProcessing = true;
            SoundManager.instance.PlaySound("CannonSound");

            if (projectileSimulationCoroutine != null)
                StopCoroutine(projectileSimulationCoroutine);
            projectileSimulationCoroutine = StartCoroutine(SimulateProjectile());      
        }
    }

    IEnumerator SimulateProjectile()
    {        
        capturedCharacter.transform.localScale = originalScale;

        /* Calculate distance to target */
        targetDistance = Vector3.Distance(capturedCharacter.transform.position, exactDestinationPoint);

        /* Calculate the speed needed to projectile the object to the target at specified angle */
        projectileSpeed = targetDistance / (Mathf.Sin(2 * SHOOTING_ANGLE * Mathf.Deg2Rad) / GRAVITY);

        /* Extract the delta x, y componenent of the velocity */
        delta.x = Mathf.Sqrt(projectileSpeed) * Mathf.Cos(SHOOTING_ANGLE * Mathf.Deg2Rad);
        delta.y = Mathf.Sqrt(projectileSpeed) * Mathf.Sin(SHOOTING_ANGLE * Mathf.Deg2Rad);

        /* Calculate flight time */
        flightDuration = targetDistance / delta.x;

        /* Rotate projectile to face the destination */
        capturedCharacter.transform.rotation = Quaternion.LookRotation(exactDestinationPoint - capturedCharacter.transform.position);

        elapseTime = 0;

        while (elapseTime < flightDuration)
        {
            capturedCharacter.transform.Translate(0, (delta.y - (GRAVITY * elapseTime)) * Time.deltaTime, delta.x * Time.deltaTime);

            elapseTime += Time.deltaTime;

            yield return null;
        }

        catapult.rotation = originalCatapultRotation;
        pivot.Rotate(transform.right, SHOOTING_ANGLE);
        capturedCharacter.transform.rotation = originalRotation;
        capturedCharacter.isCaptured = false;
        capturedCharacter.rb.useGravity = true;
        capturedCharacter.rb.isKinematic = false;
        capturedCharacter.characterCollision.enabled = true;
        capturedCharacter = null;
        isOccupied = false;
        isCharacterProjectileStarted = false;
        isCharacterProjectileProcessing = false;
    }
}
