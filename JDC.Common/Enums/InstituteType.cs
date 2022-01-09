using System.ComponentModel;

namespace JDC.Common.Enums
{
    public enum InstituteType
    {
        /// <summary>
        /// The meaning sets the institution as an institute
        /// </summary>
        [Description("Университет")]
        Institute,

        /// <summary>
        /// The meaning sets the institution as a college
        /// </summary>
        [Description("Колледж")]
        College,

        /// <summary>
        /// The significance of establishing an institution as a technical college
        /// </summary>
        [Description("Техникум")]
        TechnicalSchool,

        /// <summary>
        /// The meaning sets the institution as a school
        /// </summary>
        [Description("Школа")]
        School,
    }
}
