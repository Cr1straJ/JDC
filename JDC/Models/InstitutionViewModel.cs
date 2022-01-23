using JDC.Common.Enums;
using Microsoft.AspNetCore.Http;

namespace JDC.Models
{
    /// <summary>
    /// Institution view model.
    /// </summary>
    public class InstitutionViewModel
    {
        /// <summary>
        /// Gets or sets an institution id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets an institution name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a director id of the institution.
        /// </summary>
        public string DirectorId { get; set; }

        /// <summary>
        /// Gets or sets a link to the website of the institution.
        /// </summary>
        public string WebsiteLink { get; set; }

        /// <summary>
        /// Gets or sets an image of this institution.
        /// </summary>
        public IFormFile Image { get; set; }

        /// <summary>
        /// Gets or sets an institution type.
        /// </summary>
        public InstituteType? InstituteType { get; set; }
    }
}
