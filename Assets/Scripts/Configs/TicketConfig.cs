using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Coupon Factory Configs", menuName = "Factory Configs / Coupon")]
    public class TicketConfig : ScriptableObject
    {
        [field: SerializeField] public float CountCouponInScene { get; private set; }
        [field: SerializeField] public string KeyForGetCoupon { get; private set; }
        [field: SerializeField] public string PathLoadPrefab { get; private set; }
    }
}