namespace WebApiTemplate.Services.Exceptions
{
    public class EntityNotFoundException : MissingDataException
    {
        public override string Message
        {
            get
            {
                return "Couldn't find an entity with the solicited id!";
            }
        }
    }
}
