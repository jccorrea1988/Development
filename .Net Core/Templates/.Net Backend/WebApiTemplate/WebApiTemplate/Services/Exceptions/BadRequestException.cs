using System;

namespace WebApiTemplate.Services.Exceptions
{
    public class BadRequestException: Exception
    {
        public override string Message
        {
            get
            {
                return "Bad Request";
            }
        }
    }
}
