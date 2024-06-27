using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotivy_Nick_en_Niels
{
    internal class Person
    {
        public string Name { get; }
        private string Password { get; }

        public Person(string name, string password)
        {
            this.Name = name;
            this.Password = password;
        }

        public override string ToString()
        {
            return $"{this.Name}";
        }

    }
}
