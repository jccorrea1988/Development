using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorPeliculasNet7.Client.Auth
{
    public class ProveedorAutenticacionPrueba : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var anonimo = new ClaimsIdentity();
            var usuarioJose = new ClaimsIdentity(
                new List<Claim>
                {
                    new Claim("llave1", "valor1"),
                    new Claim("edad", "999"),
                    new Claim(ClaimTypes.Name, "Jose"),
                    //new Claim(ClaimTypes.Role, "admin")
                }
                ,authenticationType:"prueba");
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(usuarioJose)));
        }
    }
}
