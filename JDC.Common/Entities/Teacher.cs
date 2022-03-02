namespace JDC.Common.Entities
{
    /// <summary>
    /// Teacher entity.
    /// </summary>
    public class Teacher
    {
        /// <summary>
        /// Gets or sets a teacher id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a user account.
        /// </summary>
        public User User { get; set; }

        public int? InstitutionId { get; set; }

        public Institution Institution { get; set; }
    }
}
