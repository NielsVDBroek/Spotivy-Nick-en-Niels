namespace Spotivy_Nick_en_Niels
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Music MusicCollection = new Music();

            Artist Artist1 = new Artist("Test Artist 1", "Artist1Password");
            Artist Artist2 = new Artist("Test Artist 1", "Artist1Password");
            Song Song1 = new Song("Test Nummer 1", Artist1, "Test Text 1", MusicCollection);
            Song Song2 = new Song("Test Nummer 2", Artist2, "Test Text 2", MusicCollection);
            Song Song3 = new Song("Test Nummer 3", Artist1, "Test Text 3", MusicCollection);
            //Data.AddStandardData();

            Console.WriteLine("Data toegevoegd!");
            Console.WriteLine(DateTime.Now);
            foreach (Song song in MusicCollection.GetListOfMusic())
            {
                song.ShowInfo();
                Console.WriteLine();
            }
        }
    }
}
