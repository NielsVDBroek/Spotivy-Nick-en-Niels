﻿namespace Spotivy_Nick_en_Niels
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Artist Artist1 = new Artist("Test Artist 1", "Artist1Password");
            Artist Artist2 = new Artist("Test Artist 1", "Artist1Password");
            Song Song1 = new Song("Test Nummer 1", Artist1, "Test Text 1");
            Song Song2 = new Song("Test Nummer 2", Artist2, "Test Text 2");
            //Data.AddStandardData();

            Console.WriteLine("Data toegevoegd!");
            Console.WriteLine(DateTime.Now);
            Song1.ShowInfo();
            Song1.PlaySong();
            Song1.ShowInfo();
        }
    }
}
