using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignStudio.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }

        // 🔗 Послуга, яку замовив
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        // Дата замовлення
        public DateTime OrderDate { get; set; }

        // Сума замовлення
        public decimal TotalAmount { get; set; }
    }


}
