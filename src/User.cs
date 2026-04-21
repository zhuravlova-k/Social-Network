using System;
using System.Collections.Generic;

namespace SocialTopology
{
    public class User
    {
        public Guid Id { get; private set; } 
        public string Name { get; set; }
        public List<User> Friends { get; private set; } 

        public User(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Friends = new List<User>();
        }

        public override string ToString()
        {
            return $"[{Id.ToString().Substring(0, 5)}] {Name} (friends: {Friends.Count})";
        }
    }
}