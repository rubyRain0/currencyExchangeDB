using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using currencyExchangeDB.DAL;
using currencyExchangeDB.Models;
using currencyExchangeDB.Controllers;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        using (var context = new CurrencyExchangeContext())
        {
            // Create database 
            var databaseManager = new DatabaseManager(context);

            var controller = new CurrencyExchangeController(databaseManager);

            controller.Start();

            context.Dispose();

        }
    }
}