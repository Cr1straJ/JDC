using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using JDC.Common.Enums;

namespace JDC.Common.Entities
{
    public class EInstitution
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int UserID { get; set; }

        public User Director { get; set; }

        public string WebsiteLink { get; set; }

        public string ImageUrl { get; set; }

        public bool IsBlock { get; set; } = false;

        public InstituteType InstituteType { get; set; }

        public List<StudentGroup> Groups { get; set; } = new List<StudentGroup>();

        public List<Speciality> Specialties { get; set; } = new List<Speciality>();

        public List<Teacher> Teachers { get; set; } = new List<Teacher>();

        [NotMapped]
        public string Type
        {
            get
            {
                InstituteType type = InstituteType.College;

                return type switch
                {
                    InstituteType.Institute => "Университет",
                    InstituteType.College => "Колледж",
                    InstituteType.TechnicalSchool => "Техникум",
                    InstituteType.School => "Школа",
                    _ => "Не определено",
                };
            }

            set
            {
            }
        }
    }
}
