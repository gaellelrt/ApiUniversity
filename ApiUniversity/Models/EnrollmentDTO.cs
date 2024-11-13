namespace ApiUniversity.Models;

public class EnrollmentDTO
{
    public int Id { get; set; }
    public Grade Grade { get; set; }
    public int IdStudent { get; set; }
    public int IdCourse { get; set; }
    
    public EnrollmentDTO() { }

}