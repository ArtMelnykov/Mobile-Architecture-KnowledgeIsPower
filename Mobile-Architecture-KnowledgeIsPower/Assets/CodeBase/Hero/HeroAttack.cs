using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {
        public HeroAnimator Animator;
        public CharacterController Controller;
        private IInputService _inputService;
        
        private Collider[] _hits = new Collider[3];
        private Stats _stats;

        private static int _layerMask;

        private void Awake()
        {
            _inputService = ServiceLocator.Container.Single<IInputService>();
            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Update()
        {
            if (_inputService.IsAttackButtonUp() && !Animator.IsAttacking) 
                Animator.PlayAttack();
        }

        private void OnAttack()
        {
            PhysicsDebug.DrawDebug(StartPoint() + transform.forward, _stats.DamageRadius, 1.0f);
            
            for (int i = 0; i < Hit(); i++)
            {
                _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_stats.Damage);
            }
        }

        private int Hit() => 
            Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _stats.DamageRadius, _hits, _layerMask);

        private Vector3 StartPoint() => 
            new Vector3(transform.position.x, Controller.center.y / 2, transform.position.z);

        public void LoadProgress(PlayerProgress playerProgress) => 
            _stats = playerProgress.HeroStats;
    }
}