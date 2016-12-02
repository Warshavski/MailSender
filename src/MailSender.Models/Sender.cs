using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

using MailSender.Models.Exceptions;

namespace MailSender.Models
{
    public class MailSendEventArgs : EventArgs
    {
        public object Error { get; set; }
        public object State { get; set; }
    }

    /**
     * TODO : 
     *   1. Add async version of Send (add new version of the SmtpClient)
     */
    public sealed class Sender : ISender
    {
        /**
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

        public void Send(string subject, string body, string[] attachment)
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
        */


        // CONSTANTS SECTION
        //---------------------------------------------------------------------

        private readonly string _host;
        private readonly int _port;
        private readonly string[] _fromCredentials;
        private readonly string[] _to;



        // CONSTRUCTOR SECTION
        //---------------------------------------------------------------------

        public Sender()
        {

        }

        public Sender(string host, int port, string[] fromCredentials, string[] to)
        {
            _host = host;
            _port = port;
            _fromCredentials = fromCredentials;
            _to = to;
        }



        // PUBLIC INTERFACE METHODS SECTION
        //---------------------------------------------------------------------

        #region IMailSender members

        /// <summary>
        ///     Occurs when status of the sender is changed
        /// </summary>
        //public event Action<string> SenderNotify;

        /// <summary>
        ///    ***not used Occurs when mail send executed
        /// </summary>
        public event EventHandler<MailSendEventArgs> MailSend;

        /// <summary>
        ///     Sends e-mail message
        /// </summary>
        /// <param name="subject">the subject line for this e-mail message</param>
        /// <param name="body">The message body.</param>
        /// <param name="attachment">Path to the attached data</param>
        public void Send(string subject, string body, string[] attachment)
        {
            Send(_host, _port, _fromCredentials, _to, subject, body, attachment);
        }

        /// <summary>
        ///     Sends e-mail message
        /// </summary>
        /// <param name="host">The name or IP address of the host used for SMTP transactions.</param>
        /// <param name="port">The port used for SMTP transactions.</param>
        /// <param name="fromCredentials">The credentials used to authenticate the sender.</param>
        /// <param name="to">The address collection that contains the recipients of this e-mail message</param>
        /// <param name="subject">the subject line for this e-mail message</param>
        /// <param name="body">The message body.</param>
        /// <param name="attachment">Path to the attached data</param>
        public void Send(string host, int port, string[] fromCredentials, string[] to,
            string subject, string body, string[] attachment)
        {
            try
            {
                using (MailMessage message = CreateMailMessage(
                    fromCredentials[0], to, subject, body, attachment))
                {
                    SmtpClient smtpSender = CreateSmtpClient(host, port, fromCredentials);

                    smtpSender.Send(message);
                }
            }
            catch (IOException ex)
            {
                throw new MailAttachmentException(ex.Message, ex);
            }
            catch (SmtpException ex)
            {
                throw new MailSenderException(ex.Message, ex);
            }
            catch (TimeoutException ex)
            {
                throw new MailSenderException(ex.Message, ex);
            }
        }

        /** Async version (new version of the SmtpClient)
        public async Task SendAsync(string host, int port, string[] fromCredentials, string[] to,
            string subject, string body, string[] attachment)
        {
            try
            {
                using (MailMessage message = CreateMailMessage(
                    fromCredentials[0], to, subject, body, attachment))
                {
                    SmtpClient smtpSender = CreateSmtpClient(host, port, fromCredentials);

                    await smtpSender.SendMailAsync(message);
                }
            }
            catch (IOException ex)
            {
                throw new MailAttachmentException(ex.Message, ex);
            }
            catch (SmtpException ex)
            {
                throw new MailSenderException(ex.Message, ex);
            }
            catch (TimeoutException ex)
            {
                throw new MailSenderException(ex.Message, ex);
            }
        }
        */

        #endregion IMainSender members



        // PRIVATE HELPER METHODS SECTION
        //---------------------------------------------------------------------

        #region Private helper methods

        private MailMessage CreateMailMessage(string from, string[] to,
            string subject, string body, string[] attachment)
        {

            var message = new MailMessageBuilder()
                .From(from)
                .To(to)
                .Cc(to)
                .Subject(subject)
                .Body(body, Encoding.UTF8)
                .Attachment(attachment, MediaTypeNames.Application.Octet)
                .Build();

            return message;
        }

        private SmtpClient CreateSmtpClient(string host, int port, string[] credentials)
        {
            var smtpClient = new SmtpClientBuilder()
                .Host(host)
                .Port(port)
                .EnableSsl(true)
                .Credentials(credentials[0], credentials[1])
                .Build();

            return smtpClient;
        }
        

        /** *** not used
        private void OnMailSend(MailSendEventArgs e)
        {
            MailSend?.Invoke(this, e);
        }
        */
        #endregion Private helper methods
    }
}
