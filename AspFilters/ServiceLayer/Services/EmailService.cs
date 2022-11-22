using DomainLayer.Entities;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using MimeKit;
using MimeKit.Text;
using ServiceLayer.DTOs.AppUser;

using ServiceLayer.Services.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class EmailService : IEmailService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _accessor;
        private readonly LinkGenerator _generator;

        public EmailService(UserManager<AppUser> userManager, IWebHostEnvironment env, LinkGenerator generator, IHttpContextAccessor accessor)
        {
            _userManager = userManager;
            _generator = generator;
            _accessor = accessor;
            _env = env;

        }


        public void Register(RegisterDto registerDto, string link)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("TaskGov", "taskov044@gmail.com"));
            message.To.Add(new MailboxAddress(registerDto.FullName, registerDto.Email));
            message.Subject = "Confirm Email";
            string emailbody = string.Empty;

            using (StreamReader streamReader = new StreamReader(Path.Combine(_env.WebRootPath, "Templates", "Confirm.html")))
            {
                emailbody = streamReader.ReadToEnd();
            }

            emailbody = emailbody.Replace("{{code}}", $"{link}").Replace("{{fullname}}", $"{registerDto.FullName}");
            message.Body = new TextPart(TextFormat.Html) { Text = emailbody };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("taskov044@gmail.com", "qebdahsubbkhcnwt");
            smtp.Send(message);
            smtp.Disconnect(true);
        }




        public async Task ConfirmEmail(string userId, string token)
        {

            AppUser user = await _userManager.FindByIdAsync(userId);

            await _userManager.ConfirmEmailAsync(user, token);
        }

        public void OrderCreate(string email, string eventname, string seat, string hallname, string date)
        {

            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("TaskGov", "taskov044@gmail.com"));

            message.To.Add(new MailboxAddress("", email));

            message.Subject = "Bizi Seçdiyiniz Üçün Təşəkkürlər";
            string emailbody = string.Empty;
            using (StreamReader streamReader = new StreamReader(Path.Combine(_env.WebRootPath, "Templates", "Order.html")))
            {
                emailbody = streamReader.ReadToEnd();
            }


            emailbody = emailbody.Replace("{{eventname}}", $"{eventname}").Replace("{{seatid}}", $"{seat}").Replace("{{hallname}}", $"{hallname}").Replace("{{date}}", $"{date}");
            message.Body = new TextPart(TextFormat.Html) { Text = emailbody };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("taskov044@gmail.com", "qebdahsubbkhcnwt");
            smtp.Send(message);
            smtp.Disconnect(true);

        }

        public void ForgotPassword(AppUser user, string url, ForgotPasswordDto forgotPassword)
        {

            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("TaskGov", "taskov044@gmail.com"));

            message.To.Add(new MailboxAddress(user.FullName, forgotPassword.Email));
            message.Subject = "Reset Password";

            string emailbody = string.Empty;

            using (StreamReader streamReader = new StreamReader(Path.Combine(_env.WebRootPath, "Templates", "Reset.html")))
            {
                emailbody = streamReader.ReadToEnd();
            }



            emailbody = emailbody.Replace("{{fullname}}", $"{user.FullName}").Replace("{{code}}", $"{url}");

            message.Body = new TextPart(TextFormat.Html) { Text = emailbody };

            using var smtp = new SmtpClient();

            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("taskov044@gmail.com", "qebdahsubbkhcnwt");
            smtp.Send(message);
            smtp.Disconnect(true);

        }
    }
}
