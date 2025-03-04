using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using static UnityEngine.UI.Image;
using UnityEngine.UIElements;

public class BulletMovement : MonoBehaviour {
    private Rigidbody2D rb;
    private float cameraWidth;
    private Mana ownerMana;
    private float speed = 6;
    public float Speeds {
        get { return speed; }
        set { speed = value; }
    }
    // Start is called before the first frame update
    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update() {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        if (Mathf.Abs(transform.position.x) > cameraWidth) {
            Destroy(gameObject);
        } else {
            Destroy(gameObject, 5);
        }
    }

    public void SetOwnerMana(Mana mana) {
        ownerMana = mana;
    }

    //===================collision=======================================
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null) {
                var takeDamageHandler = collision.gameObject.GetComponent<ITakeDame>();
                takeDamageHandler.GetDamageEffect(collision.gameObject);
                if (gameObject.tag == "fireBall") {
                    health.TakeDamage(10);
                } else {
                    health.TakeDamage(50);
                }
                ownerMana.GetMana(5/2);

                if (health.Hp <= 0) {
                    //var cap = collision.gameObject.GetComponent<CapsuleCollider2D>();
                    //var box = collision.gameObject.GetComponent<BoxCollider2D>();
                    //if (cap != null) cap.enabled = false;
                    //if(box  != null) box.enabled = false;
                    var animation = collision.gameObject.GetComponent<Animator>();
                    animation.SetTrigger(AnimationString.die);
                    Debug.Log("chet me may di 1");
                }
            }
            Destroy(gameObject);
        }
    }
}


