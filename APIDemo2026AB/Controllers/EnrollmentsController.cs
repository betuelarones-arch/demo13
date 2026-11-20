using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDemo2026AB.Models;

[Route("api/[controller]")]
[ApiController]
public class EnrollmentsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public EnrollmentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Enrollment>> GetEnrollments()
    {
        return _context.Enrollments.Include(e => e.Student).Include(e => e.Course).ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Enrollment> GetEnrollment(int id)
    {
        var enrollment = _context.Enrollments.Include(e => e.Student).Include(e => e.Course).FirstOrDefault(e => e.IdEnrollment == id);

        if (enrollment == null)
        {
            return NotFound();
        }

        return enrollment;
    }

    [HttpPut("{id}")]
    public IActionResult PutEnrollment(int id, Enrollment enrollment)
    {
        if (id != enrollment.IdEnrollment)
        {
            return BadRequest();
        }

        _context.Entry(enrollment).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EnrollmentExists(id))
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
    public ActionResult<Enrollment> PostEnrollment(Enrollment enrollment)
    {
        _context.Enrollments.Add(enrollment);
        _context.SaveChanges();

        return CreatedAtAction("GetEnrollment", new { id = enrollment.IdEnrollment }, enrollment);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEnrollment(int id)
    {
        var enrollment = _context.Enrollments.Find(id);
        if (enrollment == null)
        {
            return NotFound();
        }

        _context.Enrollments.Remove(enrollment);
        _context.SaveChanges();

        return NoContent();
    }

    private bool EnrollmentExists(int id)
    {
        return _context.Enrollments.Any(e => e.IdEnrollment == id);
    }
}
