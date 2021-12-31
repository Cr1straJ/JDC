using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.DataAccess.Interfaces
{
    public interface IGradeRepository
    {
        Task Add(Grade grade);

        Task Update(Grade grade);

        Task<Grade> GetById(int id);
    }
}
