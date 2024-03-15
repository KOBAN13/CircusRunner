using System.Collections;
using UnityEngine;

namespace Character.ReloadTeleport
{
    public class ReloadTeleports : MonoBehaviour
    {
        [field: SerializeField] public float TimeToReloadTeleportInSecond { get; private set; }
        public bool KeyPressing { get; private set; }
        
        public void KeyDelay()
        {
            KeyPressing = true;
            StartCoroutine(ExecuteWithDelay());
        }

        private IEnumerator ExecuteWithDelay()
        {
            yield return new WaitForSeconds(TimeToReloadTeleportInSecond);
            KeyPressing = false;
        }
    }
}