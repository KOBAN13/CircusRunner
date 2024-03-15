using Ui.Finish;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    [field: SerializeField] public int Level { get; private set; }
    private void LoadScene() => SceneManager.LoadScene("Finish");

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Level>(out var level))
        {
            if (Level == level.SpawnPointPlayer.Count)
                LoadScene();
            level.SetTransformNext(Level);
        }
    }
}
