using System.Threading.Tasks;
using Assignment1.Models;

namespace Assignment1.Data
{
    public interface IUserService
    {
        Task<User> ValidateUserAsync(string userName, string password);
    }
}