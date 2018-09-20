using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoMovement : MonoBehaviour
{

    const float GRAVITY = -10;

    const float START = 0;

    const float FIRSTLANE = 10;

    const float SECONDLANE = 20;

    public bool isPlayerTwo = true;

    public Bullet prefabBullet;

    List<Bullet> playerOneBullets = new List<Bullet>();
    List<Bullet> playerTwoBullets = new List<Bullet>();

    [HideInInspector] public bool playerOneIsDead = false;

    [HideInInspector] public bool playerTwoIsDead = false;

    Vector3 velocity = new Vector3(0, 0, 0);

    Vector3 playerPos = new Vector3(0, 0, 0);

    Vector3 playerTwoPos = new Vector3(0, 0, 0);

    public float laneWidth = 2;

    int playerOneLane = 0;

    int playerTwoLane = 0;

    public GameObject otherPlayer;

    public int playerOneHealth = 10;

    public int playerTwoHealth = 10;

    bool oneHasSwap = false;

    bool oneHasPush = false;

    bool oneHasSlide = false;

    bool twoHasSwap = false;

    bool twoHasPush = false;

    bool twoHasSlide = false;

    void Start()
    {
        otherPlayer = GameObject.FindGameObjectWithTag("PlayerOne");
    }

    // Update is called once per frame
    void Update()
    {
        //print(transform.position.y);
        Controls();

        velocity += new Vector3(0, GRAVITY, 0) * Time.deltaTime;

        transform.position += velocity * Time.deltaTime;

        otherPlayer.transform.position += velocity * Time.deltaTime;

        if (transform.position.y < .5) // if on the ground:
        {
            playerPos = transform.position; // copy the position
            playerPos.y = -.5f; // clamp y value
            transform.position = playerPos;
        }

        if (otherPlayer.transform.position.y < .5) // if on the ground:
        {
            playerTwoPos = otherPlayer.transform.position; // copy the position
            playerTwoPos.y = -.5f; // clamp y value
            otherPlayer.transform.position = playerTwoPos;
        }

        for (int i = playerOneBullets.Count - 1; i >= 0; i--)
        {
            if (playerOneBullets[i].isDead)
            {
                Destroy(playerOneBullets[i].gameObject);
                playerOneBullets.RemoveAt(i);
            }
        }

        for (int j = playerTwoBullets.Count - 1; j >= 0; j--)
        {
            if (playerTwoBullets[j].isDead)
            {
                Destroy(playerTwoBullets[j].gameObject);
                playerTwoBullets.RemoveAt(j);
            }
        }

        CheckDeath();
    }

    void Controls()
    {
        if (!isPlayerTwo)
        {
            float h = Input.GetAxisRaw("Horizontal P1");
            if (Input.GetButtonDown("Horizontal P1"))
            {
                if (h == -1) // if pressing left
                {
                    playerOneLane--;
                }
                else if (h == 1) // if pressing right
                {
                    playerOneLane++;
                }

            }

            float targetX = playerOneLane * laneWidth;

            float x = (targetX - transform.position.x) * .1f;
            transform.position += new Vector3(x, 0, 0);

            if (Input.GetButtonDown("Jump P1"))
            {
                if (transform.position.y <= 0)
                {
                    velocity.y = 7;
                }
            }

            if (Input.GetButtonDown("Bullets P1"))
            {
                SpawnBullet();
            }

            if (Input.GetButtonDown("Swap P1") && oneHasSwap)
            {
                Vector3 otherPos = otherPlayer.transform.position;
                Vector3 thisPos = transform.position;

                otherPlayer.transform.position = thisPos;

                transform.position = otherPos;
            }

            if (Input.GetButtonDown("Push P1") && oneHasPush)
            {
                if (otherPlayer.transform.position.z == START)
                {
                    otherPlayer.transform.position = new Vector3(0, 0, FIRSTLANE);
                }

                else if (otherPlayer.transform.position.z == FIRSTLANE)
                {
                    otherPlayer.transform.position = new Vector3(0, 0, SECONDLANE);
                }
                else Debug.Log("Reached Maximum Lane");
            }

            if (Input.GetButtonDown("Slide P1") && oneHasSlide)
            {
                if (transform.position.z == SECONDLANE)
                {
                    transform.position = new Vector3(0, 0, FIRSTLANE);
                }

                else if (transform.position.z == FIRSTLANE)
                {
                    transform.position = new Vector3(0, 0, START);
                }
                else Debug.Log("player reached start");
            }
        }

        else
        {
            float h = Input.GetAxisRaw("Horizontal P2");
            if (Input.GetButtonDown("Horizontal P2"))
            {
                if (h == -1) // if pressing left
                {
                    playerTwoLane--;
                }
                else if (h == 1) // if pressing right
                {
                    playerTwoLane++;
                }

            }

            float targetX = playerTwoLane * laneWidth;

            float x = (targetX - transform.position.x) * .1f;
            transform.position += new Vector3(x, 0, 0);

            if (Input.GetButtonDown("Jump P2"))
            {
                if (transform.position.y <= 0)
                {
                    velocity.y = 7;
                }
            }

            if (Input.GetButtonDown("Swap P2") && twoHasSwap)
            {
                Vector3 otherPos = otherPlayer.transform.position;
                Vector3 thisPos = transform.position;

                otherPlayer.transform.position = thisPos;

                transform.position = otherPos;
            }

            if (Input.GetButtonDown("Push P2") && twoHasPush)
            {
                if (otherPlayer.transform.position.z == START)
                {
                    otherPlayer.transform.position = new Vector3(0, 0, FIRSTLANE);
                }

                else if (otherPlayer.transform.position.z == FIRSTLANE)
                {
                    otherPlayer.transform.position = new Vector3(0, 0, SECONDLANE);
                }
                else Debug.Log("Reached Maximum Lane");
            }

            if (Input.GetButtonDown("Slide P2") && twoHasSlide)
            {
                if (transform.position.z == SECONDLANE)
                {
                    transform.position = new Vector3(0, 0, FIRSTLANE);
                }

                else if (transform.position.z == FIRSTLANE)
                {
                    transform.position = new Vector3(0, 0, START);
                }
                else Debug.Log("player reached start");
            }
        }


    }

    void OverlappingAABB(AABB other)
    {
        if (other.tag == "PowerUp")
        {
            //must be a powerup
            PowerUp powerup = other.GetComponent<PowerUp>();
            switch (powerup.type) //use an enum
            {
                case PowerUp.PowerUpType.Swap:
                    if (!isPlayerTwo) oneHasSwap = true;
                    else twoHasSwap = true;
                    break;
                case PowerUp.PowerUpType.Push:
                    if (!isPlayerTwo) oneHasPush = true;
                    else twoHasPush = true;
                    break;
                case PowerUp.PowerUpType.Slide:
                    if (!isPlayerTwo) oneHasSlide = true;
                    else twoHasSlide = true;
                    break;
                default:
                    Debug.Log("something might be broken. Check PlayerMovement, Track, or PowerUp Scripts.");
                    break;
            }

            Destroy(other.gameObject);
        }

        if (other.tag == "Wall")
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

        else
        {
            Bullet newBullet = Instantiate(prefabBullet, pos, Quaternion.identity);
            newBullet.transform.Rotate(90, 0, 0);
            playerTwoBullets.Add(newBullet);
        }
    }

    void CheckDeath()
    {

        if (transform.position.z <= -5)
        {
            if (!isPlayerTwo) playerOneIsDead = true;
            else playerTwoIsDead = true;
        }

        if (playerOneHealth == 0) playerOneIsDead = true;

        if (playerTwoHealth == 0) playerTwoIsDead = true;

        if (playerOneIsDead || playerTwoIsDead)
        {
            for (int i = playerOneBullets.Count - 1; i >= 0; i--)
            {
                Destroy(playerOneBullets[i].gameObject);
                playerOneBullets.RemoveAt(i);
            }

            for (int j = playerTwoBullets.Count - 1; j >= 0; j--)
            {
                Destroy(playerTwoBullets[j].gameObject);
                playerTwoBullets.RemoveAt(j);
            }
        }
    }
}
