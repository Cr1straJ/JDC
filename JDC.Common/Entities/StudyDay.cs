using System.Collections.Generic;
using JDC.Common.Enums;

namespace JDC.Common.Entities
{
    public class StudyDay
    {
        public int Id { get; set; }

        public string Theme { get; set; }

        public string Homework { get; set; }

        public LessonDuration LessonDuration { get; set; } = LessonDuration.TwoHours;

        public int LessonId { get; set; }

        public Lesson Lesson { get; set; }

        public List<Grade> Grades { get; set; }
    }
}
