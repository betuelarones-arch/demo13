using System.ComponentModel.DataAnnotations.Schema;

namespace APIDemo2026AB.Models
{
    public class Student
    {
        public int IdStudent { get; set; }
        
        [Column("Grade_idGrade")]
        public int GradeIdGrade { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        
        public Grade Grade { get; set; }
    }
}
