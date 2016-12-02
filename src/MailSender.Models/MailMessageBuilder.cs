using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace MailSender.Models
{
    internal sealed class MailMessageBuilder  : IDisposable
    {
        // CONSTANTS SECTION
        //---------------------------------------------------------------------

        private readonly MailMessage _mailMessage = new MailMessage();



        //CONSTRUCTOR SECTION
        //---------------------------------------------------------------------

        #region MailMessageBuilder constructors

        /// <summary>
        ///      Sets the from address for this e-mail message
        /// </summary>
        /// <param name="address">The from e-mail address</param>
        /// <returns>MailMessageBuilder</returns>
        public MailMessageBuilder From(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                throw new ArgumentNullException("address");
            }

            _mailMessage.From = new MailAddress(address);
            return this;
        }

        /// <summary>
        ///     Sets the address of the recipient of this e-mail message
        /// </summary>
        /// <param name="address">Recipient e-mail address</param>
        /// <returns>MailMessageBuilder</returns>
        public MailMessageBuilder To(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                throw new ArgumentNullException("address");
            }

            _mailMessage.To.Add(address);
            return this;
        }

        /// <summary>
        ///     Sets the address collection that contains the recipients of this e-mail message
        /// </summary>
        /// <param name="addressList">Collection e-mail address of the recipients</param>
        /// <returns>MailMessageBuilder</returns>
        public MailMessageBuilder To(IEnumerable<string> addressList)
        {
            if (addressList == null)
            {
                throw new ArgumentNullException("addressList");
            }

            foreach (var address in addressList)
            {
                if (string.IsNullOrEmpty(address))
                {
                    throw new ArgumentNullException("addressList");
                }

                _mailMessage.To.Add(address);
            }

            return this;
        }

        /// <summary>
        ///      Sets the address of the carbon copy (CC) recipient of this e-mail message
        /// </summary>
        /// <param name="address">Recipient e-mail address</param>
        /// <returns>MailMessageBuilder</returns>
        public MailMessageBuilder Cc(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                throw new ArgumentNullException("address");
            }

            _mailMessage.CC.Add(address);
            return this;
        }

        /// <summary>
        ///     Sets the address collection that contains the carbon copy (CC) recipients for this e-mail message.
        /// </summary>
        /// <param name="addressList">Collection e-mail address of the recipients</param>
        /// <returns>MailMessageBuilder</returns>
        public MailMessageBuilder Cc(IEnumerable<string> addressList)
        {
            if (addressList == null)
            {
                throw new ArgumentNullException("addressList");
            }

            foreach (var address in addressList)
            {
                if (string.IsNullOrEmpty(address))
                {
                    throw new ArgumentNullException("addressList");
                }

                _mailMessage.CC.Add(address);
            }

            return this;
        }

        /// <summary>
        ///     Sets the subject line for this e-mail message.
        /// </summary>
        /// <param name="subject">Subject line text</param>
        /// <returns>MailMessageBuilder</returns>
        public MailMessageBuilder Subject(string subject)
        {
            _mailMessage.Subject = subject ?? string.Empty;
            return this;
        }

        /// <summary>
        ///     Sets the message body.
        /// </summary>
        /// <param name="body">Message body text</param>
        /// <param name="encoding">Message body text encoding</param>
        /// <returns>MailMessageBuilder</returns>
        public MailMessageBuilder Body(string body, Encoding encoding)
        {
            _mailMessage.Body = body ?? string.Empty;
            _mailMessage.BodyEncoding = encoding ?? Encoding.Default;
            return this;
        }

        /// <summary>
        ///     Sets the attachment used to store data attached to this e-mail message.
        /// </summary>
        /// <param name="fileName">Data attached to the e-mail message</param>
        /// <param name="mediaType">Media type of the attached data</param>
        /// <returns>MailMessageBuilder</returns>
        public MailMessageBuilder Attachment(string fileName, string mediaType)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }

            _mailMessage.Attachments.Add(new Attachment(fileName, mediaType));

            return this;
        }

        /// <summary>
        ///     Sets the attachment collection used to store data attached to this e-mail message.
        /// </summary>
        /// <param name="filesNames">Data attached(collection) to the e-mail message</param>
        /// <param name="mediaType">Media type of the attached data</param>
        /// <returns>MailMessageBuilder</returns>
        public MailMessageBuilder Attachment(IEnumerable<string> filesNames, string mediaType)
        {
            if (filesNames == null)
            {
                throw new ArgumentNullException("filesNames");
            }

            foreach (var item in filesNames)
            {
                if (string.IsNullOrEmpty(item))
                {
                    throw new ArgumentNullException("filesNames");
                }

                _mailMessage.Attachments.Add(new Attachment(item, mediaType));
            }

            return this;
        }

        #endregion



        // PUBLIC INTERFACE METHODS SECTION
        //---------------------------------------------------------------------

        /// <summary>
        ///     Builds MailMessage
        /// </summary>
        /// <returns>MailMessage</returns>
        public MailMessage Build()
        {
            return _mailMessage;
        }

        #region IDisposable members

        /// <summary>
        ///     Releases all resources used by the <value>MailMessageBuilder</value>
        /// </summary>
        public void Dispose()
        {
            if (_mailMessage != null)
            {
                _mailMessage.Dispose();
            }
        }

        #endregion
    }
}
