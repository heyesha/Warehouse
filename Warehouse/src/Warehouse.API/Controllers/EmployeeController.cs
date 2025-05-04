using Microsoft.AspNetCore.Mvc;
using Warehouse.Domain.Dto.Employee;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Interfaces.Services;
using Warehouse.Domain.Result;

namespace Warehouse.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<EmployeeDto>>> CreateEmployee([FromBody] CreateEmployeeDto createEmployeeDto)
    {
        var response = await _employeeService.CreateEmployee(createEmployeeDto);
        
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("{count:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CollectionResult<EmployeeDto>>> GetEmployee(int count)
    {
        var response = await _employeeService.GetTopEmployees(count);
        
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPost("achievements")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<EmployeeDto>>> EarnPointsEmployee(EarnPointsDto earnPointsDto)
    {
        var response = await _employeeService.EarnPoints(earnPointsDto);
        
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
}