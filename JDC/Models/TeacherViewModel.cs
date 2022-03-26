using JDC.Common.Entities;

namespace JDC.Models
{
    /// <summary>
    /// Teacher view model.
    /// </summary>
    public class TeacherViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TeacherViewModel"/> class.
        /// </summary>
        public TeacherViewModel()
        {
        }

        /// <inheritdoc cref="TeacherViewModel()"/>
        /// <param name="teacher">Teacher entity.</param>
        public TeacherViewModel(Teacher teacher)
        {
            Id = teacher.Id;
            Email = teacher.User.Email;
            LastName = teacher.User.LastName;
            FirstName = teacher.User.FirstName;
            MiddleName = teacher.User.MiddleName;
            GroupId = teacher.GroupId;
        }

        /// <summary>
        /// Gets or sets teacher id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets teacher email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets teacher lastname.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets teacher firstname.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets teacher middlename.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets teacher group id.
        /// </summary>
        public int? GroupId { get; set; }

        /// <summary>
        /// Gets or sets an institution id.
        /// </summary>
        public int InstitutionId { get; set; }
    }
}
