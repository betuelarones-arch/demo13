using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDemo2026AB.Models;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public CoursesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Course>> GetCourses()
    {
        return _context.Courses.Where(x => x.isActive == true).ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Course> GetCourse(int id)
    {
        var course = _context.Courses.Find(id);

        if (course == null)
        {
            return NotFound();
        }

        return course;
    }

    [HttpPut("{id}")]
    public IActionResult PutCourse(int id, Course course)
    {
        if (id != course.IdCourse)
        {
            return BadRequest();
        }

        _context.Entry(course).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CourseExists(id))
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
    public ActionResult<Course> PostCourse(Course course)
    {
        course.isActive = true;
        _context.Courses.Add(course);
        _context.SaveChanges();

        return CreatedAtAction("GetCourse", new { id = course.IdCourse }, course);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCourse(int id)
    {
        var course = _context.Courses.Find(id);
        if (course == null)
        {
            return NotFound();
        }

        course.isActive = false;
        _context.SaveChanges();

        return NoContent();
    }

    private bool CourseExists(int id)
    {
        return _context.Courses.Any(e => e.IdCourse == id);
    }
}
