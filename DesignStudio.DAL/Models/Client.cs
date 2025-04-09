using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignStudio.DAL.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Навігаційна властивість — один клієнт може мати багато замовлень
        public List<Order> Orders { get; set; } = new();
    }
}
