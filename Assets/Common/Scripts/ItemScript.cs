using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {
    float cameraWidth, positionX, positionY, positionZ;

    // Start is called before the first frame update
    void Start() {
        positionY = transform.position.y;
        positionZ = transform.position.z;
        float gOWidthX = gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        cameraWidth = Camera.main.orthographicSize * Camera.main.aspect - gOWidthX;
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (gameObject.tag == "DropItem")
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(50);
            }
            else if (gameObject.tag == "HealthItem")
            {
                collision.gameObject.GetComponent<Health>().GetHealth(50);
            }
            else if (gameObject.tag == "ManaItem")
            {
                collision.gameObject.GetComponent<Mana>().GetMana(10);
            }
            gameObject.SetActive(false);

        }
        
    }


}
