using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    const float GRAVITY = -10;

    Vector3 velocity = new Vector3(0, 0, 0);

    public Bullet prefabBullet;
    List<Bullet> bullets = new List<Bullet>();

    public float laneWidth = 2;
    int lane = 0;

    public bool isDead = false;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        print(transform.position.y);
        velocity += new Vector3(0, GRAVITY, 0) * Time.deltaTime;

        transform.position += velocity * Time.deltaTime;

        float h = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Horizontal"))
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

        if (Input.GetButtonDown("Jump"))
        {
            SpawnBullet();
        }

        for(int i = bullets.Count - 1; i >= 0; i--)
        {
            if (bullets[i].isDead)
            {
                Destroy(bullets[i].gameObject);
                bullets.RemoveAt(i);
            }
        }

        float targetX = lane * laneWidth;

        float x = (targetX - transform.position.x) * .1f;
        transform.position += new Vector3(x, 0, 0);

        if (transform.position.z <= -5) isDead = true;

        CheckDeath();
	}

    void OverlappingAABB(AABB other)
    {
        if(other.tag == "Powerup")
        {
            //must be a powerup
            //Powerup powerup = other.GetComponent<Powerup>();
            //switch (powerup.type) //use an enum
            //{
            //    case
            //        break;
            //}
            Destroy(other.gameObject);
        }
        if(other.tag == "Wall")
        {
            transform.position += new Vector3(0, 0, -1);
        }

        if(other.tag == "Track")
        {
            Vector3 pos = transform.position;
            pos.y = 1;
            transform.position = pos;
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

        Bullet newBullet = Instantiate(prefabBullet, pos, Quaternion.identity);
        newBullet.transform.Rotate(90, 0, 0);
        bullets.Add(newBullet);
    }

    void CheckDeath()
    {
        if (isDead)
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                    Destroy(bullets[i].gameObject);
                    bullets.RemoveAt(i); 
            }
        }
    }
}
