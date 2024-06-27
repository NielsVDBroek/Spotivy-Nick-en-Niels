using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Spotivy_Nick_en_Niels
{
    internal class Song
    {
        public string Name { get; }
        private Artist Artist { get; }
        private string Text { get; }
        public DateTime Date { get; private set; }
        private int TotalPlays { get; set; }
        public Album Album { get; private set; }

        //Voor het pauzeren en hervatten van een nummer.
        private bool isPaused;
        private ManualResetEventSlim pauseEvent;
        private static CancellationTokenSource cts = new CancellationTokenSource();

        public Song(string name, Artist artist, string text) 
        {
            this.Name = name;
            this.Artist = artist;
            this.Text = text;
            this.Date = DateTime.Now;
            this.TotalPlays = 0;
            this.pauseEvent = new ManualResetEventSlim(true);
            Data.GetSongs().Add(this);
        }

        public void SetAlbum(Album album)
        {
            this.Album = album;
        }



        //Tekst van het nummer opsplitsen in woorden. deze woorden worden om de 100ms getoond.
        public async Task PlaySong()
        {
            this.TotalPlays++;
            Console.WriteLine($"Playing {this.Name}");
            string[] words = Text.Split(' ');

            foreach (string word in words)
            {
                cts.Token.ThrowIfCancellationRequested();
                pauseEvent.Wait();

                Console.Write(word + " ");
                await Task.Delay(100);
            }
            Console.WriteLine();

        }

        //Nummer pauzeren.
        public void PauseSong() 
        {
            if (!isPaused)
            {
                pauseEvent.Reset();
                isPaused = true;
                Console.WriteLine("\r\nSong paused");
            }
        }

        //Nummer hervatten.
        public void ResumeSong()
        {
            if (isPaused)
            {
                pauseEvent.Set();
                isPaused = false;
                Console.WriteLine("\r\nSong resumed\r\n");
            }
        }

        //Toon de info van een nummer
        public void ShowInfo()
        {
            Console.WriteLine($"Name: {this.Name}");
            Console.WriteLine($"Artist: {this.Artist}");
            if(this.Album != null)
            {
                Console.WriteLine($"Text: {this.Album}");
            }
            Console.WriteLine($"Date: {this.Date.ToString("dd/MM/yyyy")}");
            Console.WriteLine($"TotalPlays: {this.TotalPlays}");
        }

        public override string ToString()
        {
            return $"{this.Name}";
        }
    }
}
