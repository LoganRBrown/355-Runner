using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    const float GRAVITY = -10;

    const float FIRSTLANE = 10;

    const float SECONDLANE = 20;

    public bool isPlayerTwo = false;

    Vector3 velocity = new Vector3(0, 0, 0);

    Vector3 playerPos = new Vector3(0, 0, 0);

    public Bullet prefabBullet;
    List<Bullet> playerOneBullets = new List<Bullet>();
    List<Bullet> playerTwoBullets = new List<Bullet>();

    public float laneWidth = 2;
    int lane = 0;

    [HideInInspector] public bool isDead = false;

    bool hasSwap = false;

    bool hasPush = false;

    bool hasSlide = false;

    public GameObject otherPlayer;

	void Start () {
        if (!isPlayerTwo)
        {
            otherPlayer = GameObject.Find("PlayerTwo");
        }
        else
        {
            otherPlayer = GameObject.Find("PlayerOne");
        }
		
	}
	
	// Update is called once per frame
	void Update () {
        //print(transform.position.y);
        Controls();

        velocity += new Vector3(0, GRAVITY, 0) * Time.deltaTime;

        transform.position += velocity * Time.deltaTime;

        if (transform.position.y < .5) // if on the ground:
        {
            playerPos = transform.position; // copy the position
            playerPos.y = .5f; // clamp y value
            transform.position = playerPos;
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

        float targetX = lane * laneWidth;

        float x = (targetX - transform.position.x) * .1f;
        transform.position += new Vector3(x, 0, 0);

        if (transform.position.z <= -5) isDead = true;

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
                    lane--;
                }
                else if (h == 1) // if pressing right
                {
                    lane++;
                }

            }

            if(Input.GetButtonDown("Jump P1"))
            {
                if(transform.position.y <= 0)
                {
                    velocity.y = 7;
                }
            }

            if (Input.GetButtonDown("Bullets P1"))
            {
                SpawnBullet();
            }

            if (Input.GetButtonDown("Swap P1"))
            {

            }

            if (Input.GetButtonDown("Push P1"))
            {

            }

            if (Input.GetButtonDown("Slide P1"))
            {

            }
        }

        else
        {
            float h = Input.GetAxisRaw("Horizontal P2");
            if (Input.GetButtonDown("Horizontal P2"))
            {
                if (h == -1) // if pressing left
                {
                    lane--;
                }
                else if (h == 1) // if pressing right
                {
                    lane++;
                }

            }

            if (Input.GetButtonDown("Jump P2"))
            {
                if (transform.position.y <= 0)
                {
                    velocity.y = 7;
                }
            }

            if (Input.GetButtonDown("Bullets P2"))
            {
                SpawnBullet();
            }

            if (Input.GetButtonDown("Swap P2"))
            {

            }

            if (Input.GetButtonDown("Push P2"))
            {

            }

            if (Input.GetButtonDown("Slide P2"))
            {

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
                    hasSwap = true;
                    break;
                case PowerUp.PowerUpType.Push:
                    hasPush = true;
                    break;
                case PowerUp.PowerUpType.Slide:
                    hasSlide = true;
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

        //if(other.tag == "Track")
        //{
        //    transform.position = playerPos;
        //}

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
        if (isDead)
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
