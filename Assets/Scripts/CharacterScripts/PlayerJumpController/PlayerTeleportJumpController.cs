using System;
using System.Collections;
using Character.Physics;
using Character.PlayerChoise;
using Character.ReloadTeleport;
using Configs;
using UnityEngine;
using Zenject;

namespace Character.PlayerJumpController
{
    public class PlayerTeleportJumpController : PlayerBaseJump, IJumpable, IInit
    {
        public bool IsJump { get; set; }
        private ReloadTeleports _reloadTeleports;
        private TeleporterPlayerSettings _teleporterSettings;

        public void Init<T>(T playerSettings)
        {
            _teleporterSettings = playerSettings as TeleporterPlayerSettings;
        }
        
        [Inject]
        public void Construct(ReloadTeleports reloadTeleports, PlayerComponents playerSettings, Gravity gravity)
        {
            _reloadTeleports = reloadTeleports;
            PlayerComponents = playerSettings ? playerSettings : throw new ArgumentNullException();
            Gravity = gravity ?? throw new ArgumentNullException();
        }
        
        public void Jump()
        {
            if (PlayerComponents.CharacterController.isGrounded)  
            {
                if(_reloadTeleports.KeyPressing) return;
                _reloadTeleports.KeyDelay();

                PlayerComponents.TargetDirectionY = CalculatingTargetPosition();
            }
        }

        private float CalculatingTargetPosition()
        {
            return  2 * _teleporterSettings.PlayerJumpTeleporter.JumpHeight / SetGravityForce();;
        }

        private float SetGravityForce()
        {
            var maxHeightTime = _teleporterSettings.PlayerJumpTeleporter.JumpTime / 2;
            Gravity.GravityForce = 2 * _teleporterSettings.PlayerJumpTeleporter.JumpHeight / Mathf.Pow(maxHeightTime, 2);
            return maxHeightTime;
        }

    }
}