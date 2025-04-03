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
    [ApiExplorerSettings(GroupName = "Sales")]
    [Tags("Sales")]
    public class SaleController : ControllerBase
    {
        private readonly SaleAppService _saleService;

        public SaleController(SaleAppService saleService)
        {
            _saleService = saleService;
        }

        /// <summary>
        /// Creates a new sale
        /// </summary>
        /// <param name="dto">Sale data</param>
        /// <returns>Created sale ID</returns>
        /// <response code="201">Returns the newly created sale ID</response>
        /// <response code="400">If the sale data is invalid</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<object>), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> Create([FromBody] SaleInputDto dto)
        {
            var result = await _saleService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result }, new { id = result, message = "Sale created successfully" });
        }

        /// <summary>
        /// Gets a sale by ID
        /// </summary>
        /// <param name="id">Sale ID</param>
        /// <returns>Sale details</returns>
        /// <response code="200">Returns the sale details</response>
        /// <response code="404">If the sale is not found</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var sale = await _saleService.GetByIdAsync(id);
            if (sale is null)
                return NotFound(new { message = "Sale not found" });

            return Ok(sale);
        }

        /// <summary>
        /// Lists sales with pagination
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Items per page</param>
        /// <returns>Paginated list of sales</returns>
        /// <response code="200">Returns the paginated list of sales</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<object>), 200)]
        public async Task<IActionResult> GetPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _saleService.ListPagedAsync(page, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Cancels a sale
        /// </summary>
        /// <param name="id">Sale ID</param>
        /// <returns>Success message</returns>
        /// <response code="200">If the sale was successfully cancelled</response>
        /// <response code="404">If the sale is not found</response>
        [HttpPatch("{id:guid}/cancel")]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> Cancel(Guid id)
        {
            await _saleService.CancelAsync(id);
            return Ok(new { message = "Sale cancelled successfully" });
        }
    }
}
