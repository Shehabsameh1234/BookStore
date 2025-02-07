using System.ComponentModel.DataAnnotations;


namespace BookStore.Core.Dtos
{
    public class OrderDto
    {
        [Required]
        public string BasketId { get; set; } = null!;
        [Required]
        public int DeliveryMethodId { get; set; }
        [Required]
        public AddressDto OrderAddress { get; set; } = null!;
    }
}
