using Microsoft.VisualStudio.TestTools.UnitTesting;
using DesignStudio.BusinessLogic;
using DesignStudio.Data.Models;
using DesignStudio.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DesignStudio.Tests
{
    [TestClass]
    public class DesignServiceTests
    {
        private class FakeUnitOfWork : IUnitOfWork
        {
            public IRepository<Service> Services { get; set; } = new FakeRepository<Service>();
            public IRepository<Order> Orders { get; set; } = new FakeRepository<Order>();
            public IRepository<PortfolioItem> PortfolioItems { get; set; } = new FakeRepository<PortfolioItem>();
            public int Complete() 
            {
                return 1;            }

            public void Dispose()
            {
                // Тут можна додати логіку для очищення або звільнення ресурсів
                // Якщо ви не використовуєте жодних ресурсів, це можна залишити порожнім
            }
        }

        public class FakeRepository<T> : IRepository<T> where T : class
        {
            private readonly List<T> _items = new List<T>();

            public void Add(T entity) => _items.Add(entity);

            public void Remove(T entity) => _items.Remove(entity);

            public T Get(int id) => _items.FirstOrDefault();

            public IEnumerable<T> GetAll() => _items;

            public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
            {
                // Створення компіляції виразу в лінійний код
                var compiledPredicate = predicate.Compile();
                return _items.Where(compiledPredicate).ToList();
            }
        }


        [TestMethod]
        public void AddService_ShouldAddService()
        {
            var uow = new FakeUnitOfWork();
            var service = new Service { Id = 1, Name = "Дизайн логотипу" };
            var designService = new DesignService(uow);

            designService.AddService(service);

            var result = uow.Services.GetAll().FirstOrDefault();
            Assert.IsNotNull(result);
            Assert.AreEqual("Дизайн логотипу", result.Name);
        }

        [TestMethod]
        public void GetServices_ShouldReturnServices()
        {
            var uow = new FakeUnitOfWork();
            uow.Services.Add(new Service { Id = 1, Name = "Логотип" });
            var designService = new DesignService(uow);

            var result = designService.GetServices();

            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void AddPortfolioItem_ShouldAddItem()
        {
            var uow = new FakeUnitOfWork();
            var item = new PortfolioItem { Id = 1, Title = "Портфоліо" };
            var designService = new DesignService(uow);

            designService.AddPortfolioItem(item);

            var result = uow.PortfolioItems.GetAll().FirstOrDefault();
            Assert.IsNotNull(result);
            Assert.AreEqual("Портфоліо", result.Title);
        }

        [TestMethod]
        public void GetPortfolioItems_ShouldReturnItems()
        {
            var uow = new FakeUnitOfWork();
            uow.PortfolioItems.Add(new PortfolioItem { Id = 1, Title = "Пункт 1" });
            var designService = new DesignService(uow);

            var result = designService.GetPortfolioItems();

            Assert.AreEqual(1, result.Count);
        }
    }
}
