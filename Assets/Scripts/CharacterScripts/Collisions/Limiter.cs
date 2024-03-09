using Configs;
using UnityEngine;
using Zenject;

namespace Character.Collisions
{
    public class Limiter : MonoBehaviour
    {
        private CollisionHandler _collisionHandler;

        [Inject]
        public void Construct(CollisionHandler collisionHandler)
        {
            _collisionHandler = collisionHandler;
        }
        
        public void OnTriggerEnter(Collider other)
        {
            _collisionHandler.HandleLimiterCollision(this);
        }
    }
}