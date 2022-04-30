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

        public OrderStatus OrderStatus { get; set; }

        public ICollection<OrderDetail> SaleDetails { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int Lines => SaleDetails == null ? 0 : SaleDetails.Count;

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public float Quantity => SaleDetails == null ? 0 : SaleDetails.Sum(sd => sd.Quantity);

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Value => SaleDetails == null ? 0 : SaleDetails.Sum(sd => sd.Value);
    }
}
