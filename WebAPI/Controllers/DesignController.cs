using DesignStudio.BusinessLogic;
using DesignStudio.BusinessLogic.DTO;
using DesignStudio.BusinessLogic.Mapping;
using DesignStudio.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DesignController : ControllerBase
    {
        private readonly IDesignService _designService;

        public DesignController(IDesignService designService)
        {
            _designService = designService;
        }

        // --- Service ---

        [HttpGet("services")]
        public ActionResult<IEnumerable<ServiceDto>> GetServices()
        {
            var services = _designService.GetServices();
            var dtos = services.Select(s => s.ToDto());
            return Ok(dtos);
        }

        [HttpPost("services")]
        public IActionResult AddService(ServiceDto dto)
        {
            var entity = dto.ToEntity();
            _designService.AddService(entity);
            return Ok();
        }

        // --- Orders ---

        [HttpGet("orders")]
        public ActionResult<IEnumerable<OrderDto>> GetOrders()
        {
            var orders = _designService.GetOrders();
            var dtos = orders.Select(o => o.ToDto());
            return Ok(dtos);
        }

        [HttpPost("orders/{serviceId}")]
        public IActionResult CreateOrder(int serviceId, [FromBody] string customerName)
        {
            _designService.CreateOrder(serviceId, customerName);
            return Ok();
        }


        [HttpPut("orders/{orderId}")]
        public IActionResult UpdateOrder(int orderId, [FromBody] decimal newAmount)
        {
            _designService.UpdateOrder(orderId, newAmount);
            return Ok();
        }

        [HttpDelete("orders/{orderId}")]
        public IActionResult DeleteOrder(int orderId)
        {
            _designService.DeleteOrder(orderId);
            return Ok();
        }

        // --- Portfolio Items ---

        [HttpGet("portfolio")]
        public ActionResult<IEnumerable<PortfolioItemDto>> GetPortfolioItems()
        {
            var items = _designService.GetPortfolioItems();
            var dtos = items.Select(i => i.ToDto());
            return Ok(dtos);
        }

        [HttpPost("portfolio")]
        public IActionResult AddPortfolioItem(PortfolioItemDto dto)
        {
            var entity = dto.ToEntity();
            _designService.AddPortfolioItem(entity);
            return Ok();
        }
    }
}
