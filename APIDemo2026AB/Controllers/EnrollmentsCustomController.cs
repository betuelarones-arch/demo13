using APIDemo2026AB.DTO.Request;
using APIDemo2026AB.DTO.Response;
using APIDemo2026AB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIDemo2026AB.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EnrollmentsCustomController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentsCustomController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("insert")]
        public void Insert(EnrollmentRequestV1 request)
        {
            Enrollment model = new Enrollment
            {
                StudentIdStudent = request.StudentIdStudent,
                CourseIdCourse = request.CourseIdCourse,
                Date = request.Date
            };

            _context.Enrollments.Add(model);
            _context.SaveChanges();
        }

        [HttpPost("insertAll")]
        public void InsertAll(List<EnrollmentRequestV1> request)
        {
            List<Enrollment> model = request.Select(x => new Enrollment
            {
                StudentIdStudent = x.StudentIdStudent,
                CourseIdCourse = x.CourseIdCourse,
                Date = x.Date
            }).ToList();

            _context.AddRange(model);
            _context.SaveChanges();
        }

        [HttpGet("all")]
        public List<EnrollmentResponseV1> Get()
        {
            var enrollments = _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToList();

            List<EnrollmentResponseV1> response = enrollments.Select(x => new EnrollmentResponseV1
            {
                IdEnrollment = x.IdEnrollment,
                StudentFullName = x.Student.FirstName + " " + x.Student.LastName,
                CourseName = x.Course.Name,
                Date = x.Date
            }).ToList();

            return response;
        }

        [HttpGet("bystudent")]
        public List<EnrollmentResponseV1> GetByStudent(int studentId)
        {
            var enrollments = _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Where(x => x.StudentIdStudent == studentId)
                .ToList();

            List<EnrollmentResponseV1> response = enrollments.Select(x => new EnrollmentResponseV1
            {
                IdEnrollment = x.IdEnrollment,
                StudentFullName = x.Student.FirstName + " " + x.Student.LastName,
                CourseName = x.Course.Name,
                Date = x.Date
            }).ToList();

            return response;
        }
    }
}
