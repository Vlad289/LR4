using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DesignStudio.BusinessLogic;
using DesignStudio.Data.Models;

namespace DesignStudio.Test
{
    [TestClass]
    public class DesignServiceTests
    {
        private DesignService _service;

        [TestInitialize]
        public void Setup()
        {
            // Спрощена реалізація UnitOfWork із пам'яттю
            _service = new DesignService(new FakeUnitOfWork());
        }

        [TestMethod]
        public void AddService_ShouldAddService()
        {
            // Arrange
            var newService = new Service { Id = 1, Name = "Логотип" };

            // Act
            _service.AddService(newService);
            var services = _service.GetServices();

            // Assert
            Assert.AreEqual(1, ((List<Service>)services).Count);
            Assert.AreEqual("Логотип", ((List<Service>)services)[0].Name);
        }

        [TestMethod]
        public void AddPortfolioItem_ShouldAddItem()
        {
            // Arrange
            var item = new PortfolioItem { Id = 1, Title = "Новий логотип" };

            // Act
            _service.AddPortfolioItem(item);
            var result = _service.GetPortfolioItems();

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Новий логотип", result[0].Title);
        }
    }

    // Спрощена фейкова реалізація IUnitOfWork для тестів
    public class FakeUnitOfWork : DesignStudio.Data.Repositories.IUnitOfWork
    {
        public InMemoryRepository<Service> Services { get; } = new();
        public InMemoryRepository<Order> Orders { get; } = new();
        public InMemoryRepository<PortfolioItem> PortfolioItems { get; } = new();

        public DesignStudio.Data.Repositories.IRepository<Service> Services => Services;
        public DesignStudio.Data.Repositories.IRepository<Order> Orders => Orders;
        public DesignStudio.Data.Repositories.IRepository<PortfolioItem> PortfolioItems => PortfolioItems;

        public void Complete() { /* do nothing */ }
        public void Dispose() { }
    }

    // Проста пам'яті реалізація репозиторію
    public class InMemoryRepository<T> : DesignStudio.Data.Repositories.IRepository<T> where T : class
    {
        private readonly List<T> _items = new();
        public void Add(T entity) => _items.Add(entity);
        public void Remove(T entity) => _items.Remove(entity);
        public IEnumerable<T> GetAll() => _items;
        public T Get(int id)
        {
            var prop = typeof(T).GetProperty("Id");
            return _items.FirstOrDefault(x => (int)prop.GetValue(x) == id);
        }
    }
}
