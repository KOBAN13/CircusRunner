using System;
using System.Collections;
using Character.PlayerJumpController;
using Configs;
using UnityEngine;
using Zenject;

namespace Character
{
    public class StopMovements : IStopMovable
    {
        private IMovable _movable;
        private IJumpable _jumpable;
        
        private event Action<IUseConfigable> StopMove;
        private CoroutineHelper _coroutineHelper;
        
        [Inject]
        public void Construct(IMovable move, IJumpable jump, CoroutineHelper coroutineHelper)
        {
            _movable = move ?? throw new ArgumentNullException($"{nameof(move)} is null fix this");
            _jumpable = jump ?? throw new ArgumentNullException($"{nameof(jump)} is null fix this");
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
            _jumpable.IsJump = false;
            _coroutineHelper.StartExternalCoroutine(ResumeJumpAfterDelay(config.ConfigPlayer.RecoveryTimeAfterCollision));
            _coroutineHelper.StartExternalCoroutine(InterpolateSpeed(config));
        }

        private IEnumerator ResumeJumpAfterDelay(float duration)
        {
            yield return new WaitForSeconds(duration);
            _jumpable.IsJump = true;
        }

        private IEnumerator InterpolateSpeed(IUseConfigable config)
        {
            var elapsedTime = 0f;
            var speedAfterCollision = config.ConfigPlayer.MaxSpeed / 4;
            
            while (elapsedTime < config.ConfigPlayer.RecoveryTimeAfterCollision)
            {
                _movable.Speed = Mathf.Lerp(speedAfterCollision, config.ConfigPlayer.MaxSpeed,
                    elapsedTime / config.ConfigPlayer.RecoveryTimeAfterCollision);

                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}