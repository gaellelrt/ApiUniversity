namespace ApiUniversity.Models;

public class Instructor
{
    public int Id { get; set; }
    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public DateTime HireDate { get; set; }
    public int InstructorId { get; set; }
    public ICollection<Course> Courses { get; set; } = null!;
    public ICollection<Department> AdministratedDepartements { get; set; } = null!;

}