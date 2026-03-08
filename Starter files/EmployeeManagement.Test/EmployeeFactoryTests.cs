using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;

namespace EmployeeManagement.Test;

public class EmployeeFactoryTests : IDisposable
{
    private readonly EmployeeFactory _employeeFactory;

    public EmployeeFactoryTests()
    {
        _employeeFactory = new EmployeeFactory();
    }

    public void Dispose()
    { }

    [Fact]
    public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500()
    {
        var employee = (InternalEmployee) _employeeFactory.CreateEmployee("Margaret", "Quigley");

        Assert.True(employee.Salary == 2500);

        Assert.True(employee.Salary < 3000);

        Assert.InRange(employee.Salary, 2000, 3000);
    }

    [Fact]
    public void CreateEmployee_ConstructInternalEmployee_PrecisionExample()
    {
        var employee = (InternalEmployee) _employeeFactory.CreateEmployee("Margaret", "Quigley");

        employee.Salary = 2500.123M;

        Assert.Equal(2500, employee.Salary, 0);
    }

    [Fact]
    public void CreateEmployee_ExternalIsTrue_ReturnTypeMustBeExternalEmployee()
    {
        // Arrange

        // Act

        var employee = _employeeFactory.CreateEmployee("Alexandra", "Udinov", "Division", true);

        // Assert

        Assert.IsType<ExternalEmployee>(employee);
    }

    [Fact]
    public void CreateEmployee_ExternalIsTrue_ReturnTypeMustBeObject()
    {
        // Arrange

        // Act

        var employee = _employeeFactory.CreateEmployee("Alexandra", "Udinov", "Division", true);

        // Assert

        Assert.IsAssignableFrom<Object>(employee);
    }
}
