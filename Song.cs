using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotivy_Nick_en_Niels
{
    internal class Song
    {
        private string Name { get; }
        private Artist Artist { get; }
        private string Text { get; }
        public DateTime Date { get; private set; }
        private int TotalPlays { get; set; }
        public Album Album { get; private set; }

        public Song(string name, Artist artist, string text, Music musicLibrary) 
        {
            this.Name = name;
            this.Artist = artist;
            this.Text = text;
            this.Date = DateTime.Now;
            this.TotalPlays = 0;

            musicLibrary.AddSong(this);
        }

        public void SetAlbum(Album album)
        {
            this.Album = album;
        }



        public async Task PlaySong()
        {
            this.TotalPlays++;
            Console.WriteLine($"Playing {this.Name}");
            string[] words = Text.Split(' ');

            // Iterate through each word
            foreach (string word in words)
            {
                Console.Write(word + " ");
                await Task.Delay(100);
            }
            Console.WriteLine();
        }

        public void PauseSong() { }
        public void ShowInfo()
        {
            Console.WriteLine($"Name: {this.Name}");
            Console.WriteLine($"Artist: {this.Artist}");
            if(this.Album != null)
            {
                Console.WriteLine($"Text: {this.Album}");
            }
            Console.WriteLine($"Text: {this.Text}");
            Console.WriteLine($"Date: {this.Date.ToString("dd/MM/yyyy")}");
            Console.WriteLine($"TotalPlays: {this.TotalPlays}");
        }
        public void AddToList() { }

        public override string ToString()
        {
            return $"{this.Name}";
        }
    }
}
