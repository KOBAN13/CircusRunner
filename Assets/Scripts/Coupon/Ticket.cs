using System;
using Character.Collisions;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Coupon
{
    public class Ticket : MonoBehaviour
    {
        private CollisionHandler _collisionHandler;
        private Sequence _sequence;
        private Tween _tween;
        private bool _isActive = true;

        [Inject]
        public void Construct(CollisionHandler collisionHandler)
        {
            _collisionHandler = collisionHandler;
        }

        public void OnTriggerEnter(Collider other)
        {
            _collisionHandler.NotifyCouponCollision();
            gameObject.SetActive(false);
        }
    }
}