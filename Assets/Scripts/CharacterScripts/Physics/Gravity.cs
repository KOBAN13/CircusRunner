using System;
using UnityEngine;
using Zenject;

namespace Character.Physics
{
    public class Gravity : ITickable
    {
        private float _gravityForce = 9.8f;
        private PlayerComponents _playerComponents;

        [Inject]
        public void Construct(PlayerComponents playerComponents) => _playerComponents = playerComponents;

        public void Tick()
        {
            GravityHandling();
        }
        
        public float GravityForce
        {
            set
            {
                if (value >= 0)
                    _gravityForce = value;
            }
        }

        private void GravityHandling()
        {
            if (!_playerComponents.CharacterController.isGrounded)
            {
                _playerComponents.TargetDirectionY -= _gravityForce * Time.deltaTime;
            }
        }
    }
}