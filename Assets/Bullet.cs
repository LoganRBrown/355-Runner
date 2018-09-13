using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    const float speed = 10;

    [HideInInspector] public bool isDead = false;

    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, speed) * Time.deltaTime;

        if (transform.position.z >= 200)
        {
            isDead = true;
        }
    }

    void OverlappingAABB(AABB other)
    {
        if (other.tag == "Wall")
        {
            Destroy(other.gameObject);
        }
    }
}
