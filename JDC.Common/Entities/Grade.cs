using System;

namespace JDC.Common.Entities
{
    public class Grade
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }

        public int StudyDayId { get; set; }

        public StudyDay StudyDay { get; set; }

        public int LessonId { get; set; }

        public double Value { get; set; }

        public string Comment { get; set; }

        public bool IsAbsent { get; set; }

        public DateTime BillingDate { get; set; } = DateTime.Now;

        public override int GetHashCode()
        {
            return int.Parse(this.BillingDate.ToString("ddMMyyyy"));
        }
    }
}
