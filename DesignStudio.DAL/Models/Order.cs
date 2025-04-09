using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignStudio.DAL.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;

        public DateTime OrderDate { get; set; } = DateTime.Now;

        // Замовлення може містити кілька послуг (M:N)
        public List<Service> Services { get; set; } = new();
    }
}

