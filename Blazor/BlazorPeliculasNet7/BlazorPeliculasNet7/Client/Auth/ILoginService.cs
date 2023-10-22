using BlazorPeliculasNet7.Shared.DTOs;

namespace BlazorPeliculasNet7.Client.Auth
{
    public interface ILoginService
    {
        Task Login(UserTokenDTO tokenDTO);
        Task Logout();
        Task ManejarRenovacionToken();
    }
}
