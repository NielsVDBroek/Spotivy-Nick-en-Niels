using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotivy_Nick_en_Niels
{
    internal class Person
    {
        protected string Name { get; }
        private string Password { get; }

        public Person(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public void LogIn()
        {

        }

        public override string ToString()
        {
            return $"{Name}";
        }

    }
}
