using AP.ChevronCoop.Entities;
using System.ComponentModel.DataAnnotations;

namespace ChevronCoop.Web.AppUI.BlazorServer.Data
{
    public class CreateDepositProductDTO
    {
        [MaxLength(64)]
        [Required]
        public string Code { get; set; }

        [MaxLength(256)]
        [Required]
        public string Name { get; set; }

        [MaxLength(256)]
        [Required]
        public string ShortName { get; set; }

        //[Required]
        //public int MinimumAge { get; set; }

        //[Required]
        //public int MaximumAge { get; set; }

        [Required]
        public Tenure Tenure { get; set; }

        [Required]
        public decimal TenureValue { get; set; }

        [Required]
        public DepositProductType ProductType { get; set; }

        [MaxLength(80)]
        [Required]
        public string DefaultCurrencyId { get; set; }

        [MaxLength(80)]
        public string? BankDepositAccountId { get; set; }

        [Required]
        public bool IsInterestEnabled { get; set; }

        [MaxLength(512)]
        [Required]
        public string? Description { get; set; }

        [MaxLength(80)]
        [Required]
        public List<string> ProductCharges { get; set; }

        public bool? IsDefaultProduct { get; set; } = false;
    }

}
