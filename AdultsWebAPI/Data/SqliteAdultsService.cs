using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdultsWebAPI.Models;
using AdultsWebAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AdultsWebAPI.Data
{
    public class SqliteAdultsService: IAdultsService

    {
        private AdultsContext adultsContext;

        public SqliteAdultsService(AdultsContext adultsContext)
        {
            this.adultsContext = adultsContext;
        }

        public async Task<IList<Adult>> GetAdultsAsync()
        {
           return await adultsContext.Adults.Include("JobTitle").ToListAsync();
        }

        public async Task<Adult> AddAdultAsync(Adult adult)
        {
            EntityEntry<Adult> adults = await adultsContext.AddAsync(adult);
            await adultsContext.SaveChangesAsync();
            return adults.Entity;
        }

        public async Task RemoveAdultAsync(int adultId)
        {
            Adult adult = await adultsContext.Adults.FirstOrDefaultAsync(a => a.Id == adultId);
            if (adult != null)
            {
                adultsContext.Adults.Remove(adult);
                await adultsContext.SaveChangesAsync();
            }
        }

        public async Task<Adult> EditAsync(Adult adult)
        {

            try
            {
                Adult adultToEdit = await adultsContext.Adults.Include("JobTitle").FirstAsync(a => a.Id == adult.Id);
                adultToEdit.FirstName = adult.FirstName;
                adultToEdit.LastName = adult.LastName;
                adultToEdit.Weight = adult.Weight;
                adultsContext.Adults.Update(adultToEdit);
                await adultsContext.SaveChangesAsync();
                return adultToEdit;
            }
            catch (Exception e)
            {
                throw new Exception($"Did not find adult with id{adult.Id}");
            }
        }

        public async Task<Adult> GetAsync(int id)
        {
            try
            {
                Adult adultById = await adultsContext.Adults.Include("JobTitle").FirstOrDefaultAsync(a => a.Id == id);
                await adultsContext.SaveChangesAsync();
                return adultById;
            }
            catch (Exception e)
            {
                throw new Exception($"Did not find adult with id{id}");
            }
        }
    }
}