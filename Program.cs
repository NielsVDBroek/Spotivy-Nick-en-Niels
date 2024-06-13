namespace Spotivy_Nick_en_Niels
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            Data.AddStandardData();

            var cts = new CancellationTokenSource();

            Console.WriteLine("Data toegevoegd!");
            Console.WriteLine(DateTime.Now);

            foreach (Artist artist in Data.GetArtists())
            {
                Console.WriteLine(artist);
            }

            foreach (Song song in Data.GetSongs())
            {
                var playTask = song.PlaySong(cts.Token);

                await Task.Delay(5000);
                song.PauseSong();
                await Task.Delay(5000);
                song.ResumeSong();
                await playTask;
            }
        }
    }
}