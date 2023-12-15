namespace eBanking.Domain.Abstractions
{
    public interface IAccountService
    {
        Account OpenAccount(string FirstName, string LastName, decimal depositAmount);

        Account CloseAccount(int AccountNumber);

        List<Account> AllAccountInformation();

        Account AccountInformation(int AccountNumber);
    }
}
