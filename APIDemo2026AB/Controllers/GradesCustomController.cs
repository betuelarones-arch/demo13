using APIDemo2026AB.DTO.Request;
using APIDemo2026AB.DTO.Response;
using APIDemo2026AB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIDemo2026AB.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GradesCustomController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GradesCustomController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("insert")]
        public void Insert(GradeRequestV1 request)
        {
            Grade model = new Grade
            {
                Name = request.Name,
                Description = request.Description,
                isActive = true
            };

            _context.Grades.Add(model);
            _context.SaveChanges();
        }

        [HttpPost("insertAll")]
        public void InsertAll(List<GradeRequestV1> request)
        {
            List<Grade> model = request.Select(x => new Grade
            {
                Name = x.Name,
                Description = x.Description,
                isActive = true
            }).ToList();

            _context.AddRange(model);
            _context.SaveChanges();
        }

        [HttpGet("all")]
        public List<GradeResponseV1> Get()
        {
            var grades = _context.Grades.Where(x => x.isActive == true).ToList();

            List<GradeResponseV1> response = grades.Select(x => new GradeResponseV1
            {
                IdGrade = x.IdGrade,
                Name = x.Name,
                Description = x.Description
            }).ToList();

            return response;
        }

        [HttpGet("byname")]
        public List<GradeResponseV1> GetByName(string name)
        {
            var grades = _context.Grades.Where(x => x.isActive == true && x.Name.Contains(name)).ToList();

            List<GradeResponseV1> response = grades.Select(x => new GradeResponseV1
            {
                IdGrade = x.IdGrade,
                Name = x.Name,
                Description = x.Description
            }).ToList();

            return response;
        }
    }
}
