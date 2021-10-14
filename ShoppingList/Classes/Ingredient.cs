using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Classes
{
    enum IngredientCategory
    {
        Dairy,
        Meat,
        Vegetable,
        Fruit,
        Bread,
        Spice,
        Beverage,
        Other
    }

    class Ingredient
    {
        public string Name { get; set; }
        public IngredientCategory Category { get; set; }

        public Ingredient(string name, IngredientCategory category)
        {
            Name = name;
            Category = category;
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
