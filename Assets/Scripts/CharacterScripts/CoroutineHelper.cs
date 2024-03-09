using System.Collections;
using Zenject;

namespace Character
{
    public class CoroutineHelper
    {
        private CoroutineRunner _coroutineRunner;

        [Inject]
        public CoroutineHelper(CoroutineRunner runner)
        {
            _coroutineRunner = runner;
        }

        public void StartExternalCoroutine(IEnumerator coroutine)
        {
            _coroutineRunner.StartCoroutineFromExternal(coroutine);
        }
    }
}