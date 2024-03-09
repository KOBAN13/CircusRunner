using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ui
{
    public class ViewFinishScene : MenuView
    {
        [field: SerializeField] protected TextMeshProUGUI time;
        [field: SerializeField] protected TextMeshProUGUI coupon;
        [field: SerializeField] public Button ButtonMenuRestartGame { get; private set; }

        [Inject]
        public void Construct(ViewModel viewModel)
        {
            _viewModel = viewModel;
            time.text = _viewModel.Time.Value;
            coupon.text = "Собранно купонов: " + _viewModel.CouponCount.Value;
        }

        public void AddListenerButtonRestart()
        {
            ButtonMenuRestartGame.onClick.AddListener(() => _viewModel.NameLoadScene.Value = "FinalLevel");
        }
    }
}