using System.ComponentModel.DataAnnotations.Schema;

namespace JDC.Common.Entities
{
    [Table("Teachers")]
    public class Teacher
    {
        public int Id { get; set; }

        public User User { get; set; }

        public int? InstitutionId { get; set; }

        public Institution Institution { get; set; }
    }
}
