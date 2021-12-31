using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.BusinessLogic.Interfaces
{
    public interface ILessonService
    {
        Task Create(Lesson lesson);
    }
}
