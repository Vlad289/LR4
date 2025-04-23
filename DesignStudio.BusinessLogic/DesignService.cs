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
    using DesignStudio.Data.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DesignService : IDesignService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DesignService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddService(Service service)
        {
            _unitOfWork.Services.Add(service);
            _unitOfWork.Complete();
        }

        public IEnumerable<Service> GetServices()
        {
            return _unitOfWork.Services.GetAll();
        }

        public void CreateOrder(int serviceId)
        {
            var service = _unitOfWork.Services.Get(serviceId);
            if (service == null)
            {
                Console.WriteLine("Послугу з таким ID не знайдено.");
                return;
            }

            Console.Write("Введіть ім'я клієнта: ");
            string customerName = Console.ReadLine();

            var order = new Order
            {
                ServiceId = serviceId,
                CustomerName = customerName,
                OrderDate = DateTime.Now
            };

            _unitOfWork.Orders.Add(order);
            _unitOfWork.Complete();
        }

        public void UpdateOrder(int orderId, decimal newAmount)
        {
            var order = _unitOfWork.Orders.Get(orderId);
            if (order == null)
            {
                Console.WriteLine("Замовлення не знайдено.");
                return;
            }

            order.TotalAmount = newAmount;
            _unitOfWork.Complete();
        }

        public void DeleteOrder(int orderId)
        {
            var order = _unitOfWork.Orders.Get(orderId);
            if (order == null)
            {
                Console.WriteLine("Замовлення не знайдено.");
                return;
            }

            _unitOfWork.Orders.Remove(order);
            _unitOfWork.Complete();
        }

        public IEnumerable<Order> GetOrders()
        {
            return _unitOfWork.Orders.GetAll();
        }

        public void AddPortfolioItem(PortfolioItem item)
        {
            _unitOfWork.PortfolioItems.Add(item);
            _unitOfWork.Complete();
        }

        public List<PortfolioItem> GetPortfolioItems()
        {
            return _unitOfWork.PortfolioItems.GetAll().ToList();
        }
    }

}
