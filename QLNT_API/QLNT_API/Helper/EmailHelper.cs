using System.Net.Mail;

namespace QLNT_API.Helper
{
    public class EmailHelper
    {
        private static readonly string SmtpHost = "smtp.gmail.com"; // Thay bằng SMTP server của bạn
        private static readonly int SmtpPort = 587; // Cổng SMTP
        private static readonly string FromEmail = "your-email@gmail.com"; // Email gửi
        private static readonly string Password = "your-app-password"; // Mật khẩu ứng dụng nếu dùng Gmail

        public static async Task SendAsync(string toEmail, string subject, string body)
        {
            using (var smtpClient = new SmtpClient(SmtpHost, SmtpPort))
            {
                smtpClient.EnableSsl = true; // Bật SSL cho Gmail
                smtpClient.Credentials = new System.Net.NetworkCredential(FromEmail, Password);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(FromEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false // Đặt true nếu body là HTML
                };
                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}
