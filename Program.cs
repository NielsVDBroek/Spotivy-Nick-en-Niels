using System;
using System.Threading.Tasks;
using Spotivy_Nick_en_Niels;
using static Spotivy_Nick_en_Niels.Authentication;

internal class Program
{
    static async Task Main(string[] args)
    {
        //Standaard data toevoegen en gebruiker laten inloggen.
        Data.AddStandardData();
        AskUserLogin();
        Console.Clear();

        //Gebruiker invoer ontvangen.
        while (Authentication.GetCurrentUser() != null)
        {
            Console.WriteLine("Enter your command (or type 'Help' for a list of options to show.):");
            string input = Console.ReadLine().ToLower();

            if (!string.IsNullOrEmpty(input))
            {
                await Client.ParseAndExecuteCommand(input);
            }
            else
            {
                Console.WriteLine("No command entered.");
            }
            await Task.Delay(100);
        }
    }
}
