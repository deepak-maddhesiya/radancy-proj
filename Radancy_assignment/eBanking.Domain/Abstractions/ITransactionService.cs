namespace eBanking.Domain.Abstractions
{
    public interface ITransactionService
    {
        Account DepositAmount(int accountNumber, decimal amount);

        Account WithdrawAmount(int accountNumber, decimal amount);
    }
}
