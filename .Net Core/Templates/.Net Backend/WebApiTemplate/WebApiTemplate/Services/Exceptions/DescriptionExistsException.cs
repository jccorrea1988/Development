namespace WebApiTemplate.Services.Exceptions
{
    public class DescriptionExistsException : BadRequestException
    {
        public override string Message
        {
            get
            {
                return "Description already in use!";
            }
        }
    }
}
