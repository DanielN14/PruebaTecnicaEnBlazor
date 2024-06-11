using PruebaTecnica.App.Models;

namespace PruebaTecnica.App.Services.Security.Interfaces
{
    public interface IAuthService
    {
        Task LoginAsync(SesionUsuario sesionUsuario);
        Task LogoutAsync();
    }
}
