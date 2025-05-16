using DesignStudio.BusinessLogic.DTO;
using DesignStudio.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignStudio.BusinessLogic.Mapping
{
    public static class MappingExtensions
    {
        // --- Service ---
        public static ServiceDto ToDto(this Service service)
        {
            return new ServiceDto
            {
                Id = service.Id,
                Name = service.Name
            };
        }

        public static Service ToEntity(this ServiceDto dto)
        {
            return new Service
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }

        // --- Order ---
        public static OrderDto ToDto(this Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                ServiceId = order.ServiceId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount
            };
        }

        public static Order ToEntity(this OrderDto dto)
        {
            return new Order
            {
                Id = dto.Id,
                CustomerName = dto.CustomerName,
                ServiceId = dto.ServiceId,
                OrderDate = dto.OrderDate,
                TotalAmount = dto.TotalAmount
            };
        }

        // --- PortfolioItem ---
        public static PortfolioItemDto ToDto(this PortfolioItem item)
        {
            return new PortfolioItemDto
            {
                Id = item.Id,
                Title = item.Title,
                ImageUrl = item.ImageUrl,
                ServiceId = item.ServiceId
            };
        }

        public static PortfolioItem ToEntity(this PortfolioItemDto dto)
        {
            return new PortfolioItem
            {
                Id = dto.Id,
                Title = dto.Title,
                ImageUrl = dto.ImageUrl,
                ServiceId = dto.ServiceId
            };
        }
    }
}
