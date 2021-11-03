using System.Collections.Generic;
using System.Threading.Tasks;
using AdultsWebAPI.Models;


namespace AdultsWebAPI.Data
{
    public interface IAdultsService
    {
        Task<IList<Adult>> GetAdultsAsync();
        Task<Adult> AddAdultAsync(Adult adult);
        Task RemoveAdultAsync(int adultId);
        Task<Adult> EditAsync(Adult adult);
        Task<Adult> GetAsync(int id);
    }
}