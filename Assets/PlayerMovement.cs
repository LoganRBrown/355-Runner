using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    const float GRAVITY = -10;

    const float START = 0;

    const float FIRSTLANE = 10;

    const float SECONDLANE = 20;

    public bool isPlayerTwo = false;

    public Bullet prefabBullet;

    List<Bullet> playerOneBullets = new List<Bullet>();

    [HideInInspector] public bool playerOneIsDead = false;

    Vector3 velocity = new Vector3(0, 0, 0);

    Vector3 playerPos = new Vector3(0, 0, 0);

    public float laneWidth = 2;

    public GameObject otherPlayer;

    public int playerHealth = 10;

    bool HasSwap = false;

    bool HasPush = false;

    bool HasSlide = false;

    bool inAir = true;

	void Start () {

        otherPlayer = GameObject.FindGameObjectWithTag("PlayerTwo");
        if(otherPlayer != null)
        {
            Debug.Log("Found Player 2");
        }
		
	}
	
	void Update () {
        Controls();

        velocity += new Vector3(0, GRAVITY, 0) * Time.deltaTime;

        transform.position += velocity * Time.deltaTime;

        if (transform.position.y < -.5) // if on the ground:
        {
            playerPos = transform.position; // copy the position
            playerPos.y = -.5f; // clamp y value
            transform.position = playerPos;
            inAir = false;
        }

        for (int i = playerOneBullets.Count - 1; i >= 0; i--)
        {
            if (playerOneBullets[i].isDead)
            {
                Destroy(playerOneBullets[i].gameObject);
                playerOneBullets.RemoveAt(i);
            }
        }

        CheckDeath();
	}

    void Controls()
    {
        if (!isPlayerTwo)
        {
            if (!inAir)
            {
                float x = Input.GetAxis("Horizontal P1");

                transform.Translate(new Vector3(x / 3, 0, 0));
            }

            if (Input.GetButtonDown("Jump P1"))
            {
                if(transform.position.y <= 0)
                {
                    inAir = true;
                    velocity.y = 8;
                }
            }

            if (Input.GetButtonDown("Bullets P1"))
            {
                SpawnBullet();
            }

            if (Input.GetButtonDown("Swap P1") && HasSwap)
            {
                Debug.Log("P1 Swap");
                Vector3 otherPos = otherPlayer.transform.position;
                Vector3 thisPos = transform.position;

                otherPlayer.transform.position = thisPos;

                transform.position = otherPos;

                HasSwap = false;
            }

            if (Input.GetButtonDown("Push P1") && HasPush)
            {
                Debug.Log("P1 Push");
                if (otherPlayer.transform.position.z == START)
                {
                    otherPlayer.transform.position = new Vector3(0, 0, FIRSTLANE);
                }

                else if (otherPlayer.transform.position.z == FIRSTLANE)
                {
                    otherPlayer.transform.position = new Vector3(0, 0, SECONDLANE);
                }
                else Debug.Log("Reached Maximum Lane");

                HasPush = false;
            }

            if (Input.GetButtonDown("Slide P1") && HasSlide)
            {
                Debug.Log("P1 Slide");
                if (transform.position.z == SECONDLANE)
                {
                    transform.position = new Vector3(0, 0, FIRSTLANE);
                }

                else if (transform.position.z == FIRSTLANE)
                {
                    transform.position = new Vector3(0, 0, START);
                }
                else if(transform.position.z <= START){
                    transform.position = new Vector3(0, 0, START);
                }
                else Debug.Log("player reached start");

                HasSlide = false;
            }
        }
    }

    void OverlappingAABB(AABB other)
    {
        if(other.tag == "PowerUp")
        {
            //must be a powerup
            PowerUp powerup = other.GetComponent<PowerUp>();
            switch (powerup.type) //use an enum
            {
                case PowerUp.PowerUpType.Swap:
                    if (!isPlayerTwo) HasSwap = true;
                    break;
                case PowerUp.PowerUpType.Push:
                    if (!isPlayerTwo) HasPush = true;
                    break;
                case PowerUp.PowerUpType.Slide:
                    if (!isPlayerTwo) HasSlide = true;
                    break;
                default:
                    Debug.Log("something might be broken. Check PlayerMovement, Track, or PowerUp Scripts.");
                    break;
            }

            Destroy(other.gameObject);
        }

        if(other.tag == "Wall")
        {
            transform.position += new Vector3(0, 0, -1);
        }

        if (other.tag == "TrackWall" && transform.position.x > 0)
        {
            transform.position += new Vector3(-1, 0, 0);
        }

        if (other.tag == "TrackWall" && transform.position.x < 0)
        {
            transform.position += new Vector3(1, 0, 0);
        }
    }

    void SpawnBullet()
    {
        Vector3 pos = (transform.position);

        if (!isPlayerTwo)
        {
            Bullet newBullet = Instantiate(prefabBullet, pos, Quaternion.identity);
            newBullet.transform.Rotate(90, 0, 0);
            playerOneBullets.Add(newBullet);
        }
    }

    void CheckDeath()
    {

        PlayerTwoMovement playerTwo = otherPlayer.GetComponent<PlayerTwoMovement>();

        if (transform.position.z <= -5)
        {
            if (!isPlayerTwo) playerOneIsDead = true;
            else playerTwoIsDead = true;
        }

        if (playerHealth == 0)
        {
            playerOneIsDead = true;
            print("Player 2 wins");
        }

        if ( playerTwo.playerHealth == 0)
        {
            playerTwoIsDead = true;
            print("Player 1 wins");
        }

        if (playerOneIsDead || playerTwoIsDead)
        {
            for (int i = playerOneBullets.Count - 1; i >= 0; i--)
            {
                    Destroy(playerOneBullets[i].gameObject);
                playerOneBullets.RemoveAt(i); 
            }
        }
    }
}
