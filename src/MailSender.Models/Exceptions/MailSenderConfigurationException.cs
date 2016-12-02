using System;
using System.Runtime.Serialization;

namespace MailSender.Models.Exceptions
{
    [Serializable]
    public class MailSenderConfigurationException : Exception
    {
        public MailSenderConfigurationException()
        {
            
        }

        public MailSenderConfigurationException(string message)
            : base(message)
        {

        }

        public MailSenderConfigurationException(string message, Exception ex)
            : base(message, ex)
        {

        }

        // This constructor is needed for serialization.
        protected MailSenderConfigurationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Add implementation.
        }
    }
}
