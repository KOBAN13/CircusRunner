using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    private void LoadScene() => SceneManager.LoadScene("Finish");

    public void OnTriggerEnter(Collider other)
    {
        LoadScene();
    }
}
