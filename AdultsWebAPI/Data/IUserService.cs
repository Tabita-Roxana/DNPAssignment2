

using System.Threading.Tasks;
using AdultsWebAPI.Models;

namespace AdultsWebAPI.Data
{
    public interface IUserService
    {
        Task<User> ValidateUserAsync(string userName, string password);
    }
}