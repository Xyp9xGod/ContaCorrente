using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ContaCorrente.Application.DTOs
{
    public class BankAccountDTO
    {
        [Required(ErrorMessage = "AccountNumber is Required")]
        [MinLength(8)]
        [MaxLength(8)]
        [DisplayName("Account Number")]
        public string AccountNumber { get; private set; }

        [Required(ErrorMessage = "BankCode is Required")]
        [MinLength(3)]
        [MaxLength(3)]
        [DisplayName("Bank Code")]
        public string BankCode { get; private set; }

        [Required(ErrorMessage = "Balance is Required")]
        [DisplayName("Balance")]
        [DataType(DataType.Currency)]
        [Range(0.0, Double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public double Balance { get; private set; }
    }
}
