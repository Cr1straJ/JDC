using System.Collections.Generic;

namespace JDC.Common.Entities
{
    /// <summary>
    /// Discipline entity.
    /// </summary>
    public class Discipline
    {
        /// <summary>
        /// Gets or sets a discipline id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a discipline title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a group id.
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Gets or sets a group that study the discipline.
        /// </summary>
        public Group Group { get; set; }

        /// <summary>
        /// Gets or sets a list of lessons.
        /// </summary>
        public List<Lesson> Lessons { get; set; }
    }
}
