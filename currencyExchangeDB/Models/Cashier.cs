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
    public class Cashier
    {
        [Key]
        public int CashierId { get; set; }

        public string? firstName { get; set; }
        public string? lastName { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string? email { get; set; }

        public string? gender { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string? phoneNumber { get; set; }

        public DateTime? hireDate { get; set; }
    }
}
