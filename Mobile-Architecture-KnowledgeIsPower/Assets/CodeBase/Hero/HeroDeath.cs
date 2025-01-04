using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroHealth))]
    public class HeroDeath : MonoBehaviour
    {
        [Header("Dependencies")]
        public HeroHealth HeroHealth;
        public HeroMove Move;
        public HeroAnimator Animator;
        public GameObject DeathFX;
        public HeroAttack HeroAttack;

        public bool IsDead;

        private void Start() => 
            HeroHealth.HealthChanged += HealthChanged;

        private void OnDestroy() => 
            HeroHealth.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (!IsDead && HeroHealth.Current <= 0) 
                Die();
        }

        private void Die()
        {
            IsDead = true;
            Move.enabled = false;
            HeroAttack.enabled = false;
            Animator.PlayDeath();
            
            Instantiate(DeathFX, transform.position, Quaternion.identity);
        }
    }
}