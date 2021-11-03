using System.Collections.Generic;
using System.Threading.Tasks;
using Assignment1.Models;


namespace Assignment1.Data
{
    public interface IAdultsData
    {
        Task<IList<Adult>> GetAdultsAsync();
        Task<Adult> AddAdultAsync(Adult adult);
        Task RemoveAdultAsync(int adultId);
        Task<Adult> EditAsync(Adult adult);
        Task<Adult> GetAsync(int id);

    }
}