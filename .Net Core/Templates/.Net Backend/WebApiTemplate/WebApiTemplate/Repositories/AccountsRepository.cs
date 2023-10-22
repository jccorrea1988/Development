namespace WebApiTemplate.Repository
{
    public class AccountsRepository
    {
        private readonly ApplicationDbContext context;

        public AccountsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
    }
}
