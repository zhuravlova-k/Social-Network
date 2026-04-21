using System.Collections.Generic;
using System.Linq;

namespace SocialTopology
{
    public class SocialNetwork
    {
        public List<User> Users { get; private set; }

        public SocialNetwork()
        {
            Users = new List<User>();
        }
            
        public void AddUser(string name)
        {
            Users.Add(new User(name));
        }
            
        public void RemoveUser(User user)
        {
            foreach (var friend in user.Friends)
            {
                friend.Friends.Remove(user);
            }

            Users.Remove(user);
        }
            
        public void AddFriendship(User u1, User u2)
        {
            if (u1 != u2 && !u1.Friends.Contains(u2))
            {
                u1.Friends.Add(u2);
                u2.Friends.Add(u1);
            }
        }
            
        public List<User> FindUsers(string namePattern, int minFriends)
        {
            return Users
                .Where(u => u.Name.ToLower().Contains(namePattern.ToLower()) && u.Friends.Count >= minFriends)
                .ToList();
        }
            
        public List<User> SortByFriendsCount()
        {
            return Users.OrderByDescending(u => u.Friends.Count).ToList();
        }
    }
}