using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DameAttack : MonoBehaviour {
    // Start is called before the first frame update


    private void Awake() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            var animation = collision.gameObject.GetComponent<Animator>();
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            Mana mana = gameObject.GetComponentInParent<Mana>();

            if (playerHealth != null) {
                playerHealth.TakeDamage(10);
                animation.SetTrigger(AnimationString.takeDamage);
                if (mana != null) {
                    mana.GetMana(5);
                }
                if (playerHealth.Hp <= 0) {
                    animation.SetTrigger(AnimationString.die);
                }
            }
        }

    }
}