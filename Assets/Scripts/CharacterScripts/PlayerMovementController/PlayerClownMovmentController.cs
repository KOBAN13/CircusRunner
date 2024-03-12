using System;
using Configs;
using InputSystem;
using UnityEngine;
using Zenject;

namespace Character.PlayerJumpController
{
    public class PlayerClownMovementController : PlayerBaseMovement, IMovable, ITickable
    {
        private bool _isPlayerClown;
        [Inject]
        public void Construct(PlayerComponents playerComponents)
        {
            PlayerComponents = playerComponents;
        }

        public void Init(СlownPlayerSettings configPlayer)
        {
            speed = configPlayer.MaxSpeed;
            _isPlayerClown = true;
        }

        public float Speed
        {
            get => speed;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Value must be a positive number", nameof(value));
                speed = value;
            }
        }
        private float _axis;

        public void Move(Swipe axis)
        {
            //нужно чуть перерабатывать инпут, времени уже нет
        }

        private void MoveForward()
        {
            var horizontal = Input.GetAxis("Horizontal");
            _axis = horizontal;
            TargetDirection.x = speed;
            TargetDirection.z = speed * -_axis;
            TargetDirection.y = PlayerComponents.TargetDirectionY;
            PlayerComponents.CharacterController.Move(TargetDirection * Time.deltaTime);
        }

        public void Tick()
        {
            if (!_isPlayerClown) return;
            MoveForward();
            Debug.Log(TargetDirection);
        }
    }
}