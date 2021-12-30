using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace JDC.Common.Entities
{
    [Table("Grades")]
    public class Grade
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }

        public int StudyDayId { get; set; }

        public StudyDay StudyDay { get; set; }

        public int LessonId { get; set; }

        public double Value { get; set; }

        public DateTime BillingDate { get; set; } = DateTime.Now;

        public override int GetHashCode()
        {
            return int.Parse(this.BillingDate.ToString("ddMMyyyy"));
        }
    }
}
