using System;
using Character.Physics;
using UnityEngine;
using Zenject;

namespace Character.PlayerJumpController
{
    public class PlayerJumpController : MonoBehaviour, IJumpable
    {
        [field: Header("Jump Settings")] 
        [field: SerializeField] public float JumpTime { get; private set; }
        [field: SerializeField] public float JumpHeight { get; private set; }

        public bool IsJump
        {
            get => _isJump;
            set => _isJump = value;
        }
        
        private float _jumpVelocity;
        private IPlayerSettings _playerSettings;
        private Gravity _gravity;
        private bool _isJump = true;

        [Inject]
        public void Construct(IPlayerSettings playerMovementController, Gravity gravity)
        {
            _playerSettings = playerMovementController ?? throw new ArgumentNullException($"{nameof(playerMovementController)} is null");

            _gravity = gravity ?? throw new ArgumentNullException($"{nameof(gravity)} is null");
        }

        private void Start()
        {
            var maxHeightTime = JumpTime / 2;
            _gravity.GravityForce = 2 * JumpHeight / Mathf.Pow(maxHeightTime, 2);
            _jumpVelocity = 2 * JumpHeight / maxHeightTime;
        }

        public void Jump()
        {
            if (_playerSettings.CharacterController.isGrounded)
            {
                _playerSettings.TargetDirectionY = _jumpVelocity;
            }
        }
    }
}