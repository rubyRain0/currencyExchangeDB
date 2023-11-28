using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using currencyExchangeDB.Models;


namespace currencyExchangeDB.DAL
{
    public class CurrencyExchangeContext: DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Cashier> Cashiers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyRate> CurrencyRates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;
                                        Database = CurrencyExchangeDB; 
                                        Trusted_Connection = true");
        }

        public CurrencyExchangeContext()
        {
            
            Database.EnsureDeleted();
            Database.EnsureCreated();
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.Entity<Client>().HasData(
                    new Client { ClientId = 1, firstName = "Иван", lastName = "Иванов", email = "ivan34235@mail.com", 
                        gender = "Мужской/Male", passportNumber = "1234 567890", phoneNumber = "8-123-456-78-90" },
                    new Client { ClientId = 2, firstName = "Jane", lastName = "Doe", email = "jdoe335@gmail.com", 
                        gender = "Женский/Female", passportNumber = "0987 654321", phoneNumber = "8-987-654-32-10" }
                );

                modelBuilder.Entity<Cashier>().HasData(
                    new Cashier { CashierId = 1, firstName = "CashierFirstName1", lastName = "CashierLastName1", email = "cashier1@mail.com",
                    gender = "Другой/Other", phoneNumber = "8-333-222-11-00", hireDate = DateTime.Today}
                );
                //Base state: no transactions.

                modelBuilder.Entity<Currency>().HasData(
                    new Currency { CurrencyId = 1, currencyCode = "RUB", currencyName = "Российский рубль"},
                    new Currency { CurrencyId = 2, currencyCode = "USD", currencyName = "Доллар США"},
                    new Currency { CurrencyId = 3, currencyCode = "CNY", currencyName = "Китайский юань"}
                );

                /*modelBuilder.Entity<CurrencyRate>()
                    .HasOne(cr => cr.FromCurrency)
                    .WithMany()
                    .HasForeignKey(cr => cr.fromCurrencyId)
                    .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<CurrencyRate>()
                    .HasOne(cr => cr.ToCurrency)
                    .WithMany()
                    .HasForeignKey(cr => cr.toCurrencyId)
                    .OnDelete(DeleteBehavior.NoAction);
*/

                modelBuilder.Entity<CurrencyRate>().HasData(
                new CurrencyRate { CurrencyRateId = 1, fromCurrencyId = 1, toCurrencyId = 2, currencyRate = 0.0113},
                new CurrencyRate { CurrencyRateId = 2, fromCurrencyId = 1, toCurrencyId = 3, currencyRate = 0.08},
                new CurrencyRate { CurrencyRateId = 3, fromCurrencyId = 2, toCurrencyId = 3, currencyRate = 7.16}
                //?new CurrencyRate { CurrencyRateId = 2, fromCurrencyId = 2, toCurrencyId = 1, currencyRate = 88} do we need reverse rates or should we calc them 1/reversedRate

                );

                base.OnModelCreating(modelBuilder);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при создании начальных данных:");
                Console.WriteLine(ex.Message);
                Debug.WriteLine("Произошла ошибка при создании начальных данных:");
                Debug.WriteLine(ex.Message);
            }
        }

    }
}
