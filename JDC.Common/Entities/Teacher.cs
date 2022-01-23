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
        /// Gets or sets a user id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets a user account.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets a teacher group.
        /// </summary>
        public Group Group { get; set; }
    }
}
