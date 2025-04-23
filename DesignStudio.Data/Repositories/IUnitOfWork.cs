// IUnitOfWork.cs
using DesignStudio.Data.Models;
using System;

namespace DesignStudio.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Service> Services { get; }
        IRepository<Order> Orders { get; }
        IRepository<PortfolioItem> PortfolioItems { get; }
        int Complete();
    }
}
