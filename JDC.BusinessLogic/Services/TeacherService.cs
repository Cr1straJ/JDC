using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.BusinessLogic.Utilities.EmailSender;
using JDC.BusinessLogic.Utilities.PasswordGenerator;
using JDC.Common.Entities;
using JDC.DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace JDC.BusinessLogic.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository teacherRepository;
        private readonly IPasswordGenerator passwordGenerator;
        private readonly UserManager<User> userManager;
        private readonly IEmailSender emailSender;

        public TeacherService(
            IEmailSender emailSender,
            UserManager<User> userManager,
            ITeacherRepository teacherRepository,
            IPasswordGenerator passwordGenerator)
        {
            this.emailSender = emailSender;
            this.userManager = userManager;
            this.teacherRepository = teacherRepository;
            this.passwordGenerator = passwordGenerator;
        }

        public async Task<List<Teacher>> GetInstitutionTeachers(int id)
        {
            return await teacherRepository.GetInstitutionTeachers(id);
        }

        public async Task Add(Teacher teacher)
        {
            var password = passwordGenerator.GeneratePassword();
            var result = await userManager.CreateAsync(teacher.User, password);
            if (result.Succeeded)
            {
                string message = $"Login: {teacher.User.Email}<br/>Password: <strong>{password}</strong>";
                await emailSender.SendEmailAsync(teacher.User.FullName, teacher.User.Email, "Данные для входа на платформу JDC", message);
                await userManager.AddToRoleAsync(teacher.User, "Teacher");
            }

            await teacherRepository.Add(teacher);
        }

        public IEnumerable<Teacher> Find(Expression<Func<Teacher, bool>> expression)
        {
            return teacherRepository.Find(expression);
        }

        public async Task<Teacher> GetById(int id)
        {
            return await teacherRepository.GetById(id);
        }

        public async Task Remove(Teacher entity)
        {
            await teacherRepository.Remove(entity);
        }

        public async Task Update(Teacher entity)
        {
            await teacherRepository.Update(entity);
        }
    }
}
