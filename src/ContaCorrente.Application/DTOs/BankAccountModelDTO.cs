using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ContaCorrente.Application.DTOs
{
    public class BankAccountModelDTO
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required(ErrorMessage = "AccountNumber is Required")]
        [MinLength(8)]
        [MaxLength(8)]
        [DisplayName("Account Number")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "BankCode is Required")]
        [MinLength(3)]
        [MaxLength(3)]
        [DisplayName("Bank Code")]
        public string BankCode { get; set; }

        [Required(ErrorMessage = "Agency Number is Required")]
        [MinLength(4)]
        [MaxLength(4)]
        [DisplayName("Agency Number")]
        public string AgencyNumber { get; set; }

        [Required(ErrorMessage = "Balance is Required")]
        [DisplayName("Balance")]
        [DataType(DataType.Currency)]
        [Range(0.0, Double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public double Balance { get; set; }

        [JsonIgnore]
        public virtual ICollection<TransactionDTO> Transactions { get; set; }
    }
}
