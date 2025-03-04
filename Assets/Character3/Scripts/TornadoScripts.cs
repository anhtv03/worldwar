using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Character3.Scripts
{
    public class TornadoScripts : MonoBehaviour
    {
        [SerializeField] 
        float lifeTimes = 1.3f;
        public int damageAmount;
        void Start()
        {
            Destroy(gameObject, lifeTimes);
        }

        public float LifeTime()
        {
            return lifeTimes;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Health playerHealth = collision.gameObject.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);
                }
            }
        }

    }
}