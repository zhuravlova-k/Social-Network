using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SocialTopology
{
    public class SocialNetwork
    {
        public List<User> AllUsers { get; private set; }
        
        // переменная может быть null 
        public User? CurrentUser { get; private set; } 

        private const string FilePath = "data.json";

        public SocialNetwork()
        {
            AllUsers = new List<User>();
            CurrentUser = null; 
            LoadFromFile();
        }

        private void SaveToFile() {
            var options = new JsonSerializerOptions 
            { 
                // предотвращает бесконечное зацикливание при сохранении
                // Вместо того чтобы по кругу сохранять друзей программа просто сохраняет ссылки на уже созданные объекты.
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true 
            };
            string json = JsonSerializer.Serialize(AllUsers, options);
            File.WriteAllText(FilePath, json);
        }

        private void LoadFromFile() {
            if (!File.Exists(FilePath)) return;

            try 
            {
                string json = File.ReadAllText(FilePath);
                var options = new JsonSerializerOptions 
                { 
                    ReferenceHandler = ReferenceHandler.Preserve 
                };
            
                // расшифровываем джсон обратно в список объектов.
                // оператор ?? — если файл пустой или битый (вернул null),
                // мы не выдаем ошибку, а просто создаем новый чистый список 
                AllUsers = JsonSerializer.Deserialize<List<User>>(json, options) ?? new List<User>();
            }
            catch (Exception ex) 
            {
                Console.WriteLine("[-] warning: database file is corrupted. Starting with an empty network.");
                Console.WriteLine($"[debug] {ex.Message}");
                AllUsers = new List<User>();
            }
            
        }

        public void Register(string login, string password, string name)
        {
            if (AllUsers.Any(u => u.Login.ToLower() == login.ToLower()))
            {
                // викидаємо помилку замість звичайного Console.WriteLine
                throw new NetworkException("user with this login already exists");
            }

            string hashedPassword = SecurityHelper.HashPassword(password);
            AllUsers.Add(new User(login, hashedPassword, name));
            
            SaveToFile();
            Console.WriteLine("[+] registration successful");
        }

        public void Login(string login, string password)
        {
            string hashedInput = SecurityHelper.HashPassword(password);
            var user = AllUsers.FirstOrDefault(u => u.Login == login && u.Password == hashedInput);
            
            if (user != null)
            {
                CurrentUser = user;
                Console.WriteLine($"[+] welcome, {user.Name}");
                return;
            }
            
            // наша помилка
            throw new NetworkException("invalid login or password");
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
                throw new NetworkException("user not found");
                
            if (targetUser.Login == CurrentUser.Login)
                throw new NetworkException("you can't add yourself");
           
            if (!CurrentUser.Friends.Contains(targetUser))
            {
                CurrentUser.Friends.Add(targetUser);
                targetUser.Friends.Add(CurrentUser);
                SaveToFile(); 
                Console.WriteLine($"[+] you and {targetUser.Name} are friends now");
            }
            else
            {
                throw new NetworkException("already in your friends list");
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
                SaveToFile(); 
                Console.WriteLine($"[+] user {targetUser.Name} removed from friends");
            }
            else
            {
                throw new NetworkException("user is not in your friends list");
            }
        }

        public void DeleteAccount() {
            if (CurrentUser == null) return;

            // очищаем связи (ребра) удаляем себя из списков всех наших друзей
            foreach (var friend in CurrentUser.Friends) 
            {
                friend.Friends.Remove(CurrentUser);
            }

            // удаляем себя из глобальной сети
            AllUsers.Remove(CurrentUser);

            Console.WriteLine($"[+] account {CurrentUser.Login} permanently deleted");

            CurrentUser = null;
            SaveToFile(); 
        }

        public List<User> GetSortedFriends()
        {
            if (CurrentUser == null) return new List<User>();
            return CurrentUser.Friends.OrderByDescending(u => u.Friends.Count).ToList();
        }

        public List<User> GetFriendRecommendations()
        {
            if (CurrentUser == null) return new List<User>();

            // лист для подсчета общих друзей: юзер -> колво совпадений
            var recommendations = new Dictionary<User, int>();

            foreach (var friend in CurrentUser.Friends)
            {
                foreach (var friendOfFriend in friend.Friends)
                {
                    // пропускаем самого себя и тех, кто уже есть в нашем списке друзей
                    if (friendOfFriend.Login == CurrentUser.Login || CurrentUser.Friends.Contains(friendOfFriend))
                    {
                        continue;
                    }

                    // считаем общих друзей
                    if (recommendations.ContainsKey(friendOfFriend))
                    {
                        recommendations[friendOfFriend]++;
                    }
                    else
                    {
                        recommendations[friendOfFriend] = 1;
                    }
                }
            }

            // сортируем по убыванию общих друзей и берем топ-5 рекомендаций
            return recommendations
                .OrderByDescending(r => r.Value)
                .Select(r => r.Key)
                .Take(5)
                .ToList();
        }
        
        public void EditProfile(string newName, string newBio)
        {
            if (CurrentUser == null) return;
            
            bool changed = false;

            if (!string.IsNullOrWhiteSpace(newName))
            {
                CurrentUser.Name = newName;
                changed = true;
            }
                
            if (!string.IsNullOrWhiteSpace(newBio))
            {
                CurrentUser.Profile.Bio = newBio;
                changed = true;
            }

            if (changed)
            {
                SaveToFile();
                Console.WriteLine("[+] profile updated successfully");
            }
            else
            {
                Console.WriteLine("[-] no changes were made");
            }
        }

        public void ChangePassword(string newPassword)
        {
            if (CurrentUser == null) return;
            
            if (string.IsNullOrWhiteSpace(newPassword)) 
            {
                throw new NetworkException("password cannot be empty");
            }
            
            CurrentUser.Password = SecurityHelper.HashPassword(newPassword);
            SaveToFile();
            Console.WriteLine("[+] password changed successfully");
        }
    }
}