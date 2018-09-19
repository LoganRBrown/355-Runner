using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    const float speed = 20;

    [HideInInspector] public bool isDead = false;

    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, speed) * Time.deltaTime;

        if (transform.position.z >= 15)
        {
            isDead = true;
        }
    }

    void OverlappingAABB(AABB other)
    {
        if (other.tag == "Player")
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();

            if (player.isPlayerTwo) player.playerTwoHealth--;
            else player.playerOneHealth--;

            isDead = true;
        }
    }
}
