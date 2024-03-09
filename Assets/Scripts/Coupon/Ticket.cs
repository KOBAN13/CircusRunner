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

        [Inject]
        public void Construct(CollisionHandler collisionHandler)
        {
            _collisionHandler = collisionHandler;
        }

        private void Start() => AnimationCoupon();

        private void AnimationCoupon()
        {
            _sequence = DOTween.Sequence();

            var eulerAngles = transform.localEulerAngles;
            var position = transform.localPosition;
            transform.DORotate(new Vector3(eulerAngles.x, eulerAngles.y + 360f,  eulerAngles.z), 3f, RotateMode.LocalAxisAdd).SetLoops(-1);
            _sequence.Append(transform.DOMove(new Vector3(position.x, position.y + 1.4f, position.z), 2f));
            _sequence.Append(transform.DOMove(new Vector3(position.x, position.y - 1.4f, position.z), 2f));
            _sequence.SetLoops(-1);
        }
        

        public void OnTriggerEnter(Collider other)
        {
            _collisionHandler.NotifyCouponCollision();
            
            gameObject.SetActive(false);
        }
    }
}