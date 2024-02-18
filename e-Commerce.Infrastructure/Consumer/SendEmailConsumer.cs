using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using e_Commerce.Infrastructure.Dtos;
using e_Commerce.Application.Configuration;

namespace e_Commerce.Infrastructure.Consumer
{
    public class SendEmailConsumer : IConsumer<ISendEmailDto>
    {
        private readonly SmtpSettings _smtpSettings;

        public SendEmailConsumer(SmtpSettings smtpSettings)
        {
            _smtpSettings = smtpSettings;
        }

        public Task Consume(ConsumeContext<ISendEmailDto> context)
        {
            var email = context.Message.Email;
            var body = context.Message.Body;
            var subject = context.Message.Subject;
            var isHtml = context.Message.IsHtml;


            SmtpClient sc = new SmtpClient();
            sc.Port = _smtpSettings.Port;
            sc.Host = _smtpSettings.Host;
            sc.EnableSsl = true;

            sc.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(_smtpSettings.Username);

            mail.To.Add(email);
            mail.Subject = subject;
            mail.IsBodyHtml = isHtml;
            mail.Body = body;

            sc.Send(mail);

            return Task.CompletedTask;
        }
    }
}
