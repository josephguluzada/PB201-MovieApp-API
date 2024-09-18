using PB201MovieApp.Business.DTOs.TokenDtos;
using PB201MovieApp.Business.DTOs.UserDtos;

namespace PB201MovieApp.Business.Services.Interfaces;

public interface IAuthService
{
    Task Register(UserRegisterDto dto);
    Task<TokenResponseDto> Login(UserLoginDto dto);
}
