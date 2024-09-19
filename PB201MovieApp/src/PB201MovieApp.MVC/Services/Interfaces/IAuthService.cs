using PB201MovieApp.MVC.ViewModels.AuthVMs;

namespace PB201MovieApp.MVC.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponseVM> Login(UserLoginVM vm);
    void Logout();
}
