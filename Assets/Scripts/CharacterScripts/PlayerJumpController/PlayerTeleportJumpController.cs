using System;
using System.Collections;
using Character.Physics;
using Character.ReloadTeleport;
using Configs;
using UnityEngine;
using Zenject;

namespace Character.PlayerJumpController
{
    public class PlayerTeleportJumpController : PlayerBaseJump, IJumpable
    {
        public bool IsJump { get; set; }
        private ReloadTeleports _reloadTeleports;
        private TeleporterPlayerSettings _teleporterSettings;
        private CoroutineHelper _coroutineHelper;

        public void Init(TeleporterPlayerSettings teleporterSettings)
            => _teleporterSettings = teleporterSettings ? teleporterSettings : throw new ArgumentNullException();
        
        [Inject]
        public void Construct(ReloadTeleports reloadTeleports, CoroutineHelper coroutineHelper, PlayerComponents playerSettings, Gravity gravity)
        {
            _reloadTeleports = reloadTeleports;
            _coroutineHelper = coroutineHelper;
            PlayerComponents = playerSettings ? playerSettings : throw new ArgumentNullException();
            Gravity = gravity ?? throw new ArgumentNullException();
        }
        
        public async void Jump()
        {
            if (PlayerComponents.CharacterController.isGrounded)
            {
                var startPosition = PlayerComponents.Player.transform.position;
                var parametersJump = CalculatingTargetPosition(startPosition);

                _coroutineHelper.StartExternalCoroutine(TeleportUp(startPosition, parametersJump.jumpPosition, parametersJump.jumpTime));
                _coroutineHelper.StartExternalCoroutine(TeleportDown(startPosition, parametersJump.jumpPosition, parametersJump.jumpTime));

                await _reloadTeleports.Teleport();
            }
        }

        private IEnumerator TeleportDown(Vector3 startPosition, Vector3 parametersJumpPosition, float parametersJumpTime)
        {
            yield return new WaitForSeconds(_teleporterSettings.PlayerJumpTeleporter.HangTime);

            var time = 0f;

            while (time < parametersJumpTime)
            {
                PlayerComponents.Player.transform.position = Vector3.Lerp(startPosition, parametersJumpPosition, time / parametersJumpTime);

                time += Time.deltaTime;
            }
        }

        private IEnumerator TeleportUp(Vector3 startPosition, Vector3 parametersJumpPosition, float parametersJumpTime)
        {
            var time = 0f;

            while (time < _teleporterSettings.PlayerJumpTeleporter.JumpTime)
            {
                var targetPosition =
                    CalculateParabolicPosition(startPosition, parametersJumpPosition,
                        _teleporterSettings.PlayerJumpTeleporter.JumpHeight, 
                        _teleporterSettings.PlayerJumpTeleporter.JumpTime);

                PlayerComponents.Player.transform.position = targetPosition;
                time += Time.deltaTime;
                yield return null;
            }

            PlayerComponents.Player.transform.position = parametersJumpPosition;
        }

        private Vector3 CalculateParabolicPosition(Vector3 startPos, Vector3 endPos, float jumpHeight, float t)
        {
            Vector3 position = Vector3.Lerp(startPos, endPos, t);

            var yOffset = jumpHeight * Mathf.Sin(t * Mathf.PI);
            position.y += yOffset;

            return position;
        }
        
        private (Vector3 jumpPosition, float jumpTime) CalculatingTargetPosition(Vector3 startPosition)
        {
            var jumpUpPosition = startPosition + Vector3.up * _teleporterSettings.PlayerJumpTeleporter.JumpHeight;
            var jumpTime = _teleporterSettings.PlayerJumpTeleporter.JumpTime / 2;
            Gravity.GravityForce = 2 * _teleporterSettings.PlayerJumpTeleporter.JumpHeight / Mathf.Pow(jumpTime, 2);
            return (jumpUpPosition, jumpTime);
        }
    }
}