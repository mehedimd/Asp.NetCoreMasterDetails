using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreMasterDetailAwesome.Models
{
    public class Faculty
    {
        public Faculty()
        {
            this.Students = new List<Student>();
        }
        public int FacultyID { get; set; }
        public string FacultyName { get; set; } 
        public string CourseName { get; set; }
        [DataType(DataType.Date)]
        public string CourseStartDate { get; set; }
        public string? PicPath { get; set; }
        [NotMapped]
        public IFormFile? Picture { get; set; }
        public virtual List<Student> Students { get; set; }
    }
}
