using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiUniversity.Models;

namespace ApiTodo.Controllers;

[ApiController]
[Route("api/todo")]
public class UniversityController : ControllerBase
{
    private readonly UniversityContext _context;

    public UniversityController(UniversityContext context)
    {
        _context = context;
    }

    // GET: api/student
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        // Get todos and related lists
        var students = _context.Students.Include(t => t.Enrollments);
        return await students.ToListAsync();
    }
}