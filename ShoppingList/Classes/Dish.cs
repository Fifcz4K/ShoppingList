using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Classes
{
    class Dish
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public List<Product> Products;

        public Dish(string name, params string[] products)
        {
            Name = name;
            Products = new List<Product>();
            foreach (string product in products)
            {
                Products.Add(new Product(product));
            }
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
