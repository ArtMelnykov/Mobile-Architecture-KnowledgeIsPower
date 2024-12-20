using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class RotateToHero : Follow
    {
        [SerializeField]
        private float _speed;

        private Transform _heroTransform;
        private IGameFactory _gameFactory;
        private Vector3 _positionToLookAt;

        private void Start()
        {
            _gameFactory = ServiceLocator.Container.Single<IGameFactory>();


            if (HeroExists())
            {
                InitializeHeroTransform();
            }
            else
            {
                _gameFactory.HeroCreated += InitializeHeroTransform;
            }
        }

        private void Update()
        {
            if (Initialized())
            {
                RotateTowardsHero();
            }
        }

        private void RotateTowardsHero()
        {
            UpdatePositionToLookAt();

            transform.rotation = SmoothedRotation(transform.rotation, _positionToLookAt);
        }

        private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLookAt) => 
            Quaternion.Lerp(rotation, TargetRotation(positionToLookAt), SpeedFactor());

        private static Quaternion TargetRotation(Vector3 positionToLookAt) => 
            Quaternion.LookRotation(positionToLookAt);

        private float SpeedFactor() => 
            _speed * Time.deltaTime;

        private void UpdatePositionToLookAt()
        {   
            Vector3 positionDiff = _heroTransform.position - transform.position;
            _positionToLookAt = new Vector3(positionDiff.x, transform.position.y, positionDiff.z);
        }

        private void InitializeHeroTransform() => 
            _heroTransform = _gameFactory.HeroGameObject.transform;

        private bool Initialized() => 
            _heroTransform != null;

        private bool HeroExists() => 
            _gameFactory.HeroGameObject != null;
    }
}