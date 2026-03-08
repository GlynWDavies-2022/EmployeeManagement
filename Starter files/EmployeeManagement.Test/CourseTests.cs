using EmployeeManagement.DataAccess.Entities;

namespace EmployeeManagement.Test;

public class CourseTests
{
    [Fact]
    public void CourseConstructor_ConstructCourse_IsNewMustBeTrue()
    {
        Course course = new Course("C# For Dummies");

        Assert.True(course.IsNew);
    }
}
