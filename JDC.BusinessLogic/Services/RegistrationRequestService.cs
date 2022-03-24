using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.BusinessLogic.Utilities.EmailSender;
using JDC.Common.Entities;
using JDC.DataAccess.Interfaces;

namespace JDC.BusinessLogic.Services
{
    /// <inheritdoc/>
    public class RegistrationRequestService : IRegistrationRequestService
    {
        private readonly IAuthService authService;
        private readonly IEmailSender emailSender;
        private readonly IRegistrationRequestRepository registrationRequestRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationRequestService"/> class.
        /// </summary>
        /// <param name="authService">Authentication service.</param>
        /// <param name="emailSender">Email sender service.</param>
        /// <param name="registrationRequestRepository">Registration request repository.</param>
        public RegistrationRequestService(
            IAuthService authService,
            IEmailSender emailSender,
            IRegistrationRequestRepository registrationRequestRepository)
        {
            this.authService = authService;
            this.emailSender = emailSender;
            this.registrationRequestRepository = registrationRequestRepository;
        }

        /// <inheritdoc/>
        public async Task Accept(int id)
        {
            var request = await GetById(id);

            if (await authService.Register(request))
            {
                await registrationRequestRepository.Delete(request);
            }
        }

        /// <inheritdoc/>
        public async Task ConfirmEmail(int requestId)
        {
            await registrationRequestRepository.ConfirmEmail(requestId);
        }

        /// <inheritdoc/>
        public async Task Create(RegistrationRequest request)
        {
            var confirmationCode = new Random().Next(1000, 10000);
            request.ConfirmationCode = confirmationCode;
            await registrationRequestRepository.Create(request);
            await emailSender.SendEmailAsync(request.DirectorName, request.Email, "Подтверждение регистрации учреждения", $"Ваш код: {confirmationCode}");
        }

        /// <inheritdoc/>
        public async Task Delete(RegistrationRequest registrationRequest)
        {
            await registrationRequestRepository.Delete(registrationRequest);
        }

        /// <inheritdoc/>
        public RegistrationRequest FirstOrDefault(Func<RegistrationRequest, bool> predicate)
        {
            return registrationRequestRepository.FirstOrDefault(predicate);
        }

        /// <inheritdoc/>
        public async Task<List<RegistrationRequest>> GetAll()
        {
            return await registrationRequestRepository.GetAll();
        }

        /// <inheritdoc/>
        public async Task<RegistrationRequest> GetById(int requestId)
        {
            return await registrationRequestRepository.GetById(requestId);
        }
    }
}
