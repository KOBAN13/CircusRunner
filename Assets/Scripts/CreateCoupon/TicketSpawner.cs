using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Character.Collisions;
using Character.Loader;
using Configs;
using Coupon;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace CreateCoupon
{
    public class TicketSpawner : MonoBehaviour, ILimiter
    {
        [field: Header("Spawn Points")]
        [field: SerializeField] public List<Transform> SpawnPointFirst { get; private set; }
        [field: SerializeField] public List<Transform> SpawnPointSecond { get; private set; }
        [field: SerializeField] public List<Transform> SpawnPointThird { get; private set; }
        [field: SerializeField] public List<Limiter> LimiterSpawnTicket { get; private set; }
        
        [field: Header("Coupon Spawn Settings")]
        [field: SerializeField] public TicketConfig TicketConfig { get; private set; }

        private List<Ticket> _tickets = new();
        private CouponFactory _couponFactory;
        private Dictionary<Limiter, List<Transform>> _limiterZone = new();

        [Inject]
        public void Construct(PoolObject<Ticket> poolObject, AddressableLoader loader)
        {
            _couponFactory = new CouponFactory(poolObject, TicketConfig, loader);
        }

        private void Awake()
        {
            StartCoroutine(WaitLoadCoupon());
            
            _limiterZone.Add(LimiterSpawnTicket[0], SpawnPointSecond);
            _limiterZone.Add(LimiterSpawnTicket[1], SpawnPointThird);
        }
        
        public void HandlerLimiter(Limiter limiter)
        {
            _tickets
                .ForEach(x => x.gameObject.SetActive(false));
            SpawnTicket(_limiterZone
                .SingleOrDefault(x => x.Key == limiter).Value);
        }

        private void SpawnTicket(List<Transform> spawnPoint)
        {
            spawnPoint
                .Select(position => MoveCouponToSpawnPoint(_couponFactory
                    .GetCoupon(TicketConfig.KeyForGetCoupon), position.position))
                .ToList()
                .ForEach(coupon => _tickets.Add(coupon));
        }
        
        private IEnumerator WaitLoadCoupon()
        {
            yield return new WaitForSeconds(0.5f);
            
            SpawnTicket(SpawnPointFirst);
        }

        private Ticket MoveCouponToSpawnPoint(Ticket component, Vector3 target)
        {
            component.transform.position = target;
            return component;
        }
    }
    
    
}