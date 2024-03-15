using System.Collections.Generic;
using UnityEngine;

namespace Ui.Finish
{
    public class Level : MonoBehaviour
    {
        [field: SerializeField] public CharacterController Player { get; private set; }
        [field: SerializeField] public List<Transform> SpawnPointPlayer { get; private set; }

        public void SetTransformNext(int index)
        {
            Player.enabled = false;
            Player.gameObject.transform.position = SpawnPointPlayer[index].position;
            Player.enabled = true;
        }
    }
}