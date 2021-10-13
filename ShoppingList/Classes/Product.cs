using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Classes
{
    class Product
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Product(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
