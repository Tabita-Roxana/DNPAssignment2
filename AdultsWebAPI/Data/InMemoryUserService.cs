using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdultsWebAPI.Models;


namespace AdultsWebAPI.Data
{
    public class InMemoryUserService:IUserService
    {
        private List<User> users;
        
        public InMemoryUserService()
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
            
        }
        
        public async Task<User> ValidateUserAsync(string userName, string password)
        {
            User first = users.FirstOrDefault(user => user.UserName.Equals(userName));
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