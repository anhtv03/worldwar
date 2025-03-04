using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject : MonoBehaviour
{
    [SerializeField] float kachousenLifeTime = 1.3f;
    [SerializeField] float sayoChidoriLifeTime = 0.6f;
    [SerializeField] float ryuuenbuLifeTime = 1.3f;
    public bool isKachousen;
    public int damageAmount = 10;

    public float GetKachousenLifteTime()
    {
        return kachousenLifeTime;
    }
    public float GetSayoChidoriLifteTime()
    {
        return sayoChidoriLifeTime;
    }
    public float GetRyuuenbuLifteTime()
    {
        return ryuuenbuLifeTime;
    }


    // Destroy Object
    public void DestroySkillObject(GameObject skillObject, float lifeTime)
    {
        if (skillObject != null)
        {
            Destroy(skillObject, lifeTime);
        }
    }

    // Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
            var hp = collision.gameObject.GetComponent<Health>();
            if (hp.Hp <= 0)
            {
                var animation = collision.gameObject.GetComponent<Animator>();
                animation.SetTrigger("die");
            }
            if (isKachousen)
            {
                StartCoroutine(DestroyAfterDelay(0.7f));
            }
        }
    }
    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
