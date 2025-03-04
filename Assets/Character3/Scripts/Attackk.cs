using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackk : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            Mana mana = gameObject.GetComponentInParent<Mana>();

            if (playerHealth != null) {
                playerHealth.TakeDamage(50);
                if (mana != null) {
                    mana.GetMana(5);
                }
            }
        }
    }
}
