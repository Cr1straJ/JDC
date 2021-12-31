using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.BusinessLogic.Interfaces
{
    public interface IGradeService
    {
        Task Add(Grade grade);

        Task<Grade> GetById(int id);

        Task Update(Grade grade);
    }
}
