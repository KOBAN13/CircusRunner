using System;
using Character.Loader;
using Configs;
using Coupon;
using UnityEngine;

namespace CreateCoupon
{
    public class CouponFactory
    {
        private PoolObject<Ticket> _poolObject;
        private GameObject _coupon;
        private TicketConfig _ticketConfig;
        private AddressableLoader _addressableLoader;

        public CouponFactory(PoolObject<Ticket> poolObject, TicketConfig ticketConfig, AddressableLoader loader)
        {
            _ticketConfig = ticketConfig ? ticketConfig : throw new ArgumentNullException($"{nameof(ticketConfig)} is null fix this");
            
            _poolObject = poolObject ?? throw new ArgumentNullException($"{nameof(poolObject)} is null fix this");
            _addressableLoader = loader ?? throw new ArgumentNullException($"{nameof(loader)} is null fix this");
            
            InitPrefab(_ticketConfig.PathLoadPrefab);
        }

        public Ticket GetCoupon(string key)
        {
            return _poolObject.GetElementInPool(key);
        }

        private async void InitPrefab(string path)
        {
            _coupon = await _addressableLoader.Loader<GameObject>(path);
            
            _poolObject.AddElementsInPool(_ticketConfig.KeyForGetCoupon, _coupon, _ticketConfig.CountCouponInScene);
        }
    }
}