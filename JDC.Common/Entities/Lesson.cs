using System;
using System.Collections.Generic;
using JDC.Common.Enums;

namespace JDC.Common.Entities
{
    /// <summary>
    /// Lesson entity.
    /// </summary>
    public class Lesson
    {
        /// <summary>
        /// Gets or sets a lesson id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a lesson theme.
        /// </summary>
        public string Theme { get; set; }

        /// <summary>
        /// Gets or sets a lesson date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets a homework.
        /// </summary>
        public string Homework { get; set; }

        /// <summary>
        /// Gets or sets a lesson duration.
        /// </summary>
        public LessonDuration LessonDuration { get; set; } = LessonDuration.TwoHours;

        /// <summary>
        /// Gets or sets a discipline id that includes a lesson.
        /// </summary>
        public int DisciplineId { get; set; }

        /// <summary>
        /// Gets or sets a discipline.
        /// </summary>
        public Discipline Discipline { get; set; }

        /// <summary>
        /// Gets or sets a group id that passed in class.
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Gets or sets a group.
        /// </summary>
        public Group Group { get; set; }

        /// <summary>
        /// Gets or sets lesson grades.
        /// </summary>
        public List<Grade> Grades { get; set; }
    }
}
