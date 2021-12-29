using System.Collections.Generic;

namespace JDC.Common.Entities
{
    public class Student
    {
        public int Id { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string House { get; set; }

        public string Apartament { get; set; }

        public int? GroupId { get; set; }

        public Group Group { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public List<Grade> Grades { get; set; } = new List<Grade>();
    }
}
