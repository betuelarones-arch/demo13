using APIDemo2026AB.DTO.Request;
using APIDemo2026AB.DTO.Response;
using APIDemo2026AB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIDemo2026AB.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CoursesCustomController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CoursesCustomController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("insert")]
        public void Insert(CourseRequestV1 request)
        {
            Course model = new Course
            {
                Name = request.Name,
                Credit = request.Credit,
                isActive = true
            };

            _context.Courses.Add(model);
            _context.SaveChanges();
        }

        [HttpPost("insertAll")]
        public void InsertAll(List<CourseRequestV1> request)
        {
            List<Course> model = request.Select(x => new Course
            {
                Name = x.Name,
                Credit = x.Credit,
                isActive = true
            }).ToList();

            _context.AddRange(model);
            _context.SaveChanges();
        }

        [HttpGet("all")]
        public List<CourseResponseV1> Get()
        {
            var courses = _context.Courses.Where(x => x.isActive == true).ToList();

            List<CourseResponseV1> response = courses.Select(x => new CourseResponseV1
            {
                IdCourse = x.IdCourse,
                Name = x.Name,
                Credit = x.Credit
            }).ToList();

            return response;
        }

        [HttpGet("byname")]
        public List<CourseResponseV1> GetByName(string name)
        {
            var courses = _context.Courses.Where(x => x.isActive == true && x.Name.Contains(name)).ToList();

            List<CourseResponseV1> response = courses.Select(x => new CourseResponseV1
            {
                IdCourse = x.IdCourse,
                Name = x.Name,
                Credit = x.Credit
            }).ToList();

            return response;
        }
    }
}
