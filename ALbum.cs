using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotivy_Nick_en_Niels
{
    internal class Album : List
    {
        public Album(string name, User owner) : base(name, owner) { }

        private void AddSong() { }
    }
}
