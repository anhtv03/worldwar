using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateDamageDealer : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    //===================collision=======================================
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null) {
                var takeDamageHandler = collision.gameObject.GetComponent<ITakeDame>();
                takeDamageHandler.GetDamageEffect(collision.gameObject);
                int damage = Random.Range(500, 650);
                health.TakeDamage(damage / 2);

                if (health.Hp <= 0) {
                    var animation = collision.gameObject.GetComponent<Animator>();
                    animation.SetTrigger(AnimationString.die);
                }
            }
        }
    }

}
