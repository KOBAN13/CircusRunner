using System.Collections;
using UnityEngine;
using Zenject;

public class CoroutineRunner : MonoBehaviour
{
    public void StartCoroutineFromExternal(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}
