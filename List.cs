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

        public async Task PlayList() 
        {
            if (this.Songs.Count() == 0)
            {
                Console.WriteLine("No songs in this list!");
            } else
            {
                foreach (Song song in this.Songs)
                {
                    Console.WriteLine(song);
                    song.PlaySong();
                    await Task.Delay(2000);
                }
            }
        }
        public void SkipSong() { }

        public void ShowSongs() 
        {
            foreach(Song song in this.Songs)
            {
                Console.WriteLine(song);
            }
        }
    }
}
