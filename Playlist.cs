using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotivy_Nick_en_Niels
{
    internal class Playlist : List
    {
        public Playlist(string name, User owner) : base(name, owner) 
        {
            owner.Playlists.Add(this);
        }

        //Nummer toevoegen aan de lijst
        public void AddSong(Song songToAdd) 
        {
            if (this.Songs.Contains(songToAdd))
            {
                Console.WriteLine($"{songToAdd} is already in this list.");
            }
            else
            {
                this.Songs.Add(songToAdd);
            }
        }

        //Nummer verwijderen uit de lijst
        public void RemoveSong(Song songToRemove) 
        {
            if (this.Songs.Contains(songToRemove))
            {
                this.Songs.Remove(songToRemove);
            }
            else
            {
                Console.WriteLine($"{songToRemove} is not in this list.");
            }
            
        }

        //Naam van de lijst veranderen
        public void RenameList() 
        {
            Console.WriteLine("What do you want to rename this playlist to?");
            String newListName = Console.ReadLine();
            this.Name = newListName;
        }
    }
}
