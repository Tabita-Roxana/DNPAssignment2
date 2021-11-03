using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdultsWebAPI.Models
{
    public class Job
    {
        public string JobTitle { get; set; }
        public int Salary { get; set; }
    }
}