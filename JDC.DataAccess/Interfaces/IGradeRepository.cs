using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.DataAccess.Interfaces
{
    public interface IGradeRepository
    {
        Task Add(Grade grade);
    }
}
