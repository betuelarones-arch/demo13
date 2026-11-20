using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDemo2026AB.Models;

[Route("api/[controller]")]
[ApiController]
public class GradesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public GradesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Grade>> GetGrades()
    {
        return _context.Grades.Where(x => x.isActive == true).ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Grade> GetGrade(int id)
    {
        var grade = _context.Grades.Find(id);

        if (grade == null)
        {
            return NotFound();
        }

        return grade;
    }

    [HttpPut("{id}")]
    public IActionResult PutGrade(int id, Grade grade)
    {
        if (id != grade.IdGrade)
        {
            return BadRequest();
        }

        _context.Entry(grade).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!GradeExists(id))
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
    public ActionResult<Grade> PostGrade(Grade grade)
    {
        grade.isActive = true;
        _context.Grades.Add(grade);
        _context.SaveChanges();

        return CreatedAtAction("GetGrade", new { id = grade.IdGrade }, grade);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteGrade(int id)
    {
        var grade = _context.Grades.Find(id);
        if (grade == null)
        {
            return NotFound();
        }

        grade.isActive = false;
        _context.SaveChanges();

        return NoContent();
    }

    private bool GradeExists(int id)
    {
        return _context.Grades.Any(e => e.IdGrade == id);
    }
}
