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
        private string Date { get; }
        private int TotalPlays { get; }
        public Album Album { get; private set; }

        public Song(string name, Artist artist, string text, string date) 
        {
            Name = name;
            Artist = artist;
            Text = text;
            Date = date;
            TotalPlays = 0;
        }

        public void PlaySong() { }
        public void PauseSong() { }
        public void ShowInfo() { }
        public void AddToList() { }
    }
}
