using System;

namespace JDC.Common.Entities
{
    /// <summary>
    /// Grade entity.
    /// </summary>
    public class Grade
    {
        /// <summary>
        /// Gets or sets a grade id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a student id.
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// Gets or sets a student who got the grade.
        /// </summary>
        public Student Student { get; set; }

        /// <summary>
        /// Gets or sets a lesson id.
        /// </summary>
        public int LessonId { get; set; }

        /// <summary>
        /// Gets or sets a lesson for which the grade was received.
        /// </summary>
        public Lesson Lesson { get; set; }

        /// <summary>
        /// Gets or sets a value.
        /// </summary>
        public double? Value { get; set; }

        /// <summary>
        /// Gets or sets a comment.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether that a student is absent.
        /// </summary>
        public bool IsAbsent { get; set; }

        /// <summary>
        /// Gets or sets a date of grading.
        /// </summary>
        public DateTime BillingDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets the date of the grading in number format.
        /// </summary>
        /// <returns>Grading date in number format.</returns>
        public int GetNumericDate()
        {
            return int.Parse(this.BillingDate.ToString("ddMMyyyy"));
        }
    }
}
