using System.ComponentModel.DataAnnotations.Schema;

namespace JDC.Common.Entities
{
    [Table("Teachers")]
    public class Teacher : AbstractUser
    {
        public int? InstitutionId { get; set; }

        public EInstitution Institution { get; set; }

        public StudentGroup Group { get; set; }
    }
}
