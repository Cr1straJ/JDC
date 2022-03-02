using System;
using System.Collections.Generic;
using JDC.Common.Enums;

namespace JDC.Common.Entities
{
    public class Lesson
    {
        public int Id { get; set; }

        public string Theme { get; set; }

        public DateTime Date { get; set; }

        public string Homework { get; set; }

        public LessonDuration LessonDuration { get; set; } = LessonDuration.TwoHours;

        public int DisciplineId { get; set; }

        public Discipline Discipline { get; set; }

        public List<Grade> Grades { get; set; }
    }
}
