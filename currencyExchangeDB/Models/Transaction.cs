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
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Required(ErrorMessage = "cashierId is required.")]
        public int cashierId { get; set; }

        /*[ForeignKey("CashierId")]
        public Cashier Cashier { get; set; }*/

        [Required(ErrorMessage = "clientId is required.")]
        public int clientId { get; set; }

        /*[ForeignKey("ClientId")]
        public Client Client { get; set; }*/

        public double? currencyAmount { get; set; }

        [Required(ErrorMessage = "currencyRateId is required.")]
        public int currencyRateId { get; set; }

        /*[ForeignKey("CurrencyRateId")]
        public CurrencyRate CurrencyRate { get; set; }*/

        public bool transactionStatus { get; set; }

        public string? transactionDetails { get; set; }

        public DateTime? transactionDate { get; set; }

        public virtual Cashier Cashier { get; set; }
        public virtual Client Client { get; set; }
        public virtual CurrencyRate CurrencyRate { get; set; }
    }
}
