using EmployeeManagement.Business;
using EmployeeManagement.Business.EventArguments;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Test.Services;

namespace EmployeeManagement.Test;

public class EmployeeServiceTests
{
    [Fact]
    public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourse_WithObject()
    {
        // Arrange

        var employeeManagementTestDataRepository = new EmployeeManagementTestDataRepository();

        var employeeService = new EmployeeService(employeeManagementTestDataRepository, new EmployeeFactory());

        var obligatoryCourse = employeeManagementTestDataRepository.GetCourse(Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));

        // Act

        var internalEmployee = employeeService.CreateInternalEmployee("Nikita", "Mears");

        // Assert

        Assert.Contains(obligatoryCourse, internalEmployee.AttendedCourses);
    }

    [Fact]
    public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourse()
    {
        // Arrange

        var employeeManagementTestDataRepository = new EmployeeManagementTestDataRepository();

        var employeeService = new EmployeeService(employeeManagementTestDataRepository, new EmployeeFactory());

        // Act

        var internalEmployee = employeeService.CreateInternalEmployee("Nikita", "Mears");

        // Assert

        Assert.Contains(internalEmployee.AttendedCourses, course => course.Id == Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));
    }

    [Fact]
    public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourse_Multiple()
    {
        // Arrange

        var employeeManagementTestDataRepository = new EmployeeManagementTestDataRepository();

        var employeeService = new EmployeeService(employeeManagementTestDataRepository, new EmployeeFactory());

        var obligatoryCourses = employeeManagementTestDataRepository.GetCourses
            (Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
             Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

        // Act

        var internalEmployee = employeeService.CreateInternalEmployee("Nikita", "Mears");

        // Assert

        Assert.Equal(obligatoryCourses, internalEmployee.AttendedCourses);
    }

    [Fact]
    public async Task CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourse_Multiple_Async()
    {
        // Arrange

        var employeeManagementTestDataRepository = new EmployeeManagementTestDataRepository();

        var employeeService = new EmployeeService(employeeManagementTestDataRepository, new EmployeeFactory());

        var obligatoryCourses = await employeeManagementTestDataRepository.GetCoursesAsync
            (Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
             Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

        // Act

        var internalEmployee = await employeeService.CreateInternalEmployeeAsync("Nikita", "Mears");

        // Assert

        Assert.Equal(obligatoryCourses, internalEmployee.AttendedCourses);
    }

    [Fact]
    public async Task GiveRaise_RaiseBelowMinimumGiven_EmployeeInvalidRaiseExceptionMustBeThrown()
    {
        // Arrange

        var employeeService = new EmployeeService(new EmployeeManagementTestDataRepository(), new EmployeeFactory());

        var internalEmployee = new InternalEmployee("Amanda", "Jones", 5, 3000, false, 1);

        // Assert

        await Assert.ThrowsAsync<EmployeeInvalidRaiseException>(async () => await employeeService.GiveRaiseAsync(internalEmployee, 50));
    }

    [Fact]
    public void NotifyOfAbsence_EmployeeIsAbsent_OnEmployeeIsAbsentMustBeTriggered()
    {
        // Arrange

        var employeeService = new EmployeeService(new EmployeeManagementTestDataRepository(), new EmployeeFactory());

        var internalEmployee = new InternalEmployee("Amanda", "Jones", 5, 3000, false, 1);

        // Act & Assert

        Assert.Raises<EmployeeIsAbsentEventArgs>(
            handler => employeeService.EmployeeIsAbsent += handler,
            handler => employeeService.EmployeeIsAbsent -= handler,
            () => employeeService.NotifyOfAbsence(internalEmployee)
        );
    }
}
