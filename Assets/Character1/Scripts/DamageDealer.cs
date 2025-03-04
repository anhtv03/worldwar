using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour {
    // Start is called before the first frame update
    [SerializeField] int damage = 10;
    public int Damage { get { return damage; } }

    private bool isHit = false;
    private float timeHit = 0;
    void Start() {
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        var enemyAnimator = collision.gameObject.GetComponent<Animator>();
        var audioController = collision.gameObject.GetComponent<AudioClipController>();
        if (collision.gameObject.tag == "Player" && gameObject.tag != "skill1Shank" && gameObject.tag != "skillComboShank") {

            Mana mana = gameObject.GetComponentInParent<Mana>();
            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null) {
                health.TakeDamage(damage);
                if (mana != null) {
                    mana.GetMana(3);
                }
            }
        }
        if (collision.gameObject.tag == "Player" && gameObject.tag == "skill1Shank") {
            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null) {
                enemyAnimator.SetTrigger(AnimationString.takeDamage);
                if (audioController != null) {
                    audioController.TakeDamageClip();
                }
                health.TakeDamage(damage);
                Destroy(gameObject);


            }
        }
        if (collision.gameObject.tag == "Player" && gameObject.tag == "skillComboShank") {
            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null) {
                enemyAnimator.SetTrigger(AnimationString.takeDamage);
                if (audioController != null) {
                    audioController.TakeDamageClip();
                }
                health.TakeDamage(damage);
                Destroy(gameObject);

            }
        }
        var hp = collision.gameObject.GetComponent<Health>();
        if (hp != null && hp.Hp <= 0) {
            collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            var animation = collision.gameObject.GetComponent<Animator>();
            animation.SetTrigger(AnimationString.die);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            Mana mana = gameObject.GetComponentInParent<Mana>();
            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null) {
                health.TakeDamage(damage);
                var takeDamageHandler = collision.gameObject.GetComponent<ITakeDame>();
                if (collision.gameObject != null) {
                    takeDamageHandler.GetDamageEffect(collision.gameObject);
                }
                if (mana != null) {
                    mana.GetMana(5);
                } else {
                }
            }
            var hp = collision.gameObject.GetComponent<Health>();
            if (hp.Hp <= 0) {
                //collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                //collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                var animation = collision.gameObject.GetComponent<Animator>();
                animation.SetTrigger(AnimationString.die);
                Debug.Log("chet me may di 2");
            }
        }
    }


}
