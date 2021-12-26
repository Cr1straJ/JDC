using System.Collections.Generic;

namespace JDC.Common.Entities
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int InstitutionId { get; set; }

        public Institution Institution { get; set; }

        public int TeacherId { get; set; }

        public Teacher Teacher { get; set; }

        public int SpecialityId { get; set; }

        public Speciality Speciality { get; set; }

        public List<Student> Students { get; set; }

        public List<Lesson> Lessons { get; set; }
    }
}
