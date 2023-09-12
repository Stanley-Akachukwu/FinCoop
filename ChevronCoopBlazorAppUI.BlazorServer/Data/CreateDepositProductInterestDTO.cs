using System.ComponentModel.DataAnnotations;

namespace ChevronCoop.Web.AppUI.BlazorServer.Data
{
    public class CreateDepositProductInterestDTO
    {
        [MaxLength(80)]
        [Required]
        public string ProductId { get; set; }

        [Required]
        public decimal LowerLimit { get; set; }

        [Required]
        public decimal UpperLimit { get; set; }

        [Required]
        public decimal InterestRate { get; set; }
        public string Id { get; set; }
        public string InterestId { get; set; }
    }
}
