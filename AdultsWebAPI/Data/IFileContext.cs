using System.Collections.Generic;
using AdultsWebAPI.Models;


namespace AdultsWebAPI.Data
{
    public interface IFileContext
    {
        public IList<Adult> Adults { get; set; }
        public void SaveChanges();
    }
}