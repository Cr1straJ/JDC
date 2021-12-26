using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.Common.Entities;
using JDC.DataAccess.Interfaces;

namespace JDC.BusinessLogic.Services
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository gradeRepository;

        public GradeService(IGradeRepository gradeRepository)
        {
            this.gradeRepository = gradeRepository;
        }

        public async Task Add(Grade grade)
        {
            await this.gradeRepository.Add(grade);
        }
    }
}
