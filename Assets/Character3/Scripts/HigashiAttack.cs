using Assets.Character3.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HigashiAttack : MonoBehaviour
{
    [SerializeField]public GameObject attackCollider1;
    [SerializeField] public GameObject attackCollider2;
    [SerializeField]public GameObject attackCollider3;
    [SerializeField]public GameObject attackCollider4;

    [SerializeField] private float jumpInput = 5f;

    private int jumpCount;
    private int maxJumps = 2;

    
    private Animator animator;


    TouchingDirections touchingDirections;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void OnAttack()
    {
        int randomAttack = Random.Range(1, 5);
        animator.SetInteger("AttackIndex", randomAttack);
        animator.SetTrigger("IsAttack");

        EnableAttackCollider(randomAttack);

        // Khởi chạy coroutine để đợi hoạt ảnh tấn công hoàn thành và mở khóa di chuyển
        StartCoroutine(EndAttack(randomAttack));

    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        Health playerHealth = collision.gameObject.GetComponent<Health>();
    //        Mana mana = gameObject.GetComponentInParent<Mana>();

    //        if (playerHealth != null)
    //        {
    //            playerHealth.TakeDamage(50);
    //            if (mana != null)
    //            {
    //                mana.GetMana(5);
    //            }
    //        }
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collision) {
    //    if (collision.gameObject.CompareTag("Player")) {
    //        Health playerHealth = collision.gameObject.GetComponent<Health>();
    //        Mana mana = gameObject.GetComponentInParent<Mana>();

    //        if (playerHealth != null) {
    //            playerHealth.TakeDamage(50);
    //            if (mana != null) {
    //                mana.GetMana(5);
    //            }
    //        }
    //    }
    //}

    private void EnableAttackCollider(int attackIndex)
    {
        // Bật collider tương ứng với đòn tấn công hiện tại
        switch (attackIndex)
        {
            case 1:
                attackCollider1.SetActive(true);
                break;
            case 2:
                attackCollider2.SetActive(true);
                break;
            case 3:
                attackCollider3.SetActive(true);
                break;
            case 4:
                attackCollider4.SetActive(true);
                break;
        }
    }
    private IEnumerator EndAttack(int attackIndex)
    {

        yield return new WaitForSeconds(0.6f);
        switch (attackIndex)
        {
            case 1:
                attackCollider1.SetActive(false);
                break;
            case 2:
                attackCollider2.SetActive(false);
                break;
            case 3:
                attackCollider3.SetActive(false);
                break;
            case 4:
                attackCollider4.SetActive(false);
                break;
        }
    }

}
