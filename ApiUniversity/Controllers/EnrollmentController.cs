using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace ApiUniversity.Models;

[ApiController]
[Route("api/enrollment")]
public class EnrollmentController : ControllerBase
{
    private readonly UniversityContext _context;

    public EnrollmentController(UniversityContext context)
    {
        _context = context;
    }

    // GET: api/enrollment/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<DetailedEnrollmentDTO>> GetEnrollmentById(int id)
    {
        // Récupération de l'inscription avec les données associées (Student et Course)
        var enrollment = await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (enrollment == null)
        {
            return NotFound();
        }

        // Conversion en DetailedEnrollmentDTO
        return new DetailedEnrollmentDTO(enrollment);
    }

    // POST: api/enrollment
    [HttpPost]
    public async Task<ActionResult<DetailedEnrollmentDTO>> CreateEnrollment(DetailedEnrollmentDTO enrollmentDTO)
    {
        // Vérifie si l'étudiant et le cours existent
        var student = await _context.Students.FindAsync(enrollmentDTO.Student.Id);
        var course = await _context.Courses.FindAsync(enrollmentDTO.Course.Id);

        if (student == null || course == null)
        {
            return BadRequest("Student or Course not found");
        }

        // Crée une nouvelle inscription
        var enrollment = new Enrollment
        {
            StudentId = enrollmentDTO.Student.Id,
            CourseId = enrollmentDTO.Course.Id,
            Grade = enrollmentDTO.Grade
        };

        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();

        // Retourne l'inscription sous forme de DetailedEnrollmentDTO
        return CreatedAtAction(nameof(GetEnrollmentById), new { id = enrollment.Id }, new DetailedEnrollmentDTO(enrollment));
    }

    
}