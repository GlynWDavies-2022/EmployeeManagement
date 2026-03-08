using EmployeeManagement.DataAccess.Entities;

namespace EmployeeManagement.Test;

public class EmployeeTests
{
    [Fact]
    public void EmployeeFullNamePropertyGetter_InputFirstNameAndLastName_ReturnsConcatenationOfFirstAndLastName()
    {
        Employee employee = new ExternalEmployee("Seymour","Birkoff","Division");

        var fullName = employee.FullName;

        Assert.Equal("Seymour Birkoff", fullName);

        Assert.Matches("Seymour Birkh?off", fullName);
    }
 
}
