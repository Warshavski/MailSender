using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace MailSender.Models
{
    internal sealed class MailMessageBuilder
    {
        private readonly MailMessage _mailMessage = new MailMessage();

        public MailMessageBuilder From(string address)
        {
            _mailMessage.From = new MailAddress(address);
            return this;
        }

        public MailMessageBuilder To(string address)
        {
            _mailMessage.To.Add(address);
            return this;
        }

        public MailMessageBuilder To(IEnumerable<string> addressList)
        {
            foreach(var address in addressList)
                _mailMessage.To.Add(address);
            return this;
        }

        public MailMessageBuilder Cc(string address)
        {
            _mailMessage.CC.Add(address);
            return this;
        }

        public MailMessageBuilder Subject(string subject)
        {
            _mailMessage.Subject = subject;
            return this;
        }

        public MailMessageBuilder Body(string body, Encoding encoding)
        {
            _mailMessage.Body = body;
            _mailMessage.BodyEncoding = encoding;
            return this;
        }

        public MailMessageBuilder Attachment(string fileName, string mediaType)
        {
            try
            {
                if (fileName != string.Empty)
                    _mailMessage.Attachments.Add(new System.Net.Mail.Attachment(fileName, mediaType));
                return this;
            }
            catch (System.IO.FileNotFoundException)
            {
                throw;
            }
        }

        public MailMessage Build()
        {
            return _mailMessage;
        }
    }
}
