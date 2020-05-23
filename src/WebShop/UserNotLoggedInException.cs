using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WebShop
{
    public class UserNotLoggedInException : Exception
    {
        public UserNotLoggedInException():base()
        {
        }

        public UserNotLoggedInException(string message) : base(message)
        {
        }

        public UserNotLoggedInException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserNotLoggedInException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
