using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1Controller : MonoBehaviour
{
    
    Rigidbody2D rb;
    
    float time = 0;
    float localScaleX;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        time += Time.deltaTime;
        if (transform.rotation.y == 0)
        {
            rb.velocity = new Vector2(20, 0);
        }
        else
        {
            rb.velocity = new Vector2(-20, 0);
        }
        if (time >= 4)
        {
            Destroy(gameObject);
            time = 0;
        }
    }

    
    
}
