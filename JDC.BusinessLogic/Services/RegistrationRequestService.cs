using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.BusinessLogic.Utilities.EmailSender;
using JDC.BusinessLogic.Utilities.PasswordGenerator;
using JDC.Common.Entities;
using JDC.DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace JDC.BusinessLogic.Services
{
    public class RegistrationRequestService : IRegistrationRequestService
    {
        private readonly IEmailSender emailSender;
        private readonly UserManager<User> userManager;
        private readonly IPasswordGenerator passwordGenerator;
        private readonly IInstitutionService institutionService;
        private readonly IRegistrationRequestRepository registrationRequestRepository;

        public RegistrationRequestService(
            IEmailSender emailSender,
            UserManager<User> userManager,
            IPasswordGenerator passwordGenerator,
            IInstitutionService institutionService,
            IRegistrationRequestRepository registrationRequestRepository)
        {
            this.emailSender = emailSender;
            this.userManager = userManager;
            this.passwordGenerator = passwordGenerator;
            this.institutionService = institutionService;
            this.registrationRequestRepository = registrationRequestRepository;
        }

        public async Task Accept(int id)
        {
            var request = await this.GetById(id);
            var user = (User)request;

            var password = this.passwordGenerator.GeneratePassword();
            var result = await this.userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                string message = $"Login: {request.Email}<br/>Password: <strong>{password}</strong>";
                await this.emailSender.SendEmailAsync(request.DirectorName, request.Email, "Данные для входа на платформу JDC", message);
                await this.userManager.AddToRoleAsync(user, "Director");

                await this.registrationRequestRepository.Delete(request);
            }

            await this.institutionService.Add(new Institution(user));
        }

        public async Task Create(RegistrationRequest registrationRequest)
        {
            await this.registrationRequestRepository.Create(registrationRequest);
        }

        public async Task Delete(RegistrationRequest registrationRequest)
        {
            await this.registrationRequestRepository.Delete(registrationRequest);
        }

        public async Task<List<RegistrationRequest>> GetAll()
        {
            var requests = await this.registrationRequestRepository.GetAll();

            return requests.ToList();
        }

        public async Task<RegistrationRequest> GetById(int id)
        {
            return await this.registrationRequestRepository.GetById(id);
        }

        public async Task Update(RegistrationRequest registrationRequest)
        {
            await this.registrationRequestRepository.Update(registrationRequest);
        }
    }
}
