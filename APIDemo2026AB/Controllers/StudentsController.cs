using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDemo2026AB.Models;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public StudentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Student>> GetStudents()
    {
        return _context.Students.Include(s => s.Grade).ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Student> GetStudent(int id)
    {
        var student = _context.Students.Include(s => s.Grade).FirstOrDefault(s => s.IdStudent == id);

        if (student == null)
        {
            return NotFound();
        }

        return student;
    }

    [HttpPut("{id}")]
    public IActionResult PutStudent(int id, Student student)
    {
        if (id != student.IdStudent)
        {
            return BadRequest();
        }

        _context.Entry(student).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StudentExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpPost]
    public ActionResult<Student> PostStudent(Student student)
    {
        _context.Students.Add(student);
        _context.SaveChanges();

        return CreatedAtAction("GetStudent", new { id = student.IdStudent }, student);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteStudent(int id)
    {
        var student = _context.Students.Find(id);
        if (student == null)
        {
            return NotFound();
        }

        _context.Students.Remove(student);
        _context.SaveChanges();

        return NoContent();
    }

    private bool StudentExists(int id)
    {
        return _context.Students.Any(e => e.IdStudent == id);
    }
}
