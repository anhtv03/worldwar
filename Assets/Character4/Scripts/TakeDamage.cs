using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour, ITakeDame {

    Animator animator;
    MovementController movementController;
    GameObject enemyPlayer;

    public void GetDamageEffect(GameObject enemyGameObject) {
        enemyPlayer = enemyGameObject;
        animator = enemyPlayer.GetComponent<Animator>();
        movementController = enemyPlayer.GetComponent<MovementController>();
        var capsuleCollider = enemyPlayer.GetComponent<CapsuleCollider2D>();
        var rb = enemyPlayer.GetComponent<Rigidbody2D>();

        movementController.NumberOfTimeGetDamage++;
        animator.SetTrigger(AnimationString.takeDamage);

        if (movementController.NumberOfTimeGetDamage >= 5) {
            movementController.NumberOfTimeGetDamage = 0;
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("take_damage")
               && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) {
                if (capsuleCollider != null) {
                    capsuleCollider.enabled = false;
                }
                animator.SetBool(AnimationString.isStune, true);
            }
            StartCoroutine(WaitForHeal());
        }
    }

    IEnumerator WaitForHeal() {
        yield return new WaitForSeconds(1f);
        animator.SetBool(AnimationString.isStune, false);
        enemyPlayer.GetComponent<CapsuleCollider2D>().enabled = true;
    }

}
