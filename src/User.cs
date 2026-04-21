using System;
using System.Collections.Generic;

namespace SocialTopology
{
    public class User
    {
        public Guid Id { get; private set; } 
        public string Login { get; private set; } 
        public string Password { get; private set; } 
        public string Name { get; set; } 
        public List<User> Friends { get; private set; } 
        
        public User(string login, string password, string name)
        {
            Id = Guid.NewGuid();
            Login = login;
            Password = password;
            Name = name;
            Friends = new List<User>();
        }

        public override string ToString()
        {
            return $"[{Login}] {Name} (friends: {Friends.Count})";
        }
    }
}