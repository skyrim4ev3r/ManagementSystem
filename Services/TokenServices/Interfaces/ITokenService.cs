using Project.Services.TokenServices.Models;
using Project.Core;
using Project.ProjectDataBase.Domain;
using System.Threading.Tasks;

namespace Project.Services.TokenServices.Interfaces
{
    public interface ITokenService
    {
        Task<TokenResult> CreateTokenResult(AppUser user);
    }
}
