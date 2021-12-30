using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JDC.Common.Entities
{
    [Table("Groups")]
    public class StudentGroup
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public int? TeacherId { get; set; }

        public Teacher Teacher { get; set; }

        public int? SpecialityId { get; set; }

        public Speciality Speciality { get; set; }

        public List<Student> Students { get; set; } = new List<Student>();

        public List<Lesson> Lessons { get; set; } = new List<Lesson>();

        public int EInstitutionID { get; set; }
    }
}
