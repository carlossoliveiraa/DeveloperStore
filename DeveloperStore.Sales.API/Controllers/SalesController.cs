// SaleController.cs
using DeveloperStore.Sales.API.Models;
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

        /// <summary>
        /// Creates a new sale.
        /// </summary>
        [HttpPost("create")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] SaleInputDto dto)
        {
            var result = await _saleService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result }, new { id = result, message = "Sale created successfully" });
        }

        /// <summary>
        /// Gets a sale by ID.
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var sale = await _saleService.GetByIdAsync(id);
            if (sale is null)
                return NotFound(new { message = "Sale not found" });

            return Ok(sale);
        }

        /// <summary>
        /// Lists sales with pagination.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _saleService.ListPagedAsync(page, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Cancels a sale by ID.
        /// </summary>
        [HttpPatch("{id:guid}/cancel")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Cancel(Guid id)
        {
            await _saleService.CancelAsync(id);
            return Ok(new { message = "Sale cancelled successfully" });
        }
    }
}
