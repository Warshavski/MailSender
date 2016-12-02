using System;

namespace MailSender.Models
{
    public interface ISender
    {
        /// <summary>
        ///     Occurs when status of the sender is changed
        /// </summary>
        //event Action<string> SenderNotify;

        /// <summary>
        ///     Occurs when mail send executed
        /// </summary>
        event EventHandler<MailSendEventArgs> MailSend;

        /// <summary>
        ///     Sends e-mail message
        /// </summary>
        /// <param name="subject">the subject line for this e-mail message</param>
        /// <param name="body">The message body.</param>
        /// <param name="attachment">Path to the attached data</param>
        void Send(string subject, string body, string[] attachment);


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
        void Send(string host, int port, string[] fromCredentials, string[] to,
            string subject, string body, string[] attachment);

        /** Async version (new version of the SmtpClient)
        Task SendAsync(string host, int port, string[] fromCredentials, string[] to,
            string subject, string body, string[] attachment);
        */
    }
}
