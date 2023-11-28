using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

using currencyExchangeDB.DAL;
using currencyExchangeDB.Models;
using currencyExchangeDB.Utils;

using Microsoft.EntityFrameworkCore;

/*
Menu:
1. Управление клиентами
    1. Добавить/удалить клиента
2. Управление кассирами
    1. Добавить/удалить кассира
3. Управление транзакциями
    1. Провести обмен валют (совершить транзакцию)
    2. Удалить историю транзакции
4. Управление списком валют
    1. Добавление новой валюты (Теперь принимаем/отдаем) (При добавлении проверить наличие курса обмена к другим валютам, Добавляем обмен на все существующие валюты?)
    2. Удаление существующей валюты (Более не принимаем/не отдаем), соответственно удаление всех валютных курсов, содержащих её ID.
5. Управление валютным курсом
    1.Обновление валютного курса для определенных валют(ы).
    2.Добавление валютного курса
    3.Удаление валютного курса

*/

namespace currencyExchangeDB.Controllers
{
    public class CurrencyExchangeController
    {


        private readonly DatabaseManager _databaseManager;
        private bool _exitRequested;

        public CurrencyExchangeController(DatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
            _exitRequested = false;
        }

        public void Start()
        {
           Console.OutputEncoding = Encoding.Unicode;
           Console.InputEncoding = Encoding.Unicode;

            while (!_exitRequested)
            {
                try
                {
                    MainMenu();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Произошла ошибка: " + ex.Message);
                    Debug.WriteLine("Произошла ошибка: " + ex.Message);
                }
            }
        }

        public void MainMenu()
        {
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Управление клиентами (Добавить/Удалить)");
            Console.WriteLine("2. Управление кассирами (Добавить/Удалить)");
            Console.WriteLine("3. Управление транзакциями (Провести/Удалить историю)");
            Console.WriteLine("4. Управление списком валют (Добавить/Удалить)");
            Console.WriteLine("5. Управление валютным курсом (Обновить/Добавить/Удалить)");
            Console.WriteLine("0. Выйти");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 5)
            {
                Console.WriteLine("Неверный выбор. Пожалуйста, введите число от 0 до 5.");
            }

            switch (choice)
            {
                case 1:
                    ClientMenu();
                    break;
                case 2:
                    CashierMenu();
                    break;
                case 3:
                    TransactionMenu();
                    break;
                case 4:
                    CurrencyMenu();
                    break;
                case 5:
                    CurrencyRateMenu();
                    break;
                case 0:
                    _exitRequested = true;
                    break;
                default:
                    break;
            }
        }

        public void ClientMenu()
        {
            Console.WriteLine("Управление клиентами:");
            Console.WriteLine("1. Добавить клиента");
            Console.WriteLine("2. Удалить клиента");
            Console.WriteLine("0. Назад");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 2)
            {
                Console.WriteLine("Неверный выбор. Пожалуйста, введите число от 0 до 2.");
            }

            switch (choice)
            {
                case 1:
                    try
                    {
                        CreateClient();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Произошла ошибка при создании клиента: " + ex.Message);
                        Debug.WriteLine("Произошла ошибка при создании клиента: " + ex.Message);
                    }
                    break;
                case 2:
                    try
                    {
                        DeleteClient();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Произошла ошибка при удалении клиента: " + ex.Message);
                        Debug.WriteLine("Произошла ошибка при удалении клиента: " + ex.Message);
                    }
                    break;
                case 0:
                    break;
                default:
                    break;
            }
        }

        public void CashierMenu()
        {
            Console.WriteLine("Управление кассирами:");
            Console.WriteLine("1. Добавить кассира");
            Console.WriteLine("2. Удалить кассира");
            Console.WriteLine("0. Назад");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 2)
            {
                Console.WriteLine("Неверный выбор. Пожалуйста, введите число от 0 до 2.");
            }

            switch (choice)
            {
                case 1:
                    try
                    {
                        CreateCashier();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Произошла ошибка при создании кассира: " + ex.Message);
                        Debug.WriteLine("Произошла ошибка при создании кассира: " + ex.Message);
                    }
                    break;
                case 2:
                    try
                    {
                        DeleteCashier();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Произошла ошибка при удалении кассира: " + ex.Message);
                        Debug.WriteLine("Произошла ошибка при удалении кассира: " + ex.Message);
                    }
                    break;
                case 0:
                    break;
                default:
                    break;
            }
        }


        public void TransactionMenu()
        {
                Console.WriteLine("Меню управления транзакциями:");
                Console.WriteLine("1. Провести транзакцию");
                Console.WriteLine("2. Очистить историю транзакций");
                Console.WriteLine("0. Назад");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 2)
                {
                    Console.WriteLine("Неверный выбор. Пожалуйста, введите число от 0 до 2.");
                }

                switch (choice)
                {
                    case 1:
                        try
                        {
                            CompleteTransaction();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Произошла ошибка при проведении транзакции: " + ex.Message);
                            Debug.WriteLine("Произошла ошибка при проведении транзакции: " + ex.Message);
                        }
                        break;
                    case 2:
                        try
                        {
                            DeleteTransaction();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Произошла ошибка при удалении транзакции: " + ex.Message);
                            Debug.WriteLine("Произошла ошибка при удалении транзакции: " + ex.Message);
                        }
                        break;
                    case 0:
                        break;
                    default:
                        break;
                }
        }

        public void CurrencyMenu()
        {
            try
            {
                Console.WriteLine("Управление списком валют:");
                Console.WriteLine("1. Добавить новую валюту");
                Console.WriteLine("2. Удалить валюту из списка доступных");
                Console.WriteLine("0. Назад");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 2)
                {
                    Console.WriteLine($"Неверный выбор. Пожалуйста, введите число от 0 до 2");
                }

                switch (choice)
                {
                    case 1:
                        try
                        {
                            CreateCurrency();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Произошла ошибка при добавлении валюты: " + ex.Message);
                            Debug.WriteLine("Произошла ошибка при добавлении валюты: " + ex.Message);
                        }
                        break;
                    case 2:
                        try
                        {
                            DeleteCurrency();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Произошла ошибка при удалении валюты из списка доступных: " + ex.Message);
                            Debug.WriteLine("Произошла ошибка при удалении валюты из списка доступных: " + ex.Message);
                        }
                        break;
                    case 0:
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при управлении списком валют: " + ex.Message);
                Debug.WriteLine("Произошла ошибка при управлении списком валют: " + ex.Message);
            }
        }


        public void CreateClient()
        {
            try
            {
                string clientFirstName = UtilityFunctions.PromptValidInput("Введите имя клиента:", UtilityFunctions.IsValidName);
                string clientLastName = UtilityFunctions.PromptValidInput("Введите фамилию клиента:", UtilityFunctions.IsValidName);
                int g = -1;
                Console.WriteLine("Введите пол клиента 1-3, 1-Мужской/Male, 2-Женский/Female, 3-Другой/Other");
                while (!int.TryParse(Console.ReadLine(), out g) || g < 1 || g > 3)
                {
                    Console.WriteLine("Неверный выбор. Пожалуйста, введите значение от 1 до 3.");
                }
                string? clientGender;
                switch(g)
                {
                    case 1:
                        {
                            clientGender = "Мужской/Male";
                            break;
                        }
                    case 2:
                        {
                            clientGender = "Женский/Female";
                            break;
                        }
                    case 3:
                        {
                            clientGender = "Другой/Other";
                            break;
                        }
                    default:
                        {
                            clientGender = "None";
                            break;
                        }


                }

                string clientMail = UtilityFunctions.PromptValidInput("Введите электронную почту клиента:", UtilityFunctions.IsValidEmail);
                string clientPassportNumber = UtilityFunctions.PromptValidInput("Введите паспорт в формате: XXXX XXXXXX", UtilityFunctions.IsValidPassportNumber);
                string clientPhoneNumber = UtilityFunctions.PromptValidInput("Введите номер телефона в формате: X-XXX-XXX-XX-XX", UtilityFunctions.IsValidPhoneNumber);
                var client = new Client
                {
                    firstName = clientFirstName,
                    lastName = clientLastName,
                    gender = clientGender,
                    email = clientMail,
                    passportNumber = clientPassportNumber,
                    phoneNumber = clientPhoneNumber

                };
                _databaseManager.AddClient(client);

                Console.WriteLine("Клиент успешно создан!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при создании клиента: " + ex.Message);
                Debug.WriteLine("Произошла ошибка при создании клиента: " + ex.Message);
            }
        }

        public void DeleteClient()
        {
            try
            {
                if (_databaseManager.GetAllClients().Count == 0)
                {
                    Console.WriteLine("В списке нет клиентов.");
                    return;
                }
                Console.WriteLine("Список клиентов:");
                foreach (var client in _databaseManager.GetAllClients())
                {
                    Console.WriteLine($"ID: {client.ClientId}, Фамилия/Имя: {client.lastName} {client.firstName}");
                }

                Console.WriteLine("Введите ID клиента для удаления:");
                int clientId;
                while (!int.TryParse(Console.ReadLine(), out clientId))
                {
                    Console.WriteLine("Неверный ID. Пожалуйста, введите целое число.");
                }

                if (_databaseManager.DeleteClient(clientId))
                    Console.WriteLine("Клиент успешно удален.");
                else
                    Console.WriteLine("Ошибка при удалении.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при удалении клиента: " + ex.Message);
                Debug.WriteLine("Произошла ошибка при удалении клиента: " + ex.Message);
            }
        }

        public void CompleteTransaction()
        {
            try
            {
                if (_databaseManager.GetAllClients().Count == 0)
                {
                    Console.WriteLine("В списке нет клиентов.");
                    return;
                }     
                Console.WriteLine("Список клиентов:");
                foreach (var _client in _databaseManager.GetAllClients())
                {
                    Console.WriteLine($"ID: {_client.ClientId}, Фамилия/Имя: {_client.lastName} {_client.firstName}");
                }

                Console.WriteLine("Введите ID клиента для совершения транзакции:");
                int clientId;
                while (!int.TryParse(Console.ReadLine(), out clientId))
                {
                    Console.WriteLine("Неверный ID. Пожалуйста, введите целое число.");
                }

                if (_databaseManager.GetAllCashiers().Count == 0)
                {
                    Console.WriteLine("В списке нет кассиров.");
                    return;
                }
                Console.WriteLine("Список кассиров:");
                foreach (var cashier in _databaseManager.GetAllCashiers())
                {
                    Console.WriteLine($"ID: {cashier.CashierId}, Фамилия/Имя: {cashier.lastName} {cashier.firstName}");
                }

                Console.WriteLine("Введите ID кассира для совершения транзакции:");
                int cashierId;
                while (!int.TryParse(Console.ReadLine(), out cashierId))
                {
                    Console.WriteLine("Неверный ID. Пожалуйста, введите целое число.");
                }

                if (_databaseManager.GetAllCurrencies().Count == 0)
                {
                    Console.WriteLine("В списке нет доступных валют.");
                    return;
                }
                Console.WriteLine("Список доступных к обмену валют:");
                foreach (var currency in _databaseManager.GetAllCurrencies())
                {
                    Console.WriteLine($"ID: {currency.CurrencyId}, Код/Расшифровка: {currency.currencyCode}/{currency.currencyName}");
                }

                Console.WriteLine("Введите ID продаваемой валюты для совершения транзакции:");
                int fromCurrencyId;
                while (!int.TryParse(Console.ReadLine(), out fromCurrencyId))
                {
                    Console.WriteLine("Неверный ID. Пожалуйста, введите целое число.");
                }
                Console.WriteLine("Введите ID покупаемой валюты для совершения транзакции:");
                int toCurrencyId;
                while (!int.TryParse(Console.ReadLine(), out toCurrencyId) || (toCurrencyId == fromCurrencyId))
                {
                    Console.WriteLine("Неверный ID. Пожалуйста, введите целое число, не совпадающее с ID продаваемой валюты");
                }
                
                CurrencyRate currencyRateEntity = _databaseManager.GetCurrencyRateByCurrencyIDs(fromCurrencyId, toCurrencyId);
                if(currencyRateEntity != null) {
                    bool reversed = false;
                    double? currencyRate;
                    if (currencyRateEntity.fromCurrencyId == fromCurrencyId)
                        currencyRate = currencyRateEntity.currencyRate;
                    else {
                        currencyRate = 1 / (currencyRateEntity.currencyRate);
                        reversed = true;
                    }

                    Console.WriteLine("Введите кол-во единиц приобретаемой валюты:");
                    double currencyAmount;
                    while (!double.TryParse(Console.ReadLine(), out currencyAmount) || currencyAmount < 0)
                    {
                        Console.WriteLine("Неверное число валюты.");
                    }
                    var _transaction = new Transaction
                    {
                        cashierId = cashierId,
                        clientId = clientId,
                        currencyAmount = currencyAmount,
                        currencyRateId = currencyRateEntity.CurrencyRateId,
                        transactionStatus = true,
                        transactionDetails = "None",
                        transactionDate = DateTime.Now

                    };
                    _databaseManager.AddTransaction(_transaction);

                    Console.WriteLine($"В результате транзакции клиент получил {currencyAmount} {_databaseManager.GetCurrencyById(toCurrencyId).currencyCode}" +
                        $" в обмен на {currencyAmount / currencyRate} {_databaseManager.GetCurrencyById(fromCurrencyId).currencyCode}");

                    Console.WriteLine("Транзакция успешно проведена");
                }
                else
                {  
                    var _transaction = new Transaction
                    {
                        cashierId = cashierId,
                        clientId = clientId,
                        currencyAmount = 0,
                        currencyRateId = -1,
                        transactionStatus = false,
                        transactionDetails = "Отмена транзакции. Не существует подходящего курса.",
                        transactionDate = DateTime.Now

                    };
                    _databaseManager.AddTransaction(_transaction);
                    Console.WriteLine("Существующего курса по указанным критериям не было найдено, транзакция не была совершена.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при создании клиента: " + ex.Message);
                Debug.WriteLine("Произошла ошибка при создании клиента: " + ex.Message);
            }
        }

        public void DeleteTransaction()
        {
            try
            {
                if (_databaseManager.GetAllTransactions().Count == 0)
                {
                    Console.WriteLine("В списке нет транзакций.");
                    return;
                }
                Console.WriteLine("Список транзакций:");
                foreach (var transaction in _databaseManager.GetAllTransactions())
                {
                    Console.WriteLine($"ID: {transaction.TransactionId}, Дата транзакции: {transaction.transactionDate}");
                }

                Console.WriteLine("Введите ID транзакции для удаления:");
                int transactionId;
                while (!int.TryParse(Console.ReadLine(), out transactionId))
                {
                    Console.WriteLine("Неверный ID. Пожалуйста, введите целое число.");
                }

                if (_databaseManager.DeleteTransaction(transactionId))
                    Console.WriteLine("Запись о транзакции успешна удалена.");
                else
                    Console.WriteLine("Ошибка при удалении.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при удалении записи о транзакции: " + ex.Message);
                Debug.WriteLine("Произошла ошибка при удалении записи о транзакции: " + ex.Message);
            }
        }

        public void CreateCurrency()
        {
            try
            {
                string _currencyCode = UtilityFunctions.PromptValidInput("Введите буквенный (3 буквы) код валюты стандарта ISO 4217:", UtilityFunctions.IsValidCurrencyCode);

                Currency currencyEntity = _databaseManager.GetCurrencyByCode(_currencyCode);
                if(currencyEntity != null) {
                    Console.WriteLine($"Валюта с указанным кодом: {_currencyCode} уже существует.");
                }
                else
                {
                    string _currencyName = UtilityFunctions.PromptValidInput("Введите оффициальное название валюты:", UtilityFunctions.IsValidCurrencyName);
                    var currency = new Currency
                    {
                        currencyCode = _currencyCode,
                        currencyName = _currencyName
                    };
                    _databaseManager.AddCurrency(currency);

                    Console.WriteLine("Валюта успешно добавлена в список доступных!");
                } 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при добавлении валюты: " + ex.Message);
                Debug.WriteLine("Произошла ошибка при добавлении валюты: " + ex.Message);
            }
        }

        public void DeleteCurrency()
        {
            try
            {
                if (_databaseManager.GetAllCurrencies().Count == 0)
                {
                    Console.WriteLine("В списке нет доступных валют.");
                    return;
                }
                Console.WriteLine("Список доступных валют:");
                foreach (var currency in _databaseManager.GetAllCurrencies())
                {
                    Console.WriteLine($"ID: {currency.CurrencyId}, Код/Расшифровка: {currency.currencyCode}/{currency.currencyName}");
                }

                Console.WriteLine("Введите ID валюты для удаления:");
                int currencyId;
                while (!int.TryParse(Console.ReadLine(), out currencyId))
                {
                    Console.WriteLine("Неверный ID. Пожалуйста, введите целое число.");
                }

                if (_databaseManager.DeleteCurrency(currencyId))
                {
                    List<CurrencyRate> currencyRates = _databaseManager.GetAllCurrencyRates();
                    foreach (CurrencyRate currencyRateEntity in currencyRates)
                    {
                        if (currencyRateEntity.fromCurrencyId == currencyId || currencyRateEntity.toCurrencyId == currencyId)
                            _databaseManager.DeleteCurrencyRate(currencyRateEntity.CurrencyRateId);
                    }
                    Console.WriteLine("Валюта и все связанные с ней курсы успешно удалены из списка доступных.");
                }    
                else
                    Console.WriteLine("Ошибка при удалении.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при удалении валюты: " + ex.Message);
                Debug.WriteLine("Произошла ошибка при удалении валюты: " + ex.Message);
            }
        }

        public void CreateCashier()
        {
            try
            {
                string cashierFirstName = UtilityFunctions.PromptValidInput("Введите имя кассира:", UtilityFunctions.IsValidName);
                string cashierLastName = UtilityFunctions.PromptValidInput("Введите фамилию кассира:", UtilityFunctions.IsValidName);
                int g = -1;
                Console.WriteLine("Введите пол кассира 1-3, 1-Мужской/Male, 2-Женский/Female, 3-Другой/Other");
                while (!int.TryParse(Console.ReadLine(), out g) || g < 1 || g > 3)
                {
                    Console.WriteLine("Неверный выбор. Пожалуйста, введите значение от 1 до 3.");
                }
                string? cashierGender;
                switch (g)
                {
                    case 1:
                        {
                            cashierGender = "Мужской/Male";
                            break;
                        }
                    case 2:
                        {
                            cashierGender = "Женcкий/Female";
                            break;
                        }
                    case 3:
                        {
                            cashierGender = "Другой/Other";
                            break;
                        }
                    default:
                        {
                            cashierGender = "None";
                            break;
                        }


                }

                Console.WriteLine("Введите дату приёма кассира на работу в формате dd.MM.yyyy:");
                DateTime _hireDate;
                while (!DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null,
                    System.Globalization.DateTimeStyles.None, out _hireDate))
                {
                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите корректную дату приёма кассира на работу в формате dd.MM.yyyy.");
                }

                string cashierMail = UtilityFunctions.PromptValidInput("Введите электронную почту кассира:", UtilityFunctions.IsValidEmail);
                string cashierPhoneNumber = UtilityFunctions.PromptValidInput("Введите номер телефона в формате: X-XXX-XXX-XX-XX", UtilityFunctions.IsValidPhoneNumber);
                var cashier = new Cashier
                {
                    firstName = cashierFirstName,
                    lastName = cashierLastName,
                    gender = cashierGender,
                    email = cashierMail,
                    phoneNumber = cashierPhoneNumber,
                    hireDate = _hireDate,

                };
                _databaseManager.AddCashier(cashier);

                Console.WriteLine("Кассир успешно создан!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при создании кассира: " + ex.Message);
                Debug.WriteLine("Произошла ошибка при создании кассира: " + ex.Message);
            }
        }

        public void DeleteCashier()
        {
            try
            {
                if (_databaseManager.GetAllCashiers().Count == 0)
                {
                    Console.WriteLine("В списке нет кассиров.");
                    return;
                }
                Console.WriteLine("Список кассиров:");
                foreach (var cashier in _databaseManager.GetAllCashiers())
                {
                    Console.WriteLine($"ID: {cashier.CashierId}, Фамилия/Имя: {cashier.lastName} {cashier.firstName}");
                }

                Console.WriteLine("Введите ID кассира для удаления:");
                int cashierId;
                while (!int.TryParse(Console.ReadLine(), out cashierId))
                {
                    Console.WriteLine("Неверный ID. Пожалуйста, введите целое число.");
                }

                if (_databaseManager.DeleteCashier(cashierId))
                    Console.WriteLine("Кассир успешно удален.");
                else
                    Console.WriteLine("Ошибка при удалении.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при удалении кассира: " + ex.Message);
                Debug.WriteLine("Произошла ошибка при удалении кассира: " + ex.Message);
            }
        }

        public void CurrencyRateMenu()
        {
            try
            {
                Console.WriteLine("Управление валютным курсом:");
                Console.WriteLine("1. Обновить значение курса для конкретной валюты");
                Console.WriteLine("2. Добавить новый валютный курс");
                Console.WriteLine("3. Удалить валютный курс");
                Console.WriteLine("0. Назад");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 3)
                {
                    Console.WriteLine($"Неверный выбор. Пожалуйста, введите число от 0 до 3");
                }

                switch (choice)
                {
                    case 1:
                        try
                        {
                            UpdateCurrencyRate();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Произошла ошибка при обновлении курса валюты: " + ex.Message);
                            Debug.WriteLine("Произошла ошибка при обновлении курса валюты: " + ex.Message);
                        }
                        break;
                    case 2:
                        try
                        {
                            CreateCurrencyRate();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Произошла ошибка при добавлении курса валюты: " + ex.Message);
                            Debug.WriteLine("Произошла ошибка при добавлении курса валюты: " + ex.Message);
                        }
                        break;
                    case 3:
                        try
                        {
                            DeleteCurrencyRate();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Произошла ошибка при удалении курса валюты: " + ex.Message);
                            Debug.WriteLine("Произошла ошибка при удалении курса валюты: " + ex.Message);
                        }
                        break;
                    case 0:
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при управлении валютными курсами: " + ex.Message);
                Debug.WriteLine("Произошла ошибка при управлении валютными курсами: " + ex.Message);
            }
        }

        public void UpdateCurrencyRate()
        {
            try
            {
                if (_databaseManager.GetAllCurrencies().Count == 0)
                {
                    Console.WriteLine("В списке нет доступных валют.");
                    return;
                }
                Console.WriteLine("Список доступных валют:");
                foreach (var currency in _databaseManager.GetAllCurrencies())
                {
                    Console.WriteLine($"ID: {currency.CurrencyId}, Код/Расшифровка: {currency.currencyCode}/{currency.currencyName}");
                }

                Console.WriteLine("1.Применить действия к конкретному курсу");
                Console.WriteLine("2.Применить действия ко всем курсам связанным с конкретной валютой");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 2)
                {
                    Console.WriteLine($"Неверный выбор. Пожалуйста, введите число от 0 до 2");
                }

                switch (choice)
                {
                    case 1:
                        try
                        {
                            Console.WriteLine("Введите ID валюты для обновления курса:");
                            int currencyId;
                            while (!int.TryParse(Console.ReadLine(), out currencyId))
                            {
                                Console.WriteLine("Неверный ID. Пожалуйста, введите целое число.");
                            }

                            Console.WriteLine("Введите ID связанной валюты для обновления курса:");
                            int currencySecondId;
                            while (!int.TryParse(Console.ReadLine(), out currencySecondId))
                            {
                                Console.WriteLine("Неверный ID. Пожалуйста, введите целое число.");
                            }

                            List<CurrencyRate> currencyRates = _databaseManager.GetAllCurrencyRates();
                            foreach (CurrencyRate currencyRateEntity in currencyRates)
                            {
                                if (currencyRateEntity.fromCurrencyId == currencyId && currencyRateEntity.toCurrencyId == currencySecondId
                                    || currencyRateEntity.fromCurrencyId == currencySecondId && currencyRateEntity.toCurrencyId == currencyId)
                                {
                                    Console.WriteLine($"Курс найден: {_databaseManager.GetCurrencyById(currencyRateEntity.fromCurrencyId).currencyCode} -> {_databaseManager.GetCurrencyById(currencyRateEntity.toCurrencyId).currencyCode} Коэффициент: {currencyRateEntity.currencyRate} ");

                                    Console.WriteLine("Введите новый валютный курс:");
                                    double newCurrencyRate;
                                    while (!double.TryParse(Console.ReadLine(), out newCurrencyRate) || newCurrencyRate < 0)
                                    {
                                        Console.WriteLine("Неверный ввод валютного курса.");
                                    }
                                    _databaseManager.UpdateCurrencyRate(currencyRateEntity.CurrencyRateId, newCurrencyRate);
                                }
                            }
                            Console.WriteLine("Курс успешно обновлен!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Произошла ошибка при обновлении курса валюты: " + ex.Message);
                            Debug.WriteLine("Произошла ошибка при обновлении курса валюты: " + ex.Message);
                        }
                        break;
                    case 2:
                        try
                        {
                            Console.WriteLine("Введите ID валюты для обновления курса:");
                            int currencyId;
                            while (!int.TryParse(Console.ReadLine(), out currencyId))
                            {
                                Console.WriteLine("Неверный ID. Пожалуйста, введите целое число.");
                            }

                            List<CurrencyRate> currencyRates = _databaseManager.GetAllCurrencyRates();
                            foreach (CurrencyRate currencyRateEntity in currencyRates)
                            {
                                if (currencyRateEntity.fromCurrencyId == currencyId && currencyRateEntity.toCurrencyId == currencyId)
                                {
                                    Console.WriteLine($"Курс найден: {_databaseManager.GetCurrencyById(currencyRateEntity.fromCurrencyId).currencyCode} -> {_databaseManager.GetCurrencyById(currencyRateEntity.toCurrencyId).currencyCode} Коэффициент: {currencyRateEntity.currencyRate} ");
                                    Console.WriteLine("Введите новый валютный курс:");
                                    double newCurrencyRate;
                                    while (!double.TryParse(Console.ReadLine(), out newCurrencyRate) || newCurrencyRate < 0)
                                    {
                                        Console.WriteLine("Неверный ввод валютного курса.");
                                    }
                                    _databaseManager.UpdateCurrencyRate(currencyRateEntity.CurrencyRateId, newCurrencyRate);
                                }
                            }
                            Console.WriteLine("Курс успешно обновлен!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Произошла ошибка при обновлении курса валюты: " + ex.Message);
                            Debug.WriteLine("Произошла ошибка при обновлении курса валюты: " + ex.Message);
                        }
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при обновлении курса валют: " + ex.Message);
                Debug.WriteLine("Произошла ошибка при обновлении курса валют: " + ex.Message);
            }
        }

        public void CreateCurrencyRate()
        {
            try
            {
                if (_databaseManager.GetAllCurrencies().Count == 0)
                {
                    Console.WriteLine("В списке нет доступных валют.");
                    return;
                }
                Console.WriteLine("Список доступных валют:");
                foreach (var currency in _databaseManager.GetAllCurrencies())
                {
                    Console.WriteLine($"ID: {currency.CurrencyId}, Код/Расшифровка: {currency.currencyCode}/{currency.currencyName}");
                }

                Console.WriteLine("1.Создать новый курс");
                Console.WriteLine("0.Назад");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 2)
                {
                    Console.WriteLine($"Неверный выбор. Пожалуйста, введите число от 0 до 1");
                }

                switch (choice)
                {
                    case 1:
                        try
                        {
                            Console.WriteLine("Введите ID валюты для создания курса:");
                            int currencyId;
                            while (!int.TryParse(Console.ReadLine(), out currencyId))
                            {
                                Console.WriteLine("Неверный ID. Пожалуйста, введите целое число.");
                            }

                            Console.WriteLine("Введите ID связанной валюты для создания курса:");
                            int currencySecondId;
                            while (!int.TryParse(Console.ReadLine(), out currencySecondId))
                            {
                                Console.WriteLine("Неверный ID. Пожалуйста, введите целое число.");
                            }

                            Console.WriteLine("Введите новый валютный курс:");
                            double newCurrencyRate;
                            while (!double.TryParse(Console.ReadLine(), out newCurrencyRate) || newCurrencyRate < 0)
                            {
                                Console.WriteLine("Неверный ввод валютного курса.");
                            }

                            var currencyRate = new CurrencyRate
                            {
                                fromCurrencyId = currencyId,
                                toCurrencyId = currencySecondId,
                                currencyRate = newCurrencyRate
                            };

                            _databaseManager.AddCurrencyRate(currencyRate);
                            Console.WriteLine("Курс успешно создан!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Произошла ошибка при создании курса валюты: " + ex.Message);
                            Debug.WriteLine("Произошла ошибка при создании курса валюты: " + ex.Message);
                        }
                        break;
                    case 0:
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при создании курса валют: " + ex.Message);
                Debug.WriteLine("Произошла ошибка при создании курса валют: " + ex.Message);
            }
        }

        public void DeleteCurrencyRate()
        {
            try
            {
                if (_databaseManager.GetAllCurrencies().Count == 0)
                {
                    Console.WriteLine("В списке нет доступных валют.");
                    return;
                }
                Console.WriteLine("Список доступных валют:");
                foreach (var currency in _databaseManager.GetAllCurrencies())
                {
                    Console.WriteLine($"ID: {currency.CurrencyId}, Код/Расшифровка: {currency.currencyCode}/{currency.currencyName}");
                }

                Console.WriteLine("1.Удалить конкретный курс");
                Console.WriteLine("2.Удалить курс, связанный с конкретной валютой");
                Console.WriteLine("0.Назад");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 2)
                {
                    Console.WriteLine($"Неверный выбор. Пожалуйста, введите число от 0 до 2");
                }

                switch (choice)
                {
                    case 1:
                        try
                        {
                            Console.WriteLine("Введите ID валюты для удаления курса:");
                            int currencyId;
                            while (!int.TryParse(Console.ReadLine(), out currencyId))
                            {
                                Console.WriteLine("Неверный ID. Пожалуйста, введите целое число.");
                            }

                            Console.WriteLine("Введите ID связанной валюты для удаления курса:");
                            int currencySecondId;
                            while (!int.TryParse(Console.ReadLine(), out currencySecondId))
                            {
                                Console.WriteLine("Неверный ID. Пожалуйста, введите целое число.");
                            }


                            List<CurrencyRate> currencyRates = _databaseManager.GetAllCurrencyRates();
                            foreach (CurrencyRate currencyRateEntity in currencyRates)
                            {
                                if (currencyRateEntity.fromCurrencyId == currencyId && currencyRateEntity.toCurrencyId == currencySecondId
                                    || currencyRateEntity.fromCurrencyId == currencySecondId && currencyRateEntity.toCurrencyId == currencyId)
                                {
                                    Console.WriteLine($"Курс удалён: {_databaseManager.GetCurrencyById(currencyRateEntity.fromCurrencyId).currencyCode} -> {_databaseManager.GetCurrencyById(currencyRateEntity.toCurrencyId).currencyCode} Коэффициент: {currencyRateEntity.currencyRate} ");
                                    _databaseManager.DeleteCurrencyRate(currencyRateEntity.CurrencyRateId);
                                }
                            }
                            if (currencyRates.Count == 0)
                                Console.WriteLine("Подходящих для удаления курсов не было найдено.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Произошла ошибка при удалении курса валюты: " + ex.Message);
                            Debug.WriteLine("Произошла ошибка при удалении курса валюты: " + ex.Message);
                        }
                        break;
                    case 2:
                        try
                        {
                            Console.WriteLine("Введите ID валюты для удаления курса:");
                            int currencyId;
                            while (!int.TryParse(Console.ReadLine(), out currencyId))
                            {
                                Console.WriteLine("Неверный ID. Пожалуйста, введите целое число.");
                            }

                            List<CurrencyRate> currencyRates = _databaseManager.GetAllCurrencyRates();
                            foreach (CurrencyRate currencyRateEntity in currencyRates)
                            {
                                if (currencyRateEntity.fromCurrencyId == currencyId || currencyRateEntity.toCurrencyId == currencyId)
                                {
                                    Console.WriteLine($"Курс найден: {_databaseManager.GetCurrencyById(currencyRateEntity.fromCurrencyId).currencyCode} -> {_databaseManager.GetCurrencyById(currencyRateEntity.toCurrencyId).currencyCode} Коэффициент: {currencyRateEntity.currencyRate} ");
                                    _databaseManager.DeleteCurrencyRate(currencyRateEntity.CurrencyRateId);
                                }
                            }
                            if (currencyRates.Count == 0)
                                Console.WriteLine("Подходящих для удаления курсов не было найдено.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Произошла ошибка при удалении курса валюты: " + ex.Message);
                            Debug.WriteLine("Произошла ошибка при удалении курса валюты: " + ex.Message);
                        }
                        break;
                    case 0:
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при создании курса валют: " + ex.Message);
                Debug.WriteLine("Произошла ошибка при создании курса валют: " + ex.Message);
            }
        }

    }
}