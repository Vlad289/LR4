using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignStudio.Data.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // 🔗 Зв'язок з замовленнями
        public ICollection<Order> Orders { get; set; }

        // 🔗 Зв'язок з портфоліо
        public ICollection<PortfolioItem> PortfolioItems { get; set; }
    }


}
