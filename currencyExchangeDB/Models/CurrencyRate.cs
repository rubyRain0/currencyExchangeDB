using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace currencyExchangeDB.Models
{
    public class CurrencyRate
    {
        [Key]
        public int CurrencyRateId { get; set; }

        [Required(ErrorMessage = "fromCurrencyId is required.")]
        [ForeignKey("CurrencyId")]
        public int fromCurrencyId { get; set; }

        [Required(ErrorMessage = "toCurrencyId is required.")]
        [ForeignKey("CurrencyId")]
        public int toCurrencyId { get; set; }

        /*public Currency FromCurrency { get; set; }
        public Currency ToCurrency { get; set; }*/

        public double? currencyRate { get; set; }

    }
}
