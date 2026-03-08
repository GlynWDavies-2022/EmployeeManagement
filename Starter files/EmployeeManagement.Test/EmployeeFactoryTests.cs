using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;

namespace EmployeeManagement.Test;

public class EmployeeFactoryTests
{
    [Fact]
    public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500()
    {
        var employeeFactory = new EmployeeFactory();

        var employee = (InternalEmployee) employeeFactory.CreateEmployee("Margaret", "Quigley");

        Assert.True(employee.Salary == 2500);

        Assert.True(employee.Salary < 3000);

        Assert.InRange(employee.Salary, 2000, 3000);
    }

    [Fact]
    public void CreateEmployee_ConstructInternalEmployee_PrecisionExample()
    {
        var employeeFactory = new EmployeeFactory();

        var employee = (InternalEmployee)employeeFactory.CreateEmployee("Margaret", "Quigley");

        employee.Salary = 2500.123M;

        Assert.Equal(2500, employee.Salary, 0);
    }
}
