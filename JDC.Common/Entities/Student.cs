using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace JDC.Common.Entities
{
    /// <summary>
    /// Student entity.
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Gets or sets the student id.
        /// </summary>
        public int Id { get; set; }

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
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets a user account.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets a bool values.
        /// </summary>
        public string BoolValues { get; set; }

        /// <summary>
        /// Gets or sets a characteristics values.
        /// </summary>
        [NotMapped]
        public bool[] CharacteristicValues
        {
            get
            {
                return Array.ConvertAll(BoolValues.Split(';'), bool.Parse);
            }

            set
            {
                BoolValues = string.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }

        /// <summary>
        /// Gets or sets student grades.
        /// </summary>
        public List<Grade> Grades { get; set; } = new List<Grade>();
    }
}
