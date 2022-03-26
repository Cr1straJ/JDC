using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.BusinessLogic.Interfaces
{
    /// <summary>
    /// Group service.
    /// </summary>
    public interface IStudentService
    {
        /// <summary>
        /// Gets student by Id.
        /// </summary>
        /// <param name="studentId">Student id.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<Student> GetById(int studentId);

        /// <summary>
        /// Creates student.
        /// </summary>
        /// <param name="student">Student request information.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Add(Student student);

        /// <summary>
        /// Deletes a student.
        /// </summary>
        /// <param name="student">Student request information.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Remove(Student student);

        /// <summary>
        /// Edits student information.
        /// </summary>
        /// <param name="student">Student request information.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Update(Student student);
    }
}
