using System.ComponentModel.DataAnnotations.Schema;

namespace CoreMasterDetailAwesome.Models
{
    public class Student
    {
        public int StudentID {  get; set; }
        public string StudentName { get; set; } = "";
        public string Address { get; set; } = "";

        public int FacultyID {  get; set; }
        public virtual Faculty? Faculty { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; } = false;
        
    }
}
