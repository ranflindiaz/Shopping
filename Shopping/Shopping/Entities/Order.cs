using Shopping.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shopping.Entities
{
    public class Order
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        [Required(ErrorMessage = "The field {0} is requried.")]
        public DateTime Date { get; set; }

        public User User { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comments")]
        public string Remarks { get; set; }

        [Display(Name = "Status")]
        public OrderStatus OrderStatus { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int Lines => OrderDetails == null ? 0 : OrderDetails.Count;

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public float Quantity => OrderDetails == null ? 0 : OrderDetails.Sum(sd => sd.Quantity);

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Value => OrderDetails == null ? 0 : OrderDetails.Sum(sd => sd.Value);
    }
}
