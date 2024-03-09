using System;
using System.Text.RegularExpressions;
using Ui.UiInterface;

namespace Ui
{
    public class Model : ITime, ITicket, IPause
    {
        private int _countCoupon;
        private string _time;
        private bool _isPause;

        public readonly ReactiveProperty<int> CouponCount = new();
        public readonly ReactiveProperty<string> Time = new();
        public readonly ReactiveProperty<bool> IsPause = new();

        private const string TimeFormatPattern = @"^([0-1]?[0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])$";
        
        public string TimeStartAfterGame
        {
            set
            {
                if (value == null) throw new ArgumentNullException();
                if (!CheckTimeFormat(value)) throw new FormatException();

                _time = value;
                Time.Value = _time;
            }
        }

        public bool Pause
        {
            get => _isPause;
            set
            {
                _isPause = value;
                IsPause.Value = _isPause;
            }
        }

        public int CountTicket
        {
            get => _countCoupon;
            set
            {
                if (value < 0) throw new ArgumentException();
                _countCoupon = value;

                CouponCount.Value = _countCoupon;
            }
        }

        private bool CheckTimeFormat(string time) => Regex.IsMatch(time, TimeFormatPattern);
    }
}