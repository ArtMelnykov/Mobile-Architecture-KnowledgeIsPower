using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator), typeof(EnemyHealth))]
    public class EnemyDeath : MonoBehaviour
    {
        public EnemyAnimator Animator;
        public EnemyHealth Health;
        
        public GameObject DeathFX;
        
        public event Action Happened;

        private void Start() => 
            Health.HealthChanged += HealthChanged;

        private void OnDestroy() => 
            Health.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (Health.Current <= 0) 
                Die();
        }

        private void Die()
        {
            Health.HealthChanged -= HealthChanged;
            Animator.PlayDeath();
            
            Instantiate(DeathFX, transform.position, Quaternion.identity);
            StartCoroutine(DestroyTimer());
            
            Happened?.Invoke();
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }
    }
}