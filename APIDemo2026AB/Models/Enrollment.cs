using System.ComponentModel.DataAnnotations.Schema;

namespace APIDemo2026AB.Models
{
    public class Enrollment
    {
        public int IdEnrollment { get; set; }
        
        [Column("Student_idStudent")]
        public int StudentIdStudent { get; set; }
        
        [Column("Course_idCourse")]
        public int CourseIdCourse { get; set; }
        
        public DateTime Date { get; set; }
        
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
