using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageHandler : MonoBehaviour, ITakeDame
{
    Animator animator;
    PlayerController playerController;
    GameObject enemyPlayer;
    AudioClipController audioClipController;
    bool isFly = false;
    float time = 0;
    public void GetDamageEffect(GameObject enemy)
    {
        enemyPlayer = enemy;
        animator = enemyPlayer.GetComponent<Animator>();
        playerController = enemyPlayer.GetComponent<PlayerController>();
        var boxCollider = enemy.GetComponent<BoxCollider2D>();
        var capsuleCollider = enemy.GetComponent<CapsuleCollider2D>();
        var rb = enemy.GetComponent<Rigidbody2D>();
        audioClipController.TakeDamageClip();
        playerController.NumberOfTimeGetDamage++;
        animator.SetTrigger(AnimationString.takeDamage);

        if (playerController.NumberOfTimeGetDamage >= 10)
        {
            playerController.NumberOfTimeGetDamage = 0;
            if (boxCollider != null)
            {
               // boxCollider.enabled = false;
            }
            if (capsuleCollider != null)
            {
             //   capsuleCollider.enabled = false;
            }
            animator.SetBool(AnimationString.isStune, true);              
            StartCoroutine(WaitForHeal());
        }
    }

    void Start()
    {
        audioClipController = GetComponent<AudioClipController>();
    }
    IEnumerator WaitForHeal()
    {
        yield return new WaitForSeconds(1f);        
        animator.SetBool(AnimationString.isStune, false);
        animator.SetTrigger(AnimationString.standUp);
        enemyPlayer.GetComponent<BoxCollider2D>().enabled = true;
        enemyPlayer.GetComponent<CapsuleCollider2D>().enabled = true;

    }
    
}
