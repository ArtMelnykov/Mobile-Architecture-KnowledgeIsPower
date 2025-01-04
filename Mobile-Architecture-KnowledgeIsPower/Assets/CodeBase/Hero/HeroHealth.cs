using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
    {
        public HeroAnimator Animator;
        private State _state;
        
        public float Max
        {
            get => _state.MaxHp;
            set => _state.MaxHp = value;
        }

        public event Action HealthChanged;

        public float Current
        {
            get => _state.CurrentHp;
            set
            {
                if (_state.CurrentHp != value)
                {
                    _state.CurrentHp = value;
                    HealthChanged?.Invoke();
                }
            }
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            _state = playerProgress.HeroState;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            playerProgress.HeroState.CurrentHp = Current;
            playerProgress.HeroState.MaxHp = Max;
        }

        public void TakeDamage(float damage)
        {
            if (Current <= 0)
                return;
            
            Current -= damage;
            Animator.PlayHit();
        }
    }
}