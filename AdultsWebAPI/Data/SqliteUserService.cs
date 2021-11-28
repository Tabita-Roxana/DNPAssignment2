using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdultsWebAPI.Models;
using AdultsWebAPI.Persistence;

namespace AdultsWebAPI.Data
{
    public class SqliteUserService:IUserService
    {
        private List<User> users;

        public SqliteUserService(AdultsContext adultsContext)
        {

            users = new []
            {
                new User
                {
                    City ="Horsens",
                    Password = "123456",
                    Age = 33,
                    Role = "Admin",
                    UserName = "User1"
                },
                new User {
                    City = "Aarhus",
                    Password = "654321",
                    Age = 45,
                    Role = "Guest",
                    UserName = "User2"
                },
                new User {
                    City = "Vejle",
                    Password = "123456",
                    Age = 15,
                    Role = "Guest",
                    UserName = "User3"
                }
            }.ToList();
            adultsContext.Users.AddRangeAsync(users);
            adultsContext.SaveChangesAsync();
        }

        public async Task<User> ValidateUserAsync(string userName, string password)
        {
            await using AdultsContext adultsContext = new AdultsContext();
            User first =  adultsContext.Users.FirstOrDefault(u => u.UserName== userName&& u.Password == password);
            if (first == null)
            {
                throw new Exception("User not found");
            }
        
            if (!first.Password.Equals(password))
            {
                throw new Exception("Incorrect password");
            }
            return first;
        }
       
    }
}