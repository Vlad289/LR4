using DesignStudio.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignStudio.BusinessLogic
{
    public interface IDesignService
    {
        List<PortfolioItem> GetPortfolioItems(); 
        void AddPortfolioItem(PortfolioItem item);
        void AddService(Service service);
        void CreateOrder(int serviceId, string customerName);
        void UpdateOrder(int orderId, decimal newAmount);
        void DeleteOrder(int orderId);
        IEnumerable<Order> GetOrders();
        IEnumerable<Service> GetServices();
    }

}
