﻿using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        [Header("Parameters")]
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int Win = Animator.StringToHash("Win");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Attack = Animator.StringToHash("Attack_1");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int Speed = Animator.StringToHash("Speed");
        
        [Header("States")]
        private readonly int _idleStateHash = Animator.StringToHash("idle");
        private readonly int _attackStateHash = Animator.StringToHash("attack01");
        private readonly int _movingStateHash = Animator.StringToHash("Move");
        private readonly int _deathStateHash = Animator.StringToHash("die");
        
        private Animator _animator;

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public AnimatorState State { get; private set; }

        private void Awake() => 
            _animator = GetComponent<Animator>();

        public void PlayHit() => _animator.SetTrigger(Hit);

        public void PlayDeath() => _animator.SetTrigger(Die);

        public void MoveEnemy(float speed)
        {
            _animator.SetBool(IsMoving, true);
            _animator.SetFloat(Speed, speed);
        }

        public void StopMoving() => _animator.SetBool(IsMoving, false);

        public void PlayAttack() => _animator.SetTrigger(Attack);

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash) => StateExited?.Invoke(StateFor(stateHash));

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;

            if (stateHash == _idleStateHash)
            {
                state = AnimatorState.Idle;
            }
            else if (stateHash == _attackStateHash)
            {
                state = AnimatorState.Attack;
            }
            else if (stateHash == _movingStateHash)
            {
                state = AnimatorState.Walking;
            }
            else if (stateHash == _deathStateHash)
            {
                state = AnimatorState.Died;
            }
            else
            {
                state = AnimatorState.Unknown;
            }
            
            return state; 
        }
    }
}