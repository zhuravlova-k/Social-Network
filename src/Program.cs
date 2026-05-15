using System;

namespace SocialTopology
{
    class Program
    {
        static void Main(string[] args)
        {
            var network = new SocialNetwork();
            
            while (true)
            {
                Console.Clear();

                try 
                {
                    if (network.CurrentUser == null)
                    {
                        Console.WriteLine("1. register");
                        Console.WriteLine("2. login");
                        Console.WriteLine("0. exit");
                        Console.Write("choose action: ");
                        
                        var choice = Console.ReadLine();

                        switch (choice)
                        {
                            case "1":
                                Console.Write("enter login: ");
                                var regLogin = Console.ReadLine() ?? "";
                                Console.Write("enter password: ");
                                var regPass = ReadPassword();
                                Console.Write("enter your name: ");
                                var regName = Console.ReadLine() ?? "";
                                
                                network.Register(regLogin, regPass, regName);
                                break;
                                
                            case "2":
                                Console.Write("enter login: ");
                                var login = Console.ReadLine() ?? "";
                                Console.Write("enter password: ");
                                var pass = ReadPassword();
                                
                                network.Login(login, pass);
                                break;
                                
                            case "0":
                                return;
                                
                            default:
                                Console.WriteLine("[-] invalid input");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\n--- profile: {network.CurrentUser.Name} ---");
                        Console.WriteLine($"Bio: {network.CurrentUser.Profile.Bio}");
                        Console.WriteLine("1. my friends");
                        Console.WriteLine("2. search users");
                        Console.WriteLine("3. add friend");
                        Console.WriteLine("4. remove friend");
                        Console.WriteLine("5. edit profile (name & bio)");
                        Console.WriteLine("6. friend recommendations");
                        Console.WriteLine("7. logout");
                        Console.WriteLine("8. delete account");
                        Console.WriteLine("0. exit");
                        Console.Write("choose action: ");
                        
                        var choice = Console.ReadLine();

                        switch (choice)
                        {
                            case "1":
                                var friends = network.GetSortedFriends();
                                Console.WriteLine("\n--- my friends ---");
                                
                                if (friends.Count == 0) Console.WriteLine("list is empty.");
                                foreach (var f in friends) Console.WriteLine(f);
                                break;
                                
                            case "2":
                                Console.Write("enter name to search: ");
                                var pattern = Console.ReadLine() ?? "";
                                var found = network.FindUsersInNetwork(pattern);
                                
                                Console.WriteLine("\n--- search results ---");
                                if (found.Count == 0) Console.WriteLine("no one found.");
                                foreach (var u in found) Console.WriteLine(u);
                                break;
                                
                            case "3":
                                Console.Write("enter user login: ");
                                var addLogin = Console.ReadLine() ?? "";
                                network.AddFriend(addLogin);
                                break;
                                
                            case "4":
                                Console.Write("enter login to remove: ");
                                var remLogin = Console.ReadLine() ?? "";
                                network.RemoveFriend(remLogin);
                                break;

                            case "5":
                                Console.Write("enter new name (leave empty to skip): ");
                                var newName = Console.ReadLine() ?? "";
                                Console.Write("enter new bio (leave empty to skip): ");
                                var newBio = Console.ReadLine() ?? "";
                                network.EditProfile(newName, newBio);
                                break;
                                
                            case "6":
                                var recommendations = network.GetFriendRecommendations();
                                Console.WriteLine("\n--- people you may know ---");
                                
                                if (recommendations.Count == 0) 
                                {
                                    Console.WriteLine("no recommendations yet. add more friends!");
                                }
                                else 
                                {
                                    foreach (var u in recommendations) Console.WriteLine(u);
                                }
                                break;
                                
                            case "7":
                                network.Logout();
                                break;
                                
                            case "8":
                                Console.Write("are you sure? type 'yes' to delete account: ");
                                if ((Console.ReadLine() ?? "").ToLower() == "yes")
                                {
                                    network.DeleteAccount();
                                }
                                else
                                {
                                    Console.WriteLine("[-] deletion cancelled");
                                }
                                break;

                            case "0":
                                return;
                                
                            default:
                                Console.WriteLine("[-] invalid input");
                                break;
                        }
                    }
                }
                catch (NetworkException ex)
                {
                    // ловим наши ошибки
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[-] error: {ex.Message}");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    // непредвиденные системные сбои
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[-] critical error: {ex.Message}");
                    Console.ResetColor();
                }

                Console.WriteLine("\npress Enter to continue...");
                Console.ReadLine();
            }
        }

        static string ReadPassword()
        {
            string password = "";
            while (true)
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine(); 
                    break;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password = password.Substring(0, password.Length - 1);
                        Console.Write("\b \b"); 
                    }
                }
                else if (!char.IsControl(key.KeyChar)) 
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
            }
            return password;
        }
    }
}