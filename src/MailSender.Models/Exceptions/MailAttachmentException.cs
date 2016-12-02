using System;
using System.Runtime.Serialization;

namespace MailSender.Models.Exceptions
{
    [Serializable]
    public class MailAttachmentException : Exception
    {
        public MailAttachmentException()
        {
            
        }

        public MailAttachmentException(string message)
            : base(message)
        {

        }

        public MailAttachmentException(string message, Exception ex)
            : base(message, ex)
        {

        }

        // This constructor is needed for serialization.
        protected MailAttachmentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Add implementation.
        }
    }
}
