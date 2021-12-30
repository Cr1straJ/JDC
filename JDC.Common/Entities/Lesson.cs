using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JDC.Common.Entities
{
    [Table("Lessons")]
    public class Lesson
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int StudentGroupId { get; set; }

        public Group Group { get; set; }

        public List<StudyDay> StudyDays { get; set; }
    }
}
