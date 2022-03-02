namespace JDC.Common.Entities
{
    /// <summary>
    /// Speciality entity.
    /// </summary>
    public class Speciality
    {
        /// <summary>
        /// Gets or sets a speciality id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a speciality name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a speciality learning duration.
        /// </summary>
        public int LearningDuration { get; set; } = 4;
    }
}
