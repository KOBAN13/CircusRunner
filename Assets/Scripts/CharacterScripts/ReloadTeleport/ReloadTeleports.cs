using System.Threading.Tasks;
using UnityEngine;

namespace Character.ReloadTeleport
{
    public class ReloadTeleports : MonoBehaviour
    {
        [field: SerializeField] public int TimeToReloadTeleportInMilliseconds { get; private set; }
        
        public async Task Teleport()
        {
            await Task.Delay(TimeToReloadTeleportInMilliseconds);
        }
    }
}