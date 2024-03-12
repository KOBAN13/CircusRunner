using System;
using System.Diagnostics;
using Character.PlayerChoise;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Ui
{
    public class ViewModel
    {
        private Model _model;
            
        public readonly ReactiveProperty<string> NameLoadScene = new();
        public readonly ReactiveProperty<string> TelegramLink = new();

        public readonly ReactiveProperty<int> CouponCount = new();
        public readonly ReactiveProperty<string> Time = new();
        
        public readonly ReactiveProperty<bool> IsPause = new();
        public readonly ReactiveProperty<Type> ChoisePlayer = new();
        public readonly ReactiveProperty<Canvas> UnLockCanvas = new();
        public readonly ReactiveProperty<Canvas> LockCanvas = new();

        [Inject]
        public void Construct(Model model)
        {
            _model = model;
            _model.Time.OnChanged += OnModelTimeChanged;
            _model.CouponCount.OnChanged += OnModelCountCouponChanged;
            _model.IsPause.OnChanged += OnСallingPause;
            NameLoadScene.OnChanged += OnLoadScene;
            TelegramLink.OnChanged += OpenTelegram;
            UnLockCanvas.OnChanged += UnlockCanvas;
            LockCanvas.OnChanged += OnLockCanvas;
        }

        private void UnlockCanvas(Canvas obj) => obj.enabled = true;
        private void OnLockCanvas(Canvas obj) => obj.enabled = false;
        public void ExitGame() => Application.Quit();
        private void OnLoadScene(string namedScene) => SceneManager.LoadScene(namedScene);
        private void OnModelCountCouponChanged(int couponCount) => CouponCount.Value = couponCount;
        private void OnModelTimeChanged(string time) => Time.Value = time;
        private void OpenTelegram(string link) => Process.Start(link);
        private void OnСallingPause(bool isPause) => IsPause.Value = isPause;

        ~ViewModel()
        {
            _model.Time.OnChanged -= OnModelTimeChanged;
            _model.CouponCount.OnChanged -= OnModelCountCouponChanged;
            _model.IsPause.OnChanged += OnСallingPause;
            NameLoadScene.OnChanged -= OnLoadScene;
            TelegramLink.OnChanged -= OpenTelegram;
            IsPause.OnChanged -= OnСallingPause;
            UnLockCanvas.OnChanged -= UnlockCanvas;
            LockCanvas.OnChanged -= OnLockCanvas;
        }
    }
}