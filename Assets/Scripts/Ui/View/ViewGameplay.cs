using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ui
{
    public class ViewGameplay : MenuView
    {
        [field: SerializeField] protected TextMeshProUGUI time;
        [field: SerializeField] public TextMeshProUGUI CountCoupons { get; private set; }
        
        [field: SerializeField] public Canvas UiGame { get; private set; }
        [field: SerializeField] public Canvas Pause { get; private set; }
        [field: SerializeField] public Button Resume { get; private set; }

        [Inject]
        public void Construct(ViewModel viewModel)
        {
            _viewModel = viewModel;
            Pause.enabled = false;
        }
        
        public void OnEnable()
        {
            _viewModel.Time.OnChanged += RecordViewTime;
            _viewModel.CouponCount.OnChanged += RecordViewCountCoupon;
            _viewModel.IsPause.OnChanged += CanvasLock;
        }

        public void OnDisable()
        {
            _viewModel.Time.OnChanged -= RecordViewTime;
            _viewModel.CouponCount.OnChanged -= RecordViewCountCoupon;
            _viewModel.IsPause.OnChanged -= CanvasLock;
        }

        public void AddListenerResume() => Resume.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            _viewModel.NameLoadScene.Value = "StartGame";
        });
        
        private void CanvasLock(bool isPause)
        {
            if (isPause)
            {
                UiGame.enabled = false;
                Pause.enabled = true;
            }
            else
            {
                UiGame.enabled = true;
                Pause.enabled = false;
            }
        }
        
        private void RecordViewTime(string time) => this.time.text = time;

        private void RecordViewCountCoupon(int count) => CountCoupons.text = count.ToString();
    }
}