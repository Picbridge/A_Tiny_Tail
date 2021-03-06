using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public float speed = 1.0f;
    [HideInInspector] public bool isGameEnd = false;
    [HideInInspector] public bool isCaptured = false;
    [HideInInspector] public bool isDivisible = false;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Collider characterCollision;
    Vector3 forward;
    Vector3 right;
    ParticleSystem particle;

    [HideInInspector]  public float speedCoolDown = 0;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        characterCollision = GetComponent<Collider>();
        isDivisible = false;
        forward = Camera.main.transform.forward;
        right = Camera.main.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // don't get particle system component if the character is old character (placeholder - white sphere)
        if (transform.childCount >= 2)
            particle = transform.GetChild(1).GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetKeyDown(KeyCode.Space))
            && isDivisible && !isCaptured)
        {
            bool result = Proliferation();

            if (result)
            {
                isDivisible = false;
                particle.Stop();
            }
        }

        if (speedCoolDown > 0)
        {
            speedCoolDown -= Time.deltaTime;
            speed = 4.0f;
        }
        else
            speed = 2.0f;

        Move();
    }

    void Move()
    {
        if (isCaptured || isGameEnd)
            return;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, 90, 90);
            transform.Translate(forward * speed * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, 0, 90);
            transform.Translate(-right * speed * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, -90, 90);
            transform.Translate(-forward * speed * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, 180, 90);
            transform.Translate(right * speed * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Potion" && !isDivisible)
        {
            isDivisible = true;
            particle.Play();
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.collider.tag == "Hole")
        {
            SoundManager.instance.PlaySound("FallingSound");

            Vector3 roundedcharPos = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
            TileMapManager.instance.UpdateTileMap(new Vector2(roundedcharPos.x, -roundedcharPos.z));

            if (isDivisible) // if character with divisible power fall into the hole
                CharacterSelectionManager.instance.UnlockCharacterByForce(12); // unlock poopoo

            //Vector2 characterGrid = new Vector2(roundedcharPos.x, -roundedcharPos.z);
            //Tile characterCell = TileMapManager.instance.level[GameSceneManager.instance.currentLevelIndex].tile_map[(int)characterGrid.x, (int)characterGrid.y];

            Characters.instance.RemoveCharacter(this);
            Destroy(this);
        }
    }

    //Proliferation : 자가증식하다
    private bool Proliferation()
    {        
        Vector3 charaterPos = transform.position;
        Vector3 roundedPos = new Vector3(Mathf.Round(charaterPos.x), Mathf.Round(charaterPos.y), Mathf.Round(charaterPos.z));
        //inversed Z (Scene Problem)
        Vector2 characterGrid = new Vector2(roundedPos.x, - roundedPos.z);
        Vector3 goalPos = new Vector3(0, 0, 0);

        float tilePortion = TileMapManager.instance.tilePortion;
        int howManyChecked = 0;

        Tile characterCell = TileMapManager.instance.level[GameSceneManager.instance.currentLevelIndex].tile_map[(int)characterGrid.x, (int)characterGrid.y];


        if (characterCell.isGoingLeft)
        {
            goalPos = new Vector3(roundedPos.x, roundedPos.y, roundedPos.z + tilePortion);
            howManyChecked = 1;
        }
        else if (characterCell.isGoingUp)
        {
            goalPos = new Vector3(roundedPos.x + tilePortion, roundedPos.y, roundedPos.z);
            howManyChecked = 2;
        }
        else if (characterCell.isGoingRight)
        {
            goalPos = new Vector3(roundedPos.x, roundedPos.y, roundedPos.z - tilePortion);
            howManyChecked = 3;
        }
        else if (characterCell.isGoingDown)
        {
            goalPos = new Vector3(roundedPos.x - tilePortion, roundedPos.y, roundedPos.z);
            howManyChecked = 4;
        }
        else
        {
            Debug.Log("CANNOT SEPERATE: Player is surrounding with something");
            return false;
        }

        GameSceneManager gamesceneManger = GameSceneManager.instance;

        if (gamesceneManger.IsGoalPosOccupied(goalPos) == false)
        {
            if (characterCell.isGoingUp && howManyChecked < 2)
            {
                goalPos = new Vector3(roundedPos.x + tilePortion, roundedPos.y, roundedPos.z);
            }
            else if (characterCell.isGoingRight && howManyChecked < 3)
            {
                goalPos = new Vector3(roundedPos.x, roundedPos.y, roundedPos.z - tilePortion);
            }
            else if (characterCell.isGoingDown && howManyChecked < 4)
            {
                goalPos = new Vector3(roundedPos.x - tilePortion, roundedPos.y, roundedPos.z);
            }
            else
            {
                Debug.Log("CANNOT SEPERATE_2: Player is surrounding with something");
                return false;
            }
        }

        SoundManager.instance.PlaySound("DivergeSound");

        Character clone = Instantiate(this, goalPos, Quaternion.identity) as Character;
        clone.transform.rotation = transform.rotation;
        clone.transform.SetParent(transform.parent);
        Characters.instance.AddCharacter(clone);
        return true;
    }
}
