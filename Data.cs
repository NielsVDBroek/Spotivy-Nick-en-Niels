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
            Artist Artist1 = new Artist("Test Artist 1", "Artist1Password");
            Artist Artist2 = new Artist("Test Artist 1", "Artist1Password");
            Song Song1 = new Song("Test Nummer 1", Artist1, "Test Text 1");
            Song Song2 = new Song("Test Nummer 2", Artist2, "Test Text 2");
        }
    }
}
