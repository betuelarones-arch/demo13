using APIDemo2026AB.DTO.Request;
using APIDemo2026AB.DTO.Response;
using APIDemo2026AB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIDemo2026AB.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentsCustomController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentsCustomController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("insert")]
        public void Insert(StudentRequestV1 request)
        {
            Student model = new Student
            {
                GradeIdGrade = request.GradeIdGrade,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone,
                Email = request.Email
            };

            _context.Students.Add(model);
            _context.SaveChanges();
        }

        [HttpPost("insertAll")]
        public void InsertAll(List<StudentRequestV1> request)
        {
            List<Student> model = request.Select(x => new Student
            {
                GradeIdGrade = x.GradeIdGrade,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Phone = x.Phone,
                Email = x.Email
            }).ToList();

            _context.AddRange(model);
            _context.SaveChanges();
        }

        [HttpGet("all")]
        public List<StudentResponseV1> Get()
        {
            var students = _context.Students.Include(s => s.Grade).ToList();

            List<StudentResponseV1> response = students.Select(x => new StudentResponseV1
            {
                IdStudent = x.IdStudent,
                FirstName = x.FirstName,
                LastName = x.LastName,
                GradeName = x.Grade.Name
            }).ToList();

            return response;
        }

        [HttpGet("bygrade")]
        public List<StudentResponseV1> GetByGrade(int gradeId)
        {
            var students = _context.Students
                .Include(s => s.Grade)
                .Where(x => x.GradeIdGrade == gradeId)
                .ToList();

            List<StudentResponseV1> response = students.Select(x => new StudentResponseV1
            {
                IdStudent = x.IdStudent,
                FirstName = x.FirstName,
                LastName = x.LastName,
                GradeName = x.Grade.Name
            }).ToList();

            return response;
        }
    }
}
