namespace Spotivy_Nick_en_Niels
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Music MusicCollection = new Music();

            Data.AddStandardData(MusicCollection);

            Console.WriteLine("Data toegevoegd!");
            Console.WriteLine(DateTime.Now);
            foreach (Song song in MusicCollection.GetListOfMusic())
            {
                Console.WriteLine(song);
                song.PlaySong();
            }

        }
    }
}
