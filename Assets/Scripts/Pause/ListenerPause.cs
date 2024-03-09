using System;
using Ui;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Pause
{
    public class ListenerPause : MonoBehaviour
    {
        private NewInputSystem _inputSystem;
        private IPause _pause;
        private bool _isPause;

        [Inject]
        public void Construct(NewInputSystem inputSystem, IPause pause)
        {
            _inputSystem = inputSystem;
            _pause = pause;
        }
        
        private void OnEnable() => _inputSystem.Pause.Pause.performed += OnСallingPause;

        private void OnDisable() => _inputSystem.Pause.Pause.performed -= OnСallingPause;

        private void OnСallingPause(InputAction.CallbackContext obj)
        {
            _isPause = !_isPause;
            _pause.Pause = _isPause;
            ChangeTimeScale();
        }
        
        private void ChangeTimeScale() => Time.timeScale = _isPause ? 0f : 1f;
    }
}