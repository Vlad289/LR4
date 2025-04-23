using DesignStudio.Data.Models;
using DesignStudio.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



    namespace DesignStudio.Data.Repositories
    {
        public class UnitOfWork : IUnitOfWork
        {
            private readonly DesignStudioContext _context;

            public IRepository<Service> Services { get; private set; }
            public IRepository<Order> Orders { get; private set; }
            public IRepository<PortfolioItem> PortfolioItems { get; private set; }

            public UnitOfWork(DesignStudioContext context)
            {
                _context = context;
                Services = new Repository<Service>(_context);
                Orders = new Repository<Order>(_context);
                PortfolioItems = new Repository<PortfolioItem>(_context);
            }

            public int Complete() => _context.SaveChanges();

            public void Dispose() => _context.Dispose();
        }
    }


