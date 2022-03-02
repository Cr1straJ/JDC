using System.Collections.Generic;

namespace JDC.Common.Entities
{
    /// <summary>
    /// Student entity.
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Gets or sets a student id.
        /// </summary>
        public int Id { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string House { get; set; }

        public string Apartament { get; set; }

        /// <summary>
        /// Gets or sets the group id to which the student belongs.
        /// </summary>
        public int? GroupId { get; set; }

        /// <summary>
        /// Gets or sets the group to which the student belongs.
        /// </summary>
        public Group Group { get; set; }

        /// <summary>
        /// Gets or sets a user id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets a user account.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets student grades.
        /// </summary>
        public List<Grade> Grades { get; set; } = new List<Grade>();
    }
}
