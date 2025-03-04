using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosterAttackScript : MonoBehaviour {

    private float speed = 5;
    public float Speeds {
        get { return speed; }
        set { speed = value; }
    }

    private Animator animator;
    private Rigidbody2D rb;
    private GameObject _character;
    private Mana ownerMana;

    private void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update() {

        if (tag == AnimationString.tagGaia) {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            DestroyMonster(AnimationString.nameGaia);
        } else {
            var attackActions = new Dictionary<string, string> {
                { AnimationString.tagKnight, AnimationString.nameKinght },
                { AnimationString.tagPunch, AnimationString.namePunch },
                { AnimationString.tagSodier, AnimationString.nameSodier },
                { AnimationString.tagMagic, AnimationString.nameMagic },
                { AnimationString.skill1, AnimationString.nameSkill1 },
                { AnimationString.skill2, AnimationString.nameSkill2 }
            };
            if (attackActions.TryGetValue(tag, out string action)) {
                DestroyMonster(action);
            }
        }
    }

    //====================logic monster=================================
    public void OpenAttack() {
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

    private void DestroyMonster(string animation) {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animation)
                && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) {
            Destroy(gameObject);
            setMovementOn(_character);
        }
    }

    private void setMovementOn(GameObject character) {
        GamePadController gamePadController = character.GetComponent<GamePadController>();
        gamePadController.enabled = true;
    }

    public void setCharacter(GameObject character) {
        _character = character;
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
                if (gameObject.tag == "skill2") {
                    health.TakeDamage(100);
                } else {
                    health.TakeDamage(10);
                }
                ownerMana.GetMana(5/2);

                if (health.Hp <= 0) {
                    //var cap = collision.gameObject.GetComponent<CapsuleCollider2D>();
                    //var box = collision.gameObject.GetComponent<BoxCollider2D>();
                    //if (cap != null) cap.enabled = false;
                    //if (box != null) box.enabled = false;
                    var animation = collision.gameObject.GetComponent<Animator>();
                    animation.SetTrigger(AnimationString.die);
                }
            }
        }
    }

}
