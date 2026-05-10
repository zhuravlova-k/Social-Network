using System;

namespace SocialTopology
{
    public class UserProfile
    {
        public string Bio { get; set; } = "No bio yet";
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}