using WholesaleManagmentSys.Models.DTO;

namespace WholesaleManagmentSys.Repositrories.Abstract
{
    public interface IUserAuthService
    {
        Task<Status> LoginAsync(LoginMod model);
        Task<Status> RegistrationAsync(RegistrationMod model);
        Task<Status> LogoutAsync();

    }
}
