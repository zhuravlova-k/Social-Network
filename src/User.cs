using System;
using System.Collections.Generic;

namespace SocialTopology
{
    public class User : BaseEntity
    {
        public string Login { get; set; } 
        public string Password { get; set; } 
        public string Name { get; set; } 
        
        public List<User> Friends { get; set; } 
        
        public UserProfile Profile { get; set; }
        
        public List<FriendGroup> Groups { get; set; }

        public User() 
        {
            Id = Guid.NewGuid();
            Login = "";
            Password = "";
            Name = "";
            Friends = new List<User>();
            Profile = new UserProfile(); 
            Groups = new List<FriendGroup>();
        }
        
        public User(string login, string password, string name)
        {
            Id = Guid.NewGuid();
            Login = login;
            Password = password;
            Name = name;
            Friends = new List<User>();
            Profile = new UserProfile();
            Groups = new List<FriendGroup>();
        }
        
        public override string GetInfo()
        {
            return $"[{Login}] {Name} (friends: {Friends.Count}, groups: {Groups.Count})";
        }

        public override string ToString()
        {
            return GetInfo();
        }
    }
}