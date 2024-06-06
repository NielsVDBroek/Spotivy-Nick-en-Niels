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
            Name = name;
            Artist = artist;
            Text = text;
            Date = DateTime.Now;
            TotalPlays = 0;

            musicLibrary.AddSong(this);
        }

        public void SetAlbum(Album album)
        {
            Album = album;
        }



        public void PlaySong() 
        {
            TotalPlays++;
            Console.WriteLine($"Playing {Name}");
        }
        public void PauseSong() { }
        public void ShowInfo()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Artist: {Artist}");
            if(Album != null)
            {
                Console.WriteLine($"Text: {Album}");
            }
            Console.WriteLine($"Text: {Text}");
            Console.WriteLine($"Date: {Date.ToString("dd/MM/yyyy")}");
            Console.WriteLine($"TotalPlays: {TotalPlays}");
        }
        public void AddToList() { }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
