using Warehouse.Domain.Dto.Employee;
using Warehouse.Domain.Result;

namespace Warehouse.Domain.Interfaces.Services;

public interface IEmployeeService
{
    /// <summary>
    /// Создание сотрудника склада
    /// </summary>
    /// <param name="createEmployeeDto"></param>
    /// <returns></returns>
    Task<BaseResult<EmployeeDto>> CreateEmployee(CreateEmployeeDto createEmployeeDto);
    
    /// <summary>
    /// Получить ТОП сотрудников склада
    /// </summary>
    /// <param name="id">Количество сотрудников в топе</param>
    /// <returns></returns>
    Task<CollectionResult<EmployeeDto>> GetTopEmployees(int count);
    
    /// <summary>
    /// Начисление очков сотруднику
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    Task<BaseResult<EmployeeDto>> EarnPoints(EarnPointsDto employeeDto);
}