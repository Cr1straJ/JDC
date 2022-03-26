using System.Collections.Generic;

namespace JDC.Common.Entities
{
    /// <summary>
    /// Group entity.
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Gets or sets a group id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a group name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets an institution id.
        /// </summary>
        public int InstitutionId { get; set; }

        /// <summary>
        /// Gets or sets an institution to which the group belongs.
        /// </summary>
        public Institution Institution { get; set; }

        /// <summary>
        /// Gets or sets a teacher id.
        /// </summary>
        public int TeacherId { get; set; }

        /// <summary>
        /// Gets or sets a group mentor.
        /// </summary>
        public Teacher Teacher { get; set; }

        /// <summary>
        /// Gets or sets a speciality id.
        /// </summary>
        public int SpecialityId { get; set; }

        /// <summary>
        /// Gets or sets group speciality.
        /// </summary>
        public Speciality Speciality { get; set; }

        /// <summary>
        /// Gets or sets group students.
        /// </summary>
        public List<Student> Students { get; set; } = new List<Student>();

        /// <summary>
        /// Gets or sets group disciplines.
        /// </summary>
        public List<Discipline> Disciplines { get; set; }
    }
}
