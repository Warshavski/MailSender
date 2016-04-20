using System.Net;
using System.Net.Mail;

namespace MailSender.Models
{
    internal sealed class SmtpClientBuilder
    {
        private readonly SmtpClient _smtpClient = new SmtpClient();

        public SmtpClientBuilder Host(string host)
        {
            _smtpClient.Host = host;
            return this;
        }

        public SmtpClientBuilder Port(int port)
        {
            _smtpClient.Port = port;
            return this;
        }

        public SmtpClientBuilder Credentials(string address, string password)
        {
            _smtpClient.Credentials = 
                new NetworkCredential(address, password);
            return this;
        }

        public SmtpClientBuilder EnableSsl(bool isSslEnable)
        {
            _smtpClient.EnableSsl = isSslEnable;
            return this;
        }

        public SmtpClient Build()
        {
            return _smtpClient;
        }
    }
}
