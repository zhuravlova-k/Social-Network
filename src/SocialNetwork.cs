using System.Collections.Generic;
using System.Linq;

namespace SocialTopology
{
    public class SocialNetwork
    {
        public List<User> AllUsers { get; private set; }
        public User CurrentUser { get; private set; } 

        public SocialNetwork()
        {
            AllUsers = new List<User>();
            CurrentUser = null; 
        }

        public void Register(string login, string password, string name)
        {
            AllUsers.Add(new User(login, password, name));
        }

        public void Login(string login, string password)
        {
            CurrentUser = AllUsers.FirstOrDefault(u => u.Login == login && u.Password == password);
        }

        public void Logout()
        {
            CurrentUser = null;
        }

        public List<User> FindUsersInNetwork(string namePattern)
        {
            return AllUsers.Where(u => u.Name.ToLower().Contains(namePattern.ToLower())).ToList();
        }

        public void AddFriend(string targetLogin)
        {
            var targetUser = AllUsers.FirstOrDefault(u => u.Login == targetLogin);
            
            CurrentUser.Friends.Add(targetUser);
            targetUser.Friends.Add(CurrentUser);
        }

        public void RemoveFriend(string targetLogin)
        {
            var targetUser = CurrentUser.Friends.FirstOrDefault(u => u.Login == targetLogin);
            
            CurrentUser.Friends.Remove(targetUser);
            targetUser.Friends.Remove(CurrentUser);
        }

        public List<User> GetSortedFriends()
        {
            if (CurrentUser == null) return new List<User>();
            return CurrentUser.Friends.OrderByDescending(u => u.Friends.Count).ToList();
        }
    }
}