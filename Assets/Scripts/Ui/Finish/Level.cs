using System.Collections.Generic;
using UnityEngine;

namespace Ui.Finish
{
    public class Level : MonoBehaviour
    {
        [field: SerializeField] public CharacterController Player { get; private set; }
        [field: SerializeField] public List<GameObject> SpawnPointPlayer { get; private set; }

        public void SetTransformNext(int index)
        {
            // Получаем целевую позицию из списка SpawnPointPlayer по указанному индексу
            Vector3 targetPosition = SpawnPointPlayer[index].transform.position;

            // Вычисляем вектор направления для перемещения
            Vector3 moveDirection = targetPosition - Player.transform.position;

            Player.Move(moveDirection);
        }
    }
}