using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignStudio.Data.Models
{
    public class PortfolioItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }

        // 🔗 Послуга, до якої належить цей елемент портфоліо
        public int ServiceId { get; set; }
        public Service Service { get; set; }  // Зв'язок з сутністю Service
    }

}
