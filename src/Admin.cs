using System;

namespace SocialTopology
{
    public class Admin : User
    {
        public Admin() : base() { }

        public Admin(string login, string password, string name) : base(login, password, name) { }
        
        public override string GetInfo()
        {
            return $"[ADMIN] {Login} - {Name} (friends: {Friends.Count})";
        }
    }
}