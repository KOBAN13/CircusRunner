using Ui.UiInterface;
using UnityEngine;
using Zenject;

namespace Ui
{
    public class TimeManager : ITickable
    {
        private float _currentTime;
        private ITime _time;

        [Inject]
        public void Construct(ITime time)
        {
            _time = time;
        }
        
        public void Tick()
        {
            _currentTime += Time.deltaTime;
            _time.TimeStartAfterGame = FormatTime(_currentTime);
        }

        private string FormatTime(float time)
        {
            var hours = Mathf.FloorToInt(time / 3600);
            var minutes = Mathf.FloorToInt((time % 3600) / 60);
            var seconds = Mathf.FloorToInt(time % 60);

            return $"{hours:00}:{minutes:00}:{seconds:00}";
        }
    }
}