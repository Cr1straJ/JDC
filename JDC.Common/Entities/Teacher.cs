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
        /// Gets or sets a group id.
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Gets or sets a user account.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets an institution id.
        /// </summary>
        public int? InstitutionId { get; set; }

        /// <summary>
        /// Gets or sets an institution.
        /// </summary>
        public Institution Institution { get; set; }
    }
}
