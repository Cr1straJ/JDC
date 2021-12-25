using System;
using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.BusinessLogic.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace JDC.BusinessLogic.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpClientSettings smtpClientSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailSender"/> class.
        /// </summary>
        /// <param name="smtpClientSettings">Settings for the smtpclient.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="smtpClientSettings"/> is null.
        /// </exception>
        public EmailSender(SmtpClientSettings smtpClientSettings)
        {
            if (smtpClientSettings is null)
            {
                throw new ArgumentNullException(nameof(smtpClientSettings));
            }

            this.smtpClientSettings = smtpClientSettings;
        }

        /// <summary>
        /// Asynchronously send the specified message.
        /// </summary>
        /// <param name="name">Name of the addressee.</param>
        /// <param name="email">Email of the addressee.</param>
        /// <param name="subject">Message subject.</param>
        /// <param name="message">Contents of the message.</param>
        /// <returns>The expected task.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="email"/> is null.
        /// </exception>
        public async Task SendEmailAsync(string name, string email, string subject, string message)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email has an invalid value.", nameof(email));
            }

            var emailMessage = new MimeMessage
            {
                Subject = subject,
                Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = message,
                },
            };

            emailMessage.From.Add(new MailboxAddress(this.smtpClientSettings.Name, this.smtpClientSettings.Email));
            emailMessage.To.Add(new MailboxAddress(name, email));

            SmtpClient client = new SmtpClient();
            await client.ConnectAsync(this.smtpClientSettings.Host, this.smtpClientSettings.Port, true);
            await client.AuthenticateAsync(this.smtpClientSettings.Email, this.smtpClientSettings.Password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
