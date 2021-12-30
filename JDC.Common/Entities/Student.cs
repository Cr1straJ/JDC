using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JDC.Common.Entities
{
    [Table("Students")]
    public class Student
    {
        public int Id { get; set; }
      
        public User User { get; set; }
        
        public string City { get; set; }

        public string Street { get; set; }

        public string House { get; set; }

        public string Apartament { get; set; }

        public int? GroupId { get; set; }

        public StudentGroup Group { get; set; }

        public List<Grade> Grades { get; set; }
    }
}
