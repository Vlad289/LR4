using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignStudio.DAL.Models
{
    public class PortfolioItem
    {
        public int Id { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Яка послуга відповідає за цей приклад у портфоліо
        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;
    }
}
}
