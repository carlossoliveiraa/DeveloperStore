using DeveloperStore.Sales.Application.DTOs.Inputs;
using DeveloperStore.Sales.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperStore.Sales.API.Controllers
{
    [ApiController]
    [Route("api/v1/sales")]
    [Authorize]
    public class SaleController : ControllerBase
    {
        private readonly SaleAppService _saleService;

        public SaleController(SaleAppService saleService)
        {
            _saleService = saleService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaleInputDto dto)
        {
            var result = await _saleService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result }, new { id = result, message = "Sale created successfully" });
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var sale = await _saleService.GetByIdAsync(id);
            if (sale is null)
                return NotFound(new { message = "Sale not found." });

            return Ok(sale);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _saleService.ListPagedAsync(page, pageSize);
            return Ok(result);
        }

        [HttpPatch("{id:guid}/cancel")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            await _saleService.CancelAsync(id);
            return Ok(new { message = "Sale cancelled successfully" });
        }
    }
}
