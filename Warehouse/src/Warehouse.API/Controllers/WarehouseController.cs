using Microsoft.AspNetCore.Mvc;
using Warehouse.Domain.Dto;
using Warehouse.Domain.Interfaces.Services;
using Warehouse.Domain.Result;

namespace Warehouse.API.Controllers;

[ApiController]
[Route("/api/[controller]/warehouses")]
public class WarehouseController : ControllerBase
{
    private readonly IWarehouseService _warehouseService;

    public WarehouseController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    [HttpGet("")]
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

    [HttpGet("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<WarehouseDto>>> GetWarehouseByIs(long id)
    {
        var response = await _warehouseService.GetWarehouseByIdAsync(id);

        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<WarehouseDto>>> DeleteWarehouse(long id)
    {
        var response = await _warehouseService.DeleteWarehouseAsync(id);
        
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<WarehouseDto>>> CreateWarehouse(
        [FromBody] CreateWarehouseDto warehouseDto)
    {
        var response = await _warehouseService.CreateWarehouseAsync(warehouseDto);
        
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<WarehouseDto>>> UpdateWarehouse(
        [FromBody] UpdateWarehouseDto warehouseDto)
    {
        var response = await _warehouseService.UpdateWarehouseAsync(warehouseDto);
        
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
}