using Shopping.Entities;
using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class ShowCartViewModel
    {
        public User User { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comments")]
        public string Remarks { get; set; }

        public ICollection<TemporalSale> TemporalSales { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public float Quantity => TemporalSales == null ? 0 : TemporalSales.Sum(ts => ts.Quantity);

        public decimal Value => TemporalSales == null ? 0 : TemporalSales.Sum(ts => ts.Value);

    }
}
