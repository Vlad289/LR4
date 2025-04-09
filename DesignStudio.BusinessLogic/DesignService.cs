using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignStudio.Data;
using DesignStudio.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DesignStudio.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DesignService
    {
        private readonly DesignStudioContext _context;

        // Конструктор для ініціалізації контексту
        public DesignService(DesignStudioContext context)
        {
            _context = context;
        }

        // Додавання нової послуги
        public void AddService(Service service)
        {
            _context.Services.Add(service);
            _context.SaveChanges();
        }

        // Отримати всі послуги
        public List<Service> GetServices()
        {
            return _context.Services.ToList();
        }

        // Створити нове замовлення
        public void CreateOrder(int serviceId)
        {
            var service = _context.Services.Find(serviceId);
            if (service == null)
            {
                Console.WriteLine("Послугу з таким ID не знайдено.");
                return;
            }

            Console.Write("Введіть ім'я клієнта: ");
            string customerName = Console.ReadLine();  // Додано введення імені клієнта

            var order = new Order
            {
                ServiceId = serviceId,
                CustomerName = customerName,  // Використовуємо введене ім'я клієнта
                OrderDate = DateTime.Now
            };

            _context.Orders.Add(order);
            _context.SaveChanges();
            Console.WriteLine("Замовлення створено!");
        }

        // Оновити замовлення
        public void UpdateOrder(int orderId, decimal newAmount)
        {
            var order = _context.Orders.Find(orderId);
            if (order == null)
            {
                Console.WriteLine("Замовлення не знайдено.");
                return;
            }

            order.TotalAmount = newAmount;
            _context.SaveChanges();
        }

        // Видалити замовлення
        public void DeleteOrder(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if (order == null)
            {
                Console.WriteLine("Замовлення не знайдено.");
                return;
            }

            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        // Отримати всі замовлення
        public List<Order> GetOrders()
        {
            return _context.Orders.ToList();
        }

        // Додавання нового елемента в портфоліо
        public void AddPortfolioItem(PortfolioItem item)
        {
            _context.PortfolioItems.Add(item);
            _context.SaveChanges();
        }

        // Отримати всі елементи портфоліо
        public List<PortfolioItem> GetPortfolioItems()
        {
            return _context.PortfolioItems.ToList();
        }
    }

}
