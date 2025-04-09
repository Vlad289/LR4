using System;
using System.Linq;
using DesignStudio.BusinessLogic;
using DesignStudio.Data;
using DesignStudio.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DesignStudio
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=DesignStudioDB;Trusted_Connection=True;";

            var optionsBuilder = new DbContextOptionsBuilder<DesignStudioContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using (var context = new DesignStudioContext(optionsBuilder.Options))
            {
                var designService = new DesignService(context);

                bool running = true;

                while (running)
                {
                    Console.WriteLine("Виберіть операцію:");
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
                            Console.WriteLine("Послуга додана!");
                            break;

                        case "2":
                            Console.WriteLine("Список послуг:");
                            var services = designService.GetServices();
                            foreach (var service in services)
                            {
                                Console.WriteLine($"ID: {service.Id}, Назва: {service.Name}");
                            }
                            Console.Write("Введіть ID послуги для замовлення: ");
                            int serviceId = int.Parse(Console.ReadLine());
                            designService.CreateOrder(serviceId);
                            Console.WriteLine("Замовлення створено!");
                            break;

                        case "3":
                            Console.Write("Введіть ID замовлення для оновлення: ");
                            int orderIdToUpdate = int.Parse(Console.ReadLine());
                            Console.Write("Введіть нову суму замовлення: ");
                            decimal newAmount = decimal.Parse(Console.ReadLine());
                            designService.UpdateOrder(orderIdToUpdate, newAmount);
                            Console.WriteLine("Замовлення оновлено!");
                            break;

                        case "4":
                            Console.Write("Введіть ID замовлення для видалення: ");
                            int orderIdToDelete = int.Parse(Console.ReadLine());
                            designService.DeleteOrder(orderIdToDelete);
                            Console.WriteLine("Замовлення видалено!");
                            break;

                        case "5":
                            var orders = designService.GetOrders();
                            foreach (var order in orders)
                            {
                                Console.WriteLine($"ID: {order.Id}, Клієнт: {order.CustomerName}, Сума: {order.TotalAmount}");
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
                            // Додати елемент до портфоліо
                            Console.WriteLine("Список послуг для елементів портфоліо:");
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
                                ServiceId = selectedServiceId,
                                ImageUrl = " "
                            };

                            designService.AddPortfolioItem(newPortfolioItem);
                            Console.WriteLine("Елемент портфоліо додано!");
                            break;

                        case "8":
                            // Перегляд елементів портфоліо
                            var portfolioItems = designService.GetPortfolioItems();
                            foreach (var item in portfolioItems)
                            {
                                Console.WriteLine($"ID: {item.Id}, Назва: {item.Title}, Послуга: {item.Service.Name}");
                            }
                            break;

                        case "9":
                            running = false;
                            break;

                        default:
                            Console.WriteLine("Невірний вибір!");
                            break;
                    }
                }
            }

            Console.WriteLine("Натисніть будь-яку клавішу для завершення...");
            Console.ReadKey();
        }
    }
}
