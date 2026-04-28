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
                            var regLogin = Console.ReadLine();
                            Console.Write("enter password: ");
                            var regPass = Console.ReadLine();
                            Console.Write("enter your name: ");
                            var regName = Console.ReadLine();
                            
                            network.Register(regLogin, regPass, regName);
                            break;
                            
                        case "2":
                            Console.Write("enter login: ");
                            var login = Console.ReadLine();
                            Console.Write("enter password: ");
                            var pass = Console.ReadLine();
                            
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
                    Console.WriteLine("1. my friends");
                    Console.WriteLine("2. search users");
                    Console.WriteLine("3. add friend");
                    Console.WriteLine("4. remove friend");
                    Console.WriteLine("5. logout");
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
                            var pattern = Console.ReadLine();
                            var found = network.FindUsersInNetwork(pattern);
                            
                            Console.WriteLine("\n--- search results ---");
                            if (found.Count == 0) Console.WriteLine("no one found.");
                            foreach (var u in found) Console.WriteLine(u);
                            break;
                            
                        case "3":
                            Console.Write("enter user login: ");
                            var addLogin = Console.ReadLine();
                            network.AddFriend(addLogin);
                            break;
                            
                        case "4":
                            Console.Write("enter login to remove: ");
                            var remLogin = Console.ReadLine();
                            network.RemoveFriend(remLogin);
                            break;
                            
                        case "5":
                            network.Logout();
                            break;
                            
                        case "6":
                            Console.Write("are you sure? type 'yes' to delete account: ");
                            if (Console.ReadLine()?.ToLower() == "yes")
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
                Console.WriteLine("\npress Enter to continue...");
                Console.ReadLine();
            }
        }
    }
}