﻿using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    /// <summary>
    /// Класс отправления email сообщения
    /// </summary>
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта BrowserGame", "ro0t.r0ot@yandex.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.yandex.ru", 25, false);
                await client.AuthenticateAsync("ro0t.r0ot@yandex.ru", "ro0t.r0ot777");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}