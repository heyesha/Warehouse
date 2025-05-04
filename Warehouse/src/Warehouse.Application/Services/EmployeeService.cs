using Microsoft.EntityFrameworkCore;
using Warehouse.Domain.Dto.Employee;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Enums;
using Warehouse.Domain.Interfaces.Repositories;
using Warehouse.Domain.Interfaces.Services;
using Warehouse.Domain.Result;

namespace Warehouse.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IBaseRepository<Employee> _employeeRepository;
    private readonly IBaseRepository<Achievement> _achievementRepository;
    private readonly IBaseRepository<EmployeeAchievement> _employeeAchievementRepository;

    public EmployeeService(
        IBaseRepository<Employee> employeeRepository, 
        IBaseRepository<Achievement> achievementRepository, 
        IBaseRepository<EmployeeAchievement> employeeAchievementRepository)
    {
        _employeeRepository = employeeRepository;
        _achievementRepository = achievementRepository;
        _employeeAchievementRepository = employeeAchievementRepository;
    }

    public async Task<BaseResult<EmployeeDto>> CreateEmployee(CreateEmployeeDto createEmployeeDto)
    {
        var employee = await _employeeRepository.GetAll()
            .FirstOrDefaultAsync(x => x.Email == createEmployeeDto.Email);
        if (employee != null)
        {
            return new BaseResult<EmployeeDto>()
            {
                ErrorMessage = "Employee already exists!",
                ErrorCode = (int)ErrorCodes.EmployeeAlreadyExists
            };
        }

        employee = new Employee()
        {
            Email = createEmployeeDto.Email,
            Name = createEmployeeDto.Name,
            Phone = createEmployeeDto.Phone,
            WarehouseId = createEmployeeDto.WarehouseId,
        };
        await _employeeRepository.CreateAsync(employee);

        return new BaseResult<EmployeeDto>()
        {
            Data = new EmployeeDto(employee.Id, employee.Name, employee.Email, employee.Phone, employee.WarehouseId, 0, 0)
        };
    }

    public async Task<CollectionResult<EmployeeDto>> GetTopEmployees(int count)
    {
        var topEmployees = await _employeeRepository.GetAll()
            .OrderByDescending(x => x.TotalPoints)
            .Take(count)
            .ToArrayAsync();

        return new CollectionResult<EmployeeDto>()
        {
            Data = topEmployees.Select(x =>
                new EmployeeDto(x.Id, x.Name, x.Email, x.Phone, x.WarehouseId, x.TotalPoints, x.CountOfTasks))
        };
    }

    public async Task<BaseResult<EmployeeDto>> EarnPoints(EarnPointsDto employeeDto)
    {
        var employee = await _employeeRepository.GetAll()
            .FirstOrDefaultAsync(x => x.Id == employeeDto.EmployeeId);
        if (employee == null)
        {
            return new BaseResult<EmployeeDto>()
            {
                ErrorMessage = "Employee does not exist!",
                ErrorCode = (int)ErrorCodes.EmployeeNotFound
            };
        }

        employee.TotalPoints += employeeDto.Points;
        _employeeRepository.Update(employee);
        await _employeeRepository.SaveChangesAsync();

        if (employee.TotalPoints >= 1000)
        {
            var achievement = await _achievementRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Title == "1000 очков");
            if (achievement == null)
            {
                achievement = await _achievementRepository.CreateAsync(new Achievement()
                {
                    Title = "1000 очков",
                    Description = "Выдаётся за получение 1000 очков"
                });
                await _achievementRepository.CreateAsync(achievement);
            }
            
            var employeeWithThisAchievement = await _employeeAchievementRepository.GetAll()
                .FirstOrDefaultAsync(x => x.EmployeeId == employee.Id && x.AchievementId == achievement.Id);
            if (employeeWithThisAchievement != null)
            {
                return new BaseResult<EmployeeDto>()
                {
                    ErrorMessage = "Employee with this achievement already exists!",
                    ErrorCode = (int)ErrorCodes.EmployeeAlreadyExists
                };
            }

            var employeeAchievement = new EmployeeAchievement()
            {
                EmployeeId = employee.Id,
                AchievementId = achievement.Id
            };
            await _employeeAchievementRepository.CreateAsync(employeeAchievement);
        }

        return new BaseResult<EmployeeDto>()
        {
            Data = new EmployeeDto(employee.Id, employee.Name, employee.Email, employee.Phone,
                employee.WarehouseId, employee.TotalPoints, employee.CountOfTasks),
        };
    }
}