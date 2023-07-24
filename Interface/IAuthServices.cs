using AngularApi.Modell;

namespace AngularApi.Interface
{
    public interface IAuthServices
    {
        Task<AuthModell> Registration(RegisterVM register);
        Task<AuthModell> Login(LoginVM login);
        Task<string> AddRole(RoleModel model);
    }
}
