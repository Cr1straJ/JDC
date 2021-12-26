using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.BusinessLogic.Interfaces
{
    public interface IGradeService
    {
        Task Add(Grade grade);
    }
}
