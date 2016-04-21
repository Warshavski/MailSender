using System;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace MailSender.Models
{
    public sealed class Sender
    {
        public event Action<string> SenderNotify;

        private string _host;
        private int _port;
        private string[] _fromCredentials;
        private string[] _to;

        public Sender(string host, int port, string[] fromCredentials, string[] to)
        {
            _host = host;
            _port = port;
            _fromCredentials = fromCredentials;
            _to = to;
        }

        private void Invoke(Action<string> action, string obj)
        {
            if (action != null)
                action.Invoke(obj);
        }

        public void Send(string subject, string body, string attachment)
        {
            try
            {
                using (var message = new MailMessageBuilder()
                    .From(_fromCredentials[0])
                    .To(_to)
                    .Subject(subject)
                    .Body(body, Encoding.UTF8)
                    .Attachment(attachment, MediaTypeNames.Application.Octet)
                    .Build())
                {
                    var smtpSender = new SmtpClientBuilder()
                        .Host(_host)
                        .Port(_port)
                        .EnableSsl(true)
                        .Credentials(_fromCredentials[0], _fromCredentials[1])
                        .Build();
                    
                    smtpSender.Send(message);

                    Invoke(SenderNotify, "Send complete");
                }
            }
            catch(System.IO.FileNotFoundException ex)
            {
                Invoke(SenderNotify, "Error : " + ex.Message);
            }
            catch (SmtpException ex)
            {
                Invoke(SenderNotify, "Error : " + ex.Message);
            }
            catch (TimeoutException ex)
            {
                Invoke(SenderNotify, "Error : " + ex.Message);
            }

        }
    }
}
