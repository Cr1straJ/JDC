﻿using System.Collections.Generic;

namespace JDC.Common.Entities
{
    public class Lesson
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int GroupId { get; set; }

        public Group Group { get; set; }

        public List<StudyDay> StudyDays { get; set; }
    }
}
