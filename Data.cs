using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace Spotivy_Nick_en_Niels
{
    internal class Data
    {
        public static void AddStandardData()
        {
            Music MusicCollection = new Music();

            Artist Artist1 = new Artist("Test Artist 1", "Artist1Password");
            Artist Artist2 = new Artist("Test Artist 1", "Artist1Password");
            Song Song1 = new Song("Test Nummer 1", Artist1, "Test Text 1", MusicCollection);
            Song Song2 = new Song("Test Nummer 2", Artist2, "Test Text 2", MusicCollection);
        }
    }

    internal class Music
    {
        private List<Song> music = new List<Song>();

        public void AddSong(Song song)
        {
            music.Add(song);
        }

        public List<Song> GetListOfMusic()
        {
            return music;
        }
    }

    internal class Artists
    {
        private List<Artist> artists = new List<Artist>();

        public void AddArtist(Artist artist)
        {
            artists.Add(artist);
        }

        public List<Artist> GetListOfMusic()
        {
            return artists;
        }
    }
}
