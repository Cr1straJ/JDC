using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using JDC.Common.Enums;
using JDC.Common.Extensions;

namespace JDC.Common.Entities
{
    /// <summary>
    /// Institution entity.
    /// </summary>
    public class Institution
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Institution"/> class.
        /// </summary>
        public Institution()
        {
        }

        /// <inheritdoc cref="Institution()"/>
        /// <param name="director">Director of the institution.</param>
        public Institution(User director)
        {
            this.Director = director;
        }

        /// <summary>
        /// Gets or sets an institution id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets an institution name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a director id.
        /// </summary>
        public string DirectorId { get; set; }

        /// <summary>
        /// Gets or sets an institution director.
        /// </summary>
        public User Director { get; set; }

        /// <summary>
        /// Gets or sets a link to the website of the institution.
        /// </summary>
        public string WebsiteLink { get; set; }

        /// <summary>
        /// Gets or sets an URL of the image of this institution.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets an institution type.
        /// </summary>
        public InstituteType? InstituteType { get; set; }

        /// <summary>
        /// Gets or sets institution groups.
        /// </summary>
        public List<Group> Groups { get; set; }

        /// <summary>
        /// Gets or sets institution students.
        /// </summary>
        public List<Student> Students { get; set; }

        /// <summary>
        /// Gets or sets institution teachers.
        /// </summary>
        public List<Teacher> Teachers { get; set; }

        /// <summary>
        /// Gets or sets institution specialities.
        /// </summary>
        public List<Speciality> Specialities { get; set; }

        /// <summary>
        /// Gets the type of institution in a written format.
        /// </summary>
        [NotMapped]
        public string Type => this.InstituteType?.GetDescription() ?? "Не определено";
    }
}
