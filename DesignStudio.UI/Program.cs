using DesignStudio.BusinessLogic;
using DesignStudio.Data;
using DesignStudio.Data.Models;
using DesignStudio.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DesignStudio
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            // ✅ Налаштування DI контейнера
            var services = new ServiceCollection();

            // 🔧 Підключаємо DbContext
            services.AddDbContext<DesignStudioContext>(options =>
                options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=DesignStudioDB;Trusted_Connection=True;"));

            // 📦 Реєстрація UnitOfWork та репозиторіїв
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // 📦 Реєстрація сервісу логіки
            services.AddScoped<IDesignService, DesignService>();

            // 🔧 Створення провайдера
            var serviceProvider = services.BuildServiceProvider();

            // ✅ Отримуємо екземпляр сервісу
            var designService = serviceProvider.GetRequiredService<IDesignService>();

            bool running = true;

            while (running)
            {
                Console.WriteLine("\nВиберіть операцію:");
                Console.WriteLine("1. Додати нову послугу");
                Console.WriteLine("2. Створити нове замовлення");
                Console.WriteLine("3. Оновити замовлення");
                Console.WriteLine("4. Видалити замовлення");
                Console.WriteLine("5. Переглянути замовлення");
                Console.WriteLine("6. Переглянути послуги");
                Console.WriteLine("7. Додати елемент до портфоліо");
                Console.WriteLine("8. Переглянути портфоліо");
                Console.WriteLine("9. Вихід");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Введіть назву послуги: ");
                        var serviceName = Console.ReadLine();
                        var newService = new Service { Name = serviceName };
                        designService.AddService(newService);
                        Console.WriteLine("✅ Послуга додана!");
                        break;

                    case "2":
                        Console.WriteLine("Список послуг:");
                        var servicesList = designService.GetServices();
                        foreach (var service in servicesList)
                        {
                            Console.WriteLine($"ID: {service.Id}, Назва: {service.Name}");
                        }

                        Console.Write("Введіть ID послуги для замовлення: ");
                        int serviceId = int.Parse(Console.ReadLine());
                        designService.CreateOrder(serviceId);
                        break;

                    case "3":
                        Console.Write("Введіть ID замовлення для оновлення: ");
                        int orderIdToUpdate = int.Parse(Console.ReadLine());
                        Console.Write("Введіть нову суму замовлення: ");
                        decimal newAmount = decimal.Parse(Console.ReadLine());
                        designService.UpdateOrder(orderIdToUpdate, newAmount);
                        Console.WriteLine("✅ Замовлення оновлено!");
                        break;

                    case "4":
                        Console.Write("Введіть ID замовлення для видалення: ");
                        int orderIdToDelete = int.Parse(Console.ReadLine());
                        designService.DeleteOrder(orderIdToDelete);
                        Console.WriteLine("✅ Замовлення видалено!");
                        break;

                    case "5":
                        var orders = designService.GetOrders();
                        foreach (var order in orders)
                        {
                            Console.WriteLine($"ID: {order.Id}, Клієнт: {order.CustomerName}, Сума: {order.TotalAmount}, Дата: {order.OrderDate}");
                        }
                        break;

                    case "6":
                        var allServices = designService.GetServices();
                        foreach (var service in allServices)
                        {
                            Console.WriteLine($"ID: {service.Id}, Назва: {service.Name}");
                        }
                        break;

                    case "7":
                        Console.WriteLine("Список послуг:");
                        var portfolioServices = designService.GetServices();
                        foreach (var service in portfolioServices)
                        {
                            Console.WriteLine($"ID: {service.Id}, Назва: {service.Name}");
                        }

                        Console.Write("Введіть ID послуги для елемента портфоліо: ");
                        int selectedServiceId = int.Parse(Console.ReadLine());
                        Console.Write("Введіть назву елемента портфоліо: ");
                        string portfolioTitle = Console.ReadLine();
                        Console.Write("Введіть опис елемента портфоліо: ");
                        string portfolioDescription = Console.ReadLine();

                        var newPortfolioItem = new PortfolioItem
                        {
                            Title = portfolioTitle,
                            ImageUrl = " ", // заглушка
                            ServiceId = selectedServiceId
                        };

                        designService.AddPortfolioItem(newPortfolioItem);
                        Console.WriteLine("✅ Елемент портфоліо додано!");
                        break;

                    case "8":
                        var portfolioItems = ((DesignService)designService).GetPortfolioItems(); // або через IDesignService, якщо додати метод
                        foreach (var item in portfolioItems)
                        {
                            string serviceNameOutput = item.Service?.Name ?? "Невідома послуга";
                            Console.WriteLine($"ID: {item.Id}, Назва: {item.Title}, Послуга: {serviceNameOutput}");
                        }
                        break;

                    case "9":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("❌ Невірний вибір. Спробуйте ще раз.");
                        break;
                }
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу для завершення...");
            Console.ReadKey();
        }
    }
}
