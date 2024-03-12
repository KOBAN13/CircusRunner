using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Character.Loader
{
    public class AddressableLoader
    {
        public async Task<T> Loader<T>(string path)
        {
            TaskCompletionSource<T> isTaskCompletion = new TaskCompletionSource<T>();

            try
            {
                AsyncOperationHandle<T> coupon = Addressables.LoadAssetAsync<T>(path);
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