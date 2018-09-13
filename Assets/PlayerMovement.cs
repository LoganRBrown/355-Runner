using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Bullet prefabBullet;
    List<Bullet> bullets = new List<Bullet>();

    public float laneWidth = 2;
    int lane = 0;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
            lane = Mathf.Clamp(lane, -1, 1);
        }

        if (Input.GetButtonDown("Jump"))
        {
            spawnBullet();
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
            //must be a wall
            //Destroy(other.gameObject);
        }
    }

    void spawnBullet()
    {
        Vector3 pos = (transform.position);

        Bullet newBullet = Instantiate(prefabBullet, pos, Quaternion.identity);
        bullets.Add(newBullet);
    }
}
