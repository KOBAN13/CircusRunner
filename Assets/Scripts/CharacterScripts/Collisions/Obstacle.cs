using Configs;
using UnityEngine;
using Zenject;

namespace Character.Collisions
{
    public class Obstacle : MonoBehaviour
    {
        private CollisionHandler _collisionHandler;

        [Inject]
        public void Construct(CollisionHandler collisionHandler)
        {
            _collisionHandler = collisionHandler;
        }
        
        public void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<IUseConfigable>(out var config)) 
                _collisionHandler.NotifyObstacleCollision(config);
            
            gameObject.SetActive(false);
        }
    }
}