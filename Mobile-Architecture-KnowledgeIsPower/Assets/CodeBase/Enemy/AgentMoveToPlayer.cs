using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace CodeBase.Enemy
{
    public class AgentMoveToPlayer : MonoBehaviour
    {
        private const float MinimalDistance = 1f;
        
        [FormerlySerializedAs("agent")] 
        public NavMeshAgent Agent;
        private Transform _heroTransform;
        private IGameFactory _gameFactory;
        
        private void Start()
        {
            _gameFactory = ServiceLocator.Container.Single<IGameFactory>();
            
            if (_gameFactory.HeroGameObject != null)
            {
                InitializeHeroTransform();
            }
            else
            {
                _gameFactory.HeroCreated += HeroCreated;
            }
        }

        private void Update()
        {
            if (IsHeroNotReached() && IsInitialized()) 
                Agent.destination = _heroTransform.position;
        }

        private bool IsInitialized() => 
            _heroTransform != null;

        private void HeroCreated() => 
            InitializeHeroTransform();

        private void InitializeHeroTransform() => 
            _heroTransform = _gameFactory.HeroGameObject.transform;

        private bool IsHeroNotReached() => 
            Agent.transform.position.SqrMagnitudeTo(_heroTransform.position) >= MinimalDistance;
    }
}