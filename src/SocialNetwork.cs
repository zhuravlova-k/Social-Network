using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialTopology
{
    public class SocialNetwork
    {
        public List<User> AllUsers { get; private set; }
        
        // переменная может быть null 
        public User? CurrentUser { get; private set; } 

        public SocialNetwork()
        {
            AllUsers = new List<User>();
            CurrentUser = null; 
        }

        public bool Register(string login, string password, string name)
        {
            if (AllUsers.Any(u => u.Login.ToLower() == login.ToLower()))
            {
                Console.WriteLine("[-] error: user with this login already exists");
                return false; 
            }

            AllUsers.Add(new User(login, password, name));
            Console.WriteLine("[+] registration successful");
            return true;
        }

        public bool Login(string login, string password)
        {
            var user = AllUsers.FirstOrDefault(u => u.Login == login && u.Password == password);
            
            if (user != null)
            {
                CurrentUser = user;
                Console.WriteLine($"[+] welcome, {user.Name}");
                return true;
            }
            
            Console.WriteLine("[-] error: invalid login or password");
            return false;
        }

        public void Logout()
        {
            CurrentUser = null;
            Console.WriteLine("[+] logged out");
        }

        public List<User> FindUsersInNetwork(string namePattern)
        {
            if (CurrentUser == null) return new List<User>();
           
            return AllUsers
                .Where(u => u.Name.ToLower().Contains(namePattern.ToLower()) && u.Login != CurrentUser.Login)
                .ToList();
        }

        public void AddFriend(string targetLogin)
        {
            if (CurrentUser == null) return;

            var targetUser = AllUsers.FirstOrDefault(u => u.Login == targetLogin);
            
            if (targetUser == null)
            {
                Console.WriteLine("[-] user not found");
                return;
            }

            // проверка не добавляем сами себя
            if (targetUser.Login == CurrentUser.Login)
            {
                Console.WriteLine("[-] you can't add yourself");
                return;
            }
           
            if (!CurrentUser.Friends.Contains(targetUser))
            {
                CurrentUser.Friends.Add(targetUser);
                targetUser.Friends.Add(CurrentUser);
                Console.WriteLine($"[+] you and {targetUser.Name} are friends now");
            }
            else
            {
                Console.WriteLine("[-] already in your friends list");
            }
        }

        public void RemoveFriend(string targetLogin)
        {
            if (CurrentUser == null) return;

            var targetUser = CurrentUser.Friends.FirstOrDefault(u => u.Login == targetLogin);
            
            if (targetUser != null)
            {
                CurrentUser.Friends.Remove(targetUser);
                targetUser.Friends.Remove(CurrentUser);
                Console.WriteLine($"[+] user {targetUser.Name} removed from friends");
            }
            else
            {
                Console.WriteLine("[-] user is not in your friends list");
            }
        }

        public List<User> GetSortedFriends()
        {
            if (CurrentUser == null) return new List<User>();
            return CurrentUser.Friends.OrderByDescending(u => u.Friends.Count).ToList();
        }
    }
}