﻿using Microsoft.AspNetCore.Mvc;
using Warehouse.Domain.Dto.Supply;
using Warehouse.Domain.Interfaces.Services;
using Warehouse.Domain.Result;

namespace Warehouse.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class SupplyController : ControllerBase
{
    private readonly ISupplyService _supplyService;

    public SupplyController(ISupplyService supplyService)
    {
        _supplyService = supplyService;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<SupplyDto>>> CreateSupply(
        [FromBody] CreateSupplyDto supplyDto)
    {
        var response = await _supplyService.CreateSupplyAsync(supplyDto);
        
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<SupplyDto>>> GetSupplyById(long id)
    {
        var response = await _supplyService.GetSupplyByIdAsync(id);
        
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("warehouse/{warehouseId:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<SupplyDto>>> GetSuppliesByWarehouse(long warehouseId)
    {
        var response = await _supplyService.GetSuppliesByWarehouse(warehouseId);
        
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
}