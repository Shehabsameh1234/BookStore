using System.Runtime.Serialization;

namespace BookStore.Core.Entities.Orders
{
    public enum OrderStatus
    {
        [EnumMember(Value ="Pending")]
        Pending,

        [EnumMember(Value ="Payment Recieved")]
        PaymentRecieved,

        [EnumMember (Value ="Payment Faild")]
        PaymentFaild
    }
}
