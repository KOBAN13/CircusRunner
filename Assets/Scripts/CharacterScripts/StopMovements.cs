using System;
using System.Collections;
using Character.PlayerJumpController;
using UnityEngine;
using Zenject;

namespace Character
{
    public class StopMovements : IStopMovable
    {
        private IMovement _movement;

        private event Action<IUseConfigable> StopMove;
        private CoroutineHelper _coroutineHelper;
        
        [Inject]
        public void Construct(IMovement movement, CoroutineHelper coroutineHelper)
        {
            _movement = movement ?? throw new ArgumentNullException($"{nameof(movement)} is null fix this");
            _coroutineHelper = coroutineHelper ?? throw new ArgumentNullException($"{nameof(coroutineHelper)} is null fix this");
        }
        
        public void OnSubcribeEvent() => StopMove += StopMovementsForDuration;
        
        public void Dispose()
        {
            StopMove -= StopMovementsForDuration;
        }
        
        public void InvokeEventStopMovements(IUseConfigable config) => StopMove?.Invoke(config);
        
        private void StopMovementsForDuration(IUseConfigable config)
        {
            _movement.Jumpable.IsJump = false;
            _coroutineHelper.StartExternalCoroutine(ResumeJumpAfterDelay(config.Config.RecoveryTimeAfterCollision));
            _coroutineHelper.StartExternalCoroutine(InterpolateSpeed(config));
        }

        private IEnumerator ResumeJumpAfterDelay(float duration)
        {
            yield return new WaitForSeconds(duration);
            _movement.Jumpable.IsJump = true;
        }

        private IEnumerator InterpolateSpeed(IUseConfigable config)
        {
            var elapsedTime = 0f;
            var speedAfterCollision = config.Config.MaxSpeed / 4;
            
            while (elapsedTime < config.Config.RecoveryTimeAfterCollision)
            {
                _movement.Movable.Speed = Mathf.Lerp(speedAfterCollision, config.Config.MaxSpeed,
                    elapsedTime / config.Config.RecoveryTimeAfterCollision);

                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}