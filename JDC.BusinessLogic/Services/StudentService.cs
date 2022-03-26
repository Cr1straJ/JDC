using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.Common.Entities;
using JDC.DataAccess.Interfaces;

namespace JDC.BusinessLogic.Services
{
    /// <inheritdoc/>
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository studentRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentService"/> class.
        /// </summary>
        /// <param name="studentRepository">Student repository.</param>
        public StudentService(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        /// <inheritdoc/>
        public async Task Add(Student student)
        {
            await studentRepository.Add(student);
        }

        /// <inheritdoc/>
        public async Task<Student> GetById(int studentId)
        {
            return await studentRepository.GetById(studentId);
        }

        /// <inheritdoc/>
        public async Task Remove(Student student)
        {
            await studentRepository.Remove(student);
        }

        /// <inheritdoc/>
        public async Task Update(Student student)
        {
            await studentRepository.Update(student);
        }
    }
}
