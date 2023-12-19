namespace eBanking.Domain.Abstractions
{
    public interface ITransactionService
    {
        Task<JSONResponse> DepositAmount(int accountNumber, decimal amount);

        Task<JSONResponse> WithdrawAmount(int accountNumber, decimal amount);
    }
}
