using System;
using UnityEngine;
using Zenject;

namespace Character.Physics
{
    public class Gravity : ITickable
    {
        private float _gravityForce = 9.8f;
        private IPlayerSettings _playerSettings;

        [Inject]
        public void Construct(IPlayerSettings playerSettings)
        {
            _playerSettings = playerSettings ?? throw new ArgumentNullException($"{nameof(playerSettings)}");
        }
        
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
            if (!_playerSettings.CharacterController.isGrounded)
            {
                _playerSettings.TargetDirectionY -= _gravityForce * Time.deltaTime;
            }
        }
    }
}