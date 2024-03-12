using System;
using System.Collections;
using System.Collections.Generic;
using Configs;
using InputSystem;
using UnityEngine;
using Zenject;
using Character.ReloadTeleport;

namespace Character.PlayerJumpController
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerTeleportMovementController : PlayerBaseMovement, IMovable, ITickable
    {
        private CoroutineHelper _reloadTeleports;
        private TeleporterPlayerSettings _playerSettings;
        private bool _isPlayerTeleporter;
        private Vector3 targetPosition;

        [Inject]
        public void Construct(PlayerComponents playerComponents, CoroutineHelper reloadTeleports)
        {
            PlayerComponents = playerComponents;
            _reloadTeleports = reloadTeleports;
        }

        public void Init(TeleporterPlayerSettings playerSettings)
        {
            _playerSettings = playerSettings;
            _isPlayerTeleporter = true;
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
            
            _reloadTeleports.StartExternalCoroutine(MoveObjectCoroutine());
        }

        private void MoveForward()
        {
            if(!_isPlayerTeleporter) return;
            TargetDirection.x = speed;
            PlayerComponents.CharacterController.Move(TargetDirection * Time.deltaTime);
        }

        private IEnumerator MoveObjectCoroutine()
        {
            PlayerComponents.Player.gameObject.SetActive(false);
            Vector3 startPosition = PlayerComponents.Player.transform.position;
            targetPosition = new Vector3(PlayerComponents.Player.transform.position.x, PlayerComponents.Player.transform.position.y, _playerSettings.FirstPosition + _lineToMove * _playerSettings.LineDistance);

            float startTime = Time.time;
            float elapsedTime = 0f;

            while (elapsedTime < 0.1f)
            {
                elapsedTime = Time.time - startTime;
                float t = Mathf.Clamp01(elapsedTime / 0.1f);

                PlayerComponents.Player.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
                
                TargetDirection.x = speed;
                yield return null;
            }

            // Убедитесь что позиция на выходе точно соответствует целевой
            PlayerComponents.Player.transform.position = targetPosition;
            PlayerComponents.Player.gameObject.SetActive(true);
        }
        
    }
}