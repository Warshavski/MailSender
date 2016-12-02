using System;
using System.Runtime.Serialization;

namespace MailSender.Models.Exceptions
{
    [Serializable]
    public class MailSenderException : Exception
    {
        public MailSenderException()
        {
            
        }

        public MailSenderException(string message)
            : base(message)
        {

        }

        public MailSenderException(string message, Exception ex)
            : base(message, ex)
        {

        }

        // This constructor is needed for serialization.
        protected MailSenderException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Add implementation.
        }
    }
}
