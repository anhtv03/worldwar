using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDmg : MonoBehaviour, ITakeDame
{
    // Start is called before the first frame update
    Animator animator;
    MovementController movementController;
    GameObject enemyPlayer;

    public void GetDamageEffect(GameObject enemyGameObject)
    {
        enemyPlayer = enemyGameObject;
        animator = enemyPlayer.GetComponent<Animator>();
        movementController = enemyPlayer.GetComponent<MovementController>();
        var capsuleCollider = enemyPlayer.GetComponent<CapsuleCollider2D>();
        var rb = enemyPlayer.GetComponent<Rigidbody2D>();

        movementController.NumberOfTimeGetDamage++;
        animator.SetTrigger(AnimationString.takeDamage);
    }
  
}
