using System;
using System.Threading.Tasks;
using Configs;
using Coupon;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CreateCoupon
{
    public class CouponFactory
    {
        private PoolObject<Ticket> _poolObject;
        private GameObject _coupon;
        private TicketConfig _ticketConfig;

        public CouponFactory(PoolObject<Ticket> poolObject, TicketConfig ticketConfig)
        {
            _ticketConfig = ticketConfig ? ticketConfig : throw new ArgumentNullException($"{nameof(poolObject)} is null fix this");
            
            _poolObject = poolObject ?? throw new ArgumentNullException($"{nameof(poolObject)} is null fix this");
            
            InitPrefab(_ticketConfig.PathLoadPrefab);
        }

        public Ticket GetCoupon(string key)
        {
            return _poolObject.GetElementInPool(key);
        }

        private async void InitPrefab(string path)
        {
            _coupon = await LoadCoupon(path);
            
            _poolObject.AddElementsInPool(_ticketConfig.KeyForGetCoupon, _coupon, _ticketConfig.CountCouponInScene);
        }

        private async Task<GameObject> LoadCoupon(string path)
        {
            TaskCompletionSource<GameObject> isTaskCompletion = new TaskCompletionSource<GameObject>();

            try
            {
                AsyncOperationHandle<GameObject> coupon = Addressables.LoadAssetAsync<GameObject>(path);
                await coupon.Task;

                if (coupon.Status == AsyncOperationStatus.Succeeded) isTaskCompletion.SetResult(coupon.Result);
                else isTaskCompletion.SetException(new Exception("Failed load asset"));
            }
            catch (Exception exception)
            {
                isTaskCompletion.SetException(exception);
            }

            return await isTaskCompletion.Task;
        }
    }
}