using System;
using System.Threading.Tasks;
using Spotivy_Nick_en_Niels;
using static Spotivy_Nick_en_Niels.Login;

internal class Program
{
    static async Task Main(string[] args)
    {
        Data.AddStandardData();
        AskUserLogin();

        Console.WriteLine("Data added!");
        Console.WriteLine(DateTime.Now);


        while (true)
        {
            Console.WriteLine("Enter your command (e.g., 'Play houdini'):");
            string input = Console.ReadLine().ToLower();

            if (!string.IsNullOrEmpty(input))
            {
                await Client.ParseAndExecuteCommand(input);
            }
            else
            {
                Console.WriteLine("No command entered.");
            }
                if (Console.KeyAvailable)
                {
                    var userInput = Console.ReadKey(intercept: true);
                    if (userInput.Key == ConsoleKey.A)
                    {
                        User loggedInUser = Login.GetCurrentUser();
                        if (loggedInUser != null)
                        {
                            loggedInUser.SendFriendRequest();
                        }
                        else
                        {
                            Console.WriteLine("No user is currently logged in.");
                        }
                    }
                    if (userInput.Key == ConsoleKey.V)
                    {
                        User loggedInUser = Login.GetCurrentUser();
                        if (loggedInUser != null)
                        {
                            loggedInUser.AcceptFriendRequest();
                        }
                        else
                        {
                            Console.WriteLine("No user is currently logged in.");
                        }
                    }
                    if (userInput.Key == ConsoleKey.P)
                    {
                        User loggedInUser = Login.GetCurrentUser();
                        if (loggedInUser != null)
                        {
                            loggedInUser.ShowPendingFriendRequests();
                        }
                        else
                        {
                            Console.WriteLine("No user is currently logged in.");
                        }
                    }
                    if (userInput.Key == ConsoleKey.D)
                    {
                        User loggedInUser = Login.GetCurrentUser();
                        if (loggedInUser != null)
                        {
                            Console.WriteLine("Enter the name of the friend request you want to delete:");
                            string friendName = Console.ReadLine();
                            User friend = Data.GetUsers().Find(u => u.Name == friendName);
                            if (friend != null)
                            {
                                loggedInUser.DeleteFriendRequest(friend);
                            }
                            else
                            {
                                Console.WriteLine("User not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No user is currently logged in.");
                        }
                    }
                    if (userInput.Key == ConsoleKey.N)
                    {
                        User loggedInUser = Login.GetCurrentUser();
                        if (loggedInUser != null)
                        {
                            Console.WriteLine("Enter the name of the friend request you want to deny:");
                            string friendName = Console.ReadLine();
                            User friend = Data.GetUsers().Find(u => u.Name == friendName);
                            if (friend != null)
                            {
                                loggedInUser.DenyFriendRequest(friend);
                            }
                            else
                            {
                                Console.WriteLine("User not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No user is currently logged in.");
                        }
                    }
                    if (userInput.Key == ConsoleKey.F)
                    {
                        User loggedInUser = Login.GetCurrentUser();
                        if (loggedInUser != null)
                        {
                            loggedInUser.ShowFriendsList();
                        }
                        else
                        {
                            Console.WriteLine("No user is currently logged in.");
                        }
                    }
                    if (userInput.Key == ConsoleKey.H)
                    {
                        ShowHelp();
                    }
                    if (userInput.Key == ConsoleKey.Spacebar)
                    {
                        song.PauseSong();
                        userInput = Console.ReadKey(intercept: true);
                        if (userInput.Key == ConsoleKey.Spacebar)
                        {
                            song.ResumeSong();
                        }
                    }
                    if (userInput.Key == ConsoleKey.O)
                    {
                        UserLogout();
                    }
                    if (userInput.Key == ConsoleKey.U)
                    {
                        User loggedInUser = Login.GetCurrentUser();
                        if (loggedInUser != null)
                        {
                            Console.WriteLine("Current logged-in user: " + loggedInUser.Name);
                        }
                        else
                        {
                            Console.WriteLine("No user is currently logged in.");
                        }
                    }
                    if (userInput.Key == ConsoleKey.R)
                    {
                        User loggedInUser = Login.GetCurrentUser();
                        if (loggedInUser != null)
                        {
                            loggedInUser.RemoveFriend();
                        }
                        else
                        {
                            Console.WriteLine("No user is currently logged in.");
                        }
                    }
                }
                await Task.Delay(100);
            }
        }

    }

    static void ShowHelp()
    {
        Console.WriteLine("Available commands:");
        Console.WriteLine("Spacebar - Pause/Resume the song");
        Console.WriteLine("A - Add a friend");
        Console.WriteLine("F - Show friends list");
        Console.WriteLine("H - Show help");
        Console.WriteLine("O - Logout");
        Console.WriteLine("R - Remove a friend");
        Console.WriteLine("U - Show current logged-in user");
        Console.WriteLine("V - Accept friend request");
        Console.WriteLine("P - Show pending friend requests");
        Console.WriteLine("D - Delete/cancel friend request");
        Console.WriteLine("N - Deny incoming friend request");
    }
}
