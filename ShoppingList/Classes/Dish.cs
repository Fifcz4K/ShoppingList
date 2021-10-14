using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShoppingList.Classes
{
    class Dish
    {
        public string Name { get; set; }
        public string ListOfIngredientsJSON { get; set; }

        public Dish(string name, params Ingredient[] ingredients)
        {
            Name = name;
            ListOfIngredientsJSON = JsonSerializer.Serialize(ingredients);
        }

        public override string ToString()
        {
            return $"{Name}";
        }

        public List<Ingredient> GetIngredientList()
        {
            return JsonSerializer.Deserialize<List<Ingredient>>(ListOfIngredientsJSON);
        }
    }
}
