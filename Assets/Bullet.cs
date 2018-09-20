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

        if (transform.position.z >= 25)
        {
            isDead = true;
        }

        PlayerMovement playerOne = GetComponent<PlayerMovement>();
        PlayerTwoMovement playerTwo = GetComponent<PlayerTwoMovement>();


        if (playerOne.playerOneIsDead || playerTwo.playerTwoIsDead)
        {
            Destroy(gameObject);
        }
    }

    void OverlappingAABB(AABB other)
    {
        if (other.tag == "Player")
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            PlayerTwoMovement playerTwo = other.GetComponent<PlayerTwoMovement>();

            if (playerTwo.isPlayerTwo) playerTwo.playerTwoHealth--;
            else player.playerOneHealth--;

            isDead = true;
        }

        if (other.tag == "Wall")
        {
            isDead = true;
        }
    }
}
