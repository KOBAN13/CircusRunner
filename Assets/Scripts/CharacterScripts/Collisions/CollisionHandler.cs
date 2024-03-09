using System;
using System.Collections;
using Configs;
using CreateCoupon;
using Ui;
using Ui.UiInterface;
using UnityEngine;
using Zenject;

namespace Character.Collisions
{
    public class CollisionHandler
    {
        private CoroutineHelper _coroutineHelper;
        private IStopMovable _stopMovable;
        private ITicket _ticketCount;
        private ILimiter _limiter;
        
        public event Action<IUseConfigable> OnObstacleCollision;
        public event Action OnCouponCollision;
        public event Action<Limiter> OnLimiterCollision;

        [Inject]
        public void Construct(CoroutineHelper coroutineHelper, IStopMovable stopMovable, ITicket ticketCount, ILimiter limiter)
        {
            _coroutineHelper = coroutineHelper;
            _stopMovable = stopMovable;
            _ticketCount = ticketCount;
            _limiter = limiter;
        }
        
        public void NotifyObstacleCollision(IUseConfigable config)
        {
            OnObstacleCollision?.Invoke(config);
        }

        public void NotifyCouponCollision()
        {
            OnCouponCollision?.Invoke();
        }
        
        public void NotifyLimiterCollision(Limiter limiter)
        {
            OnLimiterCollision?.Invoke(limiter);
        }

        public void HandleObstacleCollision(IUseConfigable config)
        {
            _coroutineHelper.StartExternalCoroutine(HittingObstacle(config));
        }
        
        public void HandleCouponCollision()
        {
            _ticketCount.CountTicket += 1;
        }

        public void HandleLimiterCollision(Limiter limiter)
        {
            _limiter.HandlerLimiter(limiter);
        }

        private IEnumerator HittingObstacle(IUseConfigable config)
        {
            _stopMovable.OnSubcribeEvent();
            _stopMovable.InvokeEventStopMovements(config);
            yield return new WaitForSeconds(config.ConfigPlayer.RecoveryTimeAfterCollision);
            
            _stopMovable.Dispose();
        }
    }
}