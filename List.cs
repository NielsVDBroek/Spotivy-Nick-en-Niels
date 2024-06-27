using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotivy_Nick_en_Niels
{
    internal class List
    {
        public string Name { get; set; }
        private User Owner { get; set; }
        public List<Song> Songs { get; }

        public List(string name, User owner) 
        {
            this.Name = name;
            this.Owner = owner;
            this.Songs = new List<Song>();
        }

        public override string ToString()
        {
            return $"{this.Name}";
        }

        //Alle nummers in de lijst afspelen
        public async Task PlayList() 
        {
            if (this.Songs.Count() == 0)
            {
                Console.WriteLine("No songs in this list!");
            } else
            {
                foreach (Song song in this.Songs)
                {
                    var playTask = song.PlaySong();

                    while (!playTask.IsCompleted)
                    {
                        if (Console.KeyAvailable)
                        {
                            var userInput = Console.ReadKey(intercept: true);
                            if (userInput.Key == ConsoleKey.Spacebar)
                            {
                                song.PauseSong();
                                userInput = Console.ReadKey(intercept: true);
                                if (userInput.Key == ConsoleKey.Spacebar)
                                {
                                    song.ResumeSong();
                                }
                            }
                        }
                        await Task.Delay(100);
                    }

                    await playTask;
                    await Task.Delay(1500);
                }
            }
        }
    }
}
