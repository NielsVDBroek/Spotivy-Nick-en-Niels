using System;
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
        }

    }
}
