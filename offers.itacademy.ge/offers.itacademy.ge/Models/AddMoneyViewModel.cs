using System.ComponentModel.DataAnnotations;

namespace ITAcademy.Offers.Web.Models
{
    public class AddMoneyViewModel
    {
        [Required(ErrorMessage = "Please enter an amount")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Maximum two decimal places allowed")]
        [Range(0.01, 10000, ErrorMessage = "Amount must be greater than 0")]

        public decimal Amount { get; set; }

        public decimal CurrentBalance { get; set; }
    }
}
