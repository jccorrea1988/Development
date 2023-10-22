namespace WebApiTemplate.Services.Exceptions
{
    public class MissingDataException : Exception
    {
        public override string Message
        {
            get
            {
                return "Missing Data";
            }
        }
    }
}
