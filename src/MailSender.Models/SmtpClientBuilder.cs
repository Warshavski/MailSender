using System;
using System.Net;
using System.Net.Mail;

namespace MailSender.Models
{
    /**
     *  TODO :
     *    1. In new version add IDisposable implementation.
     */
    internal sealed class SmtpClientBuilder 
    {
        // CONSTANTS SECTION
        //---------------------------------------------------------------------

        private readonly SmtpClient _smtpClient = new SmtpClient();



        // CONSTRUCTOR SECTION
        //---------------------------------------------------------------------

        #region SmtpClientBuider constructors

        /// <summary>
        ///     Sets the name or IP address of the host used for SMTP transactions.
        /// </summary>
        /// <param name="host">host(name or ip)...</param>
        /// <returns>SmtpClientBuilder</returns>
        public SmtpClientBuilder Host(string host)
        {
            if (string.IsNullOrEmpty(host))
            {
                throw new ArgumentNullException("host");
            }

            _smtpClient.Host = host;
            return this;
        }

        /// <summary>
        ///      Sets the port used for SMTP transactions.
        /// </summary>
        /// <param name="port">port number...</param>
        /// <returns>SmtpClientBuilder</returns>
        public SmtpClientBuilder Port(int port)
        {
            if (port < 0)
            {
                throw new ArgumentOutOfRangeException("port");
            }

            _smtpClient.Port = port;
            return this;
        }

        /// <summary>
        ///     Sets the credentials used to authenticate the sender.
        /// </summary>
        /// <param name="address">E-mail address</param>
        /// <param name="password">Password...</param>
        /// <returns>SmtpClientBuilder</returns>
        public SmtpClientBuilder Credentials(string address, string password)
        {
            if (string.IsNullOrEmpty(address))
            {
                throw new ArgumentNullException("address");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password");
            }

            _smtpClient.Credentials =
                new NetworkCredential(address, password);
            return this;
        }

        /// <summary>
        ///     Specify whether the SmtpClient uses Secure Sockets Layer (SSL) to encrypt the connection.
        /// </summary>
        /// <param name="isSslEnable">Enable(true) / Disable(false)</param>
        /// <returns>SmtpClientBuilder</returns>
        public SmtpClientBuilder EnableSsl(bool isSslEnable)
        {
            _smtpClient.EnableSsl = isSslEnable;
            return this;
        }

        #endregion SmtpClientBuider constructors



        // PUBLIC INTERFACE METHODS SECTION
        //---------------------------------------------------------------------

        /// <summary>
        ///     Builds SMTP client
        /// </summary>
        /// <returns>SmtpClient</returns>
        public SmtpClient Build()
        {
            return _smtpClient;
        }

        /**
        #region IDisposable members

        /// <summary>
        ///     Release all resources used by the <value>SmtpClientBuilder</value>
        /// </summary>
        public void Dispose()
        {
            if (_smtpClient != null)
            {
                _smtpClient
            }
        }

        #endregion
        */
    }
}
