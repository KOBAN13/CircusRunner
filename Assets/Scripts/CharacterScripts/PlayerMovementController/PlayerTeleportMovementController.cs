using System;
using System.Collections;
using Character.PlayerChoise;
using Character.ReloadTeleport;
using Configs;
using InputSystem;
using UnityEngine;
using Zenject;

namespace Character.PlayerJumpController
{
    public class PlayerTeleportMovementController : PlayerBaseMovement, IMovable, ITickable, IInit
    {
        private CoroutineHelper _coroutineHelper;
        private TeleporterPlayerSettings _playerSettings;
        private ReloadTeleports _reloadTeleports;
        private bool _isPlayerTeleporter;
        private Vector3 _targetPosition;

        [Inject]
        public void Construct(PlayerComponents playerComponents, CoroutineHelper coroutineHelper, ReloadTeleports reloadTeleports)
        {
            PlayerComponents = playerComponents;
            _coroutineHelper = coroutineHelper;
            _reloadTeleports = reloadTeleports;
        }

        public void Init<T>(T playerSettings)
        {
            _playerSettings = playerSettings as TeleporterPlayerSettings;
            _isPlayerTeleporter = true;
            if (_playerSettings == null) throw new ArgumentNullException(nameof(playerSettings));
            speed = _playerSettings.MaxSpeed;
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
        private int _lineToMove = 1;
        
        public void Tick()
        {
            MoveForward();
        }

        public void Move(Swipe axis)
        {
            if(_reloadTeleports.KeyPressing) return;
            _reloadTeleports.KeyDelay();
            
            switch (axis)
            {
                case Swipe.Right:
                {
                    if (_lineToMove < 2)
                        _lineToMove++;
                    break;
                }
                case Swipe.Left:
                {
                    if (_lineToMove > 0)
                        _lineToMove--;
                    break;
                }
            }
            
            _coroutineHelper.StartExternalCoroutine(MoveObjectCoroutine());
        }

        private void MoveForward()
        {
            if(!_isPlayerTeleporter) return;
            TargetDirection.y = PlayerComponents.TargetDirectionY;
            TargetDirection.x = speed;
            PlayerComponents.CharacterController.Move(TargetDirection * Time.deltaTime);
        }

        private IEnumerator MoveObjectCoroutine()
        {
            PlayerComponents.Player.gameObject.SetActive(false);
            Vector3 startPosition = PlayerComponents.Player.transform.position;
            _targetPosition = new Vector3(PlayerComponents.Player.transform.position.x, PlayerComponents.Player.transform.position.y, 
                _playerSettings.FirstPosition + _lineToMove * _playerSettings.LineDistance);

            var startTime = Time.time;
            var elapsedTime = 0f;

            while (elapsedTime < 0.1f)
            {
                elapsedTime = Time.time - startTime;
                var t = Mathf.Clamp01(elapsedTime / 0.1f);

                PlayerComponents.Player.transform.position = Vector3.Lerp(startPosition, _targetPosition, t);
                
                TargetDirection.x = speed;
                yield return null;
            } 
            
            PlayerComponents.Player.transform.position = _targetPosition;
            PlayerComponents.Player.gameObject.SetActive(true);
        }
    }
}