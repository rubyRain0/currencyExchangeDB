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
    public class Currency
    {
        [Key] 
        public int CurrencyId { get; set; }

        [Column(TypeName = "varchar(3)")]
        public string? currencyCode { get; set; }

        public string? currencyName { get; set; }
    }
}
