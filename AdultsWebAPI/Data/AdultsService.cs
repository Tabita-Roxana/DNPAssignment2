using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdultsWebAPI.Models;


namespace AdultsWebAPI.Data
{
    public class AdultsService:IAdultsService
    {
        private IFileContext fileContext;

        public AdultsService(IFileContext fileContext)
        {
            this.fileContext = fileContext;
        }

        public async Task<IList<Adult>> GetAdultsAsync()
        {
            return fileContext.Adults;
        }

        public async Task<Adult> AddAdultAsync(Adult adult)
        {
            int max = fileContext.Adults.Max(a => a.Id);
            adult.Id = (++max);
            fileContext.Adults.Add(adult);
            fileContext.SaveChanges();
            return adult;
        }

        public async Task RemoveAdultAsync(int adultId)
        {
            Adult toRemove = fileContext.Adults.First(a => a.Id == adultId);
            fileContext.Adults.Remove(toRemove);
            fileContext.SaveChanges();
        }

        public async Task<Adult> EditAsync(Adult adult)
        {
            Adult toEdit = fileContext.Adults.First(a => a.Id == adult.Id);
            toEdit.Weight = adult.Weight;
            fileContext.SaveChanges();
            return toEdit;
        }

        public async Task<Adult> GetAsync(int id)
        {
            return fileContext.Adults.FirstOrDefault(a => a.Id == id);
        }
    }
    
}