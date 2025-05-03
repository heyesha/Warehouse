using Microsoft.AspNetCore.Mvc;
using Warehouse.Domain.Dto;
using Warehouse.Domain.Interfaces.Services;
using Warehouse.Domain.Result;

namespace Warehouse.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class WarehouseController : ControllerBase
{
    private readonly IWarehouseService _warehouseService;

    public WarehouseController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    [HttpGet("warehouses")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<WarehouseDto>>> GetWarehouses()
    {
        var response  = await _warehouseService.GetWarehousesAsync();

        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
}