using System;
using Character.Physics;
using Character.PlayerChoise;
using UnityEngine;
using Zenject;

namespace Character.PlayerJumpController
{
    public class PlayerClownJumpController : PlayerBaseJump, IJumpable, IInit
    {
        public bool IsJump { get; set; } = true;

        private float _jumpVelocity;
        private СlownPlayerSettings _clownSettings;

        [Inject]
        public void Inject(PlayerComponents playerSettings, Gravity gravity)
        {
            PlayerComponents = playerSettings ? playerSettings : throw new ArgumentNullException();
            Gravity = gravity ?? throw new ArgumentNullException();
        }

        public void Init<T>(T playerSettings)
        {
            _clownSettings = playerSettings as СlownPlayerSettings;
            if (_clownSettings == null) return;
            
            var maxHeightTime = _clownSettings.PlayerClownJump.JumpTime / 2;
            Gravity.GravityForce = 2 * _clownSettings.PlayerClownJump.JumpHeight / Mathf.Pow(maxHeightTime, 2);
            _jumpVelocity = 2 * _clownSettings.PlayerClownJump.JumpHeight / maxHeightTime;
        }

        public void Jump()
        {
            if (PlayerComponents.CharacterController.isGrounded)
            {
                PlayerComponents.TargetDirectionY = _jumpVelocity;
            }
        }
    }
}