using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using currencyExchangeDB.Models;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

namespace currencyExchangeDB.DAL
{
    public class DatabaseManager
    {
        private readonly CurrencyExchangeContext _context;

        public DatabaseManager(CurrencyExchangeContext context)
        {
            _context = context;
        }

        public List<Client> GetAllClients()
        {
            try
            {
                return _context.Clients.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при получении списка читателей:");
                Console.WriteLine(ex.Message);

                Debug.WriteLine("Произошла ошибка при получении списка читателей:");
                Debug.WriteLine(ex.Message);

                return new List<Client>();
            }
        }

        public Client GetClientById(int clientId)
        {
            try
            {
                return _context.Clients.FirstOrDefault(r => r.ClientId == clientId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при получении клиента из базы данных:");
                Console.WriteLine(ex.Message);
                Debug.WriteLine("Произошла ошибка при получении клиента из базы данных:");
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public void AddClient(Client client)
        {
            try
            {
                _context.Clients.Add(client);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при добавлении клиента:");
                Console.WriteLine(ex.Message);

                Debug.WriteLine("Произошла ошибка при добавлении клиента:");
                Debug.WriteLine(ex.Message);

            }
        }

        public bool DeleteClient(int clientId)
        {
            try
            {
                Client? client = _context.Clients.Find(clientId);

                if (client != null)
                {
                    _context.Clients.Remove(client);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при удалении клиента:");
                Console.WriteLine(ex.Message);

                Debug.WriteLine("Произошла ошибка при удалении клиента:");
                Debug.WriteLine(ex.Message);

                return false;
            }
        }


        public List<Cashier> GetAllCashiers()
        {
            try
            {
                return _context.Cashiers.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при получении списка кассиров:");
                Console.WriteLine(ex.Message);

                Debug.WriteLine("Произошла ошибка при получении списка кассиров:");
                Debug.WriteLine(ex.Message);

                return new List<Cashier>();
            }
        }

        public Cashier GetCashierById(int cashierId)
        {
            try
            {
                return _context.Cashiers.FirstOrDefault(r => r.CashierId == cashierId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при получении кассира из базы данных:");
                Console.WriteLine(ex.Message);
                Debug.WriteLine("Произошла ошибка при получении кассира из базы данных:");
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public void AddCashier(Cashier cashier)
        {
            try
            {
                _context.Cashiers.Add(cashier);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при добавлении кассира:");
                Console.WriteLine(ex.Message);

                Debug.WriteLine("Произошла ошибка при добавлении кассира:");
                Debug.WriteLine(ex.Message);

            }
        }

        public bool DeleteCashier(int cashierId)
        {
            try
            {
                Cashier? cashier = _context.Cashiers.Find(cashierId);

                if (cashier != null)
                {
                    _context.Cashiers.Remove(cashier);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при удалении кассира:");
                Console.WriteLine(ex.Message);

                Debug.WriteLine("Произошла ошибка при удалении кассира:");
                Debug.WriteLine(ex.Message);

                return false;
            }
        }

        public List<Currency> GetAllCurrencies()
        {
            try
            {
                return _context.Currencies.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при получении списка доступных валют:");
                Console.WriteLine(ex.Message);

                Debug.WriteLine("Произошла ошибка при получении списка доступных валют:");
                Debug.WriteLine(ex.Message);

                return new List<Currency>();
            }
        }

        public Currency GetCurrencyById(int currencyId)
        {
            try
            {
                return _context.Currencies.FirstOrDefault(r => r.CurrencyId == currencyId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при получении валюты из базы данных:");
                Console.WriteLine(ex.Message);
                Debug.WriteLine("Произошла ошибка при получении валюты из базы данных:");
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public Currency GetCurrencyByCode(string currencyCode)
        {
            try
            {
                return _context.Currencies.FirstOrDefault(r => r.currencyCode == currencyCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при получении валюты из базы данных:");
                Console.WriteLine(ex.Message);
                Debug.WriteLine("Произошла ошибка при получении валюты из базы данных:");
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public void AddCurrency(Currency currency)
        {
            try
            {
                _context.Currencies.Add(currency);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при добавлении валюты:");
                Console.WriteLine(ex.Message);

                Debug.WriteLine("Произошла ошибка при добавлении валюты:");
                Debug.WriteLine(ex.Message);

            }
        }

        public bool DeleteCurrency(int currencyId)
        {
            try
            {
                Currency? currency = _context.Currencies.Find(currencyId);

                if (currency != null)
                {
                    _context.Currencies.Remove(currency);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при удалении валюты:");
                Console.WriteLine(ex.Message);

                Debug.WriteLine("Произошла ошибка при удалении валюты:");
                Debug.WriteLine(ex.Message);

                return false;
            }
        }

        public List<CurrencyRate> GetAllCurrencyRates()
        {
            try
            {
                return _context.CurrencyRates.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при получении списка доступных курсов валют:");
                Console.WriteLine(ex.Message);

                Debug.WriteLine("Произошла ошибка при получении списка доступных курсов валют:");
                Debug.WriteLine(ex.Message);

                return new List<CurrencyRate>();
            }
        }

        public CurrencyRate GetCurrencyRateById(int currencyRateId)
        {
            try
            {
                return _context.CurrencyRates.FirstOrDefault(r => r.CurrencyRateId == currencyRateId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при получении курса валюты из базы данных:");
                Console.WriteLine(ex.Message);
                Debug.WriteLine("Произошла ошибка при получении курса валюты из базы данных:");
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public CurrencyRate GetCurrencyRateByCurrencyCodes(int fromCurrencyId, int toCurrencyId)
        {
            try
            {
                return _context.CurrencyRates.FirstOrDefault(r => (r.fromCurrencyId == fromCurrencyId && r.toCurrencyId == toCurrencyId)
                || (r.fromCurrencyId == toCurrencyId && r.toCurrencyId == fromCurrencyId));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при получении курса валюты из базы данных:");
                Console.WriteLine(ex.Message);
                Debug.WriteLine("Произошла ошибка при получении курса валюты из базы данных:");
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public void AddCurrencyRate(CurrencyRate currencyRate)
        {
            try
            {
                _context.CurrencyRates.Add(currencyRate);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при добавлении курса валюты:");
                Console.WriteLine(ex.Message);

                Debug.WriteLine("Произошла ошибка при добавлении курса валюты:");
                Debug.WriteLine(ex.Message);

            }
        }

        public bool DeleteCurrencyRate(int currencyRateId)
        {
            try
            {
                CurrencyRate? currencyRate = _context.CurrencyRates.Find(currencyRateId);

                if (currencyRate != null)
                {
                    _context.CurrencyRates.Remove(currencyRate);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при удалении курса валюты:");
                Console.WriteLine(ex.Message);

                Debug.WriteLine("Произошла ошибка при удалении курса валюты:");
                Debug.WriteLine(ex.Message);

                return false;
            }
        }

        public bool UpdateCurrencyRate(int currencyRateId, double newCurrencyRate)
        {
            try
            {
                CurrencyRate? currencyRate = _context.CurrencyRates.Find(currencyRateId);

                if (currencyRate != null)
                {
                    currencyRate.currencyRate = newCurrencyRate;
                    _context.Entry(currencyRate).State = EntityState.Modified;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при обновлении курса валюты:");
                Console.WriteLine(ex.Message);

                Debug.WriteLine("Произошла ошибка при обновлении курса валюты:");
                Debug.WriteLine(ex.Message);

                return false;
            }
        }


        public List<Transaction> GetAllTransactions()
        {
            try
            {
                return _context.Transactions.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при получении списка транзакций:");
                Console.WriteLine(ex.Message);

                Debug.WriteLine("Произошла ошибка при получении списка транзакций:");
                Debug.WriteLine(ex.Message);

                return new List<Transaction>();
            }
        }

        public Transaction GetTransactionById(int transactionId)
        {
            try
            {
                return _context.Transactions.FirstOrDefault(r => r.TransactionId == transactionId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при получении транзакции из базы данных:");
                Console.WriteLine(ex.Message);
                Debug.WriteLine("Произошла ошибка при получении транзакции из базы данных:");
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public void AddTransaction(Transaction transaction)
        {
            try
            {
                _context.Transactions.Add(transaction);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при добавлении транзакции:");
                Console.WriteLine(ex.Message);

                Debug.WriteLine("Произошла ошибка при добавлении транзакции:");
                Debug.WriteLine(ex.Message);

            }
        }

        public bool DeleteTransaction(int transactionId)
        {
            try
            {
                Transaction? transaction = _context.Transactions.Find(transactionId);

                if (transaction != null)
                {
                    _context.Transactions.Remove(transaction);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при удалении транзакции:");
                Console.WriteLine(ex.Message);

                Debug.WriteLine("Произошла ошибка при удалении транзакции:");
                Debug.WriteLine(ex.Message);

                return false;
            }
        }

    }
}
