using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.DataAccess.Interfaces
{
    public interface ILessonRepository
    {
        Task Create(Lesson lesson);
    }
}
