using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Services.Input;
using UnityEngine;

namespace Assets.CodeBase.Hero
{
    public class HeroMove : MonoBehaviour
    {
        public CharacterController CharacterController;
        public float MovementSpeed;

        private IInputService _inputService;
        private Camera _camera;

        void Awake()
        {
            _inputService = ServiceLocator.Container.Single<IInputService>();
        }

        void Start()
        {
            _camera = Camera.main;
        }

        void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0f;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            CharacterController.Move(movementVector * Time.deltaTime * MovementSpeed);
        }
    }
}