using eBanking.Domain.Models;

namespace eBanking.Domain.Abstractions
{
    public interface IAccountService
    {
        Task<JSONResponse> OpenAccount(AccountDTO accountDTO);

        Task<JSONResponse> CloseAccount(int AccountNumber);

        Task<JSONResponse> AllAccountInformation();

        Task<JSONResponse> AccountInformation(int AccountNumber);
    }
}
