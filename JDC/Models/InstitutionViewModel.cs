using JDC.Common.Enums;
using Microsoft.AspNetCore.Http;

namespace JDC.Models
{
    public class InstitutionViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DirectorId { get; set; }

        public string WebsiteLink { get; set; }

        public IFormFile Image { get; set; }

        public InstituteType? InstituteType { get; set; }
    }
}
