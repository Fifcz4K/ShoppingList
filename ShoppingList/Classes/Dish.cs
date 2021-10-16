using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShoppingList.Classes
{
    public class Dish
    {
        public string Name { get; set; }
        public string ListOfIngredientsJSON { get; set; }

        public Dish(string name, params Ingredient[] ingredients)
        {
            Name = name;
            if(ingredients != null)
                ListOfIngredientsJSON = JsonSerializer.Serialize(ingredients);
        }

        public void AddIngredient(Ingredient ingredient)
        {
            List<Ingredient> ingredientList;
            if (string.IsNullOrEmpty(ListOfIngredientsJSON))
                ingredientList = new List<Ingredient>();
            else
                ingredientList = JsonSerializer.Deserialize<List<Ingredient>>(ListOfIngredientsJSON);

            ingredientList.Add(ingredient);
            ListOfIngredientsJSON = JsonSerializer.Serialize(ingredientList);
        }

        public void UpdateIngredient(Ingredient oldIngredient, Ingredient newIngredient)
        {
            List<Ingredient> ingredientList = JsonSerializer.Deserialize<List<Ingredient>>(ListOfIngredientsJSON);
            ingredientList[ingredientList.FindIndex(x => x.Name.Equals(oldIngredient.Name))] = newIngredient;
            ListOfIngredientsJSON = JsonSerializer.Serialize(ingredientList);
        }

        public void RemoveIngredient(Ingredient ingredient)
        {
            List<Ingredient> ingredientList = JsonSerializer.Deserialize<List<Ingredient>>(ListOfIngredientsJSON);
            ingredientList.Remove(ingredientList.Find(x => x.Name.Equals(ingredient.Name)));
            ListOfIngredientsJSON = JsonSerializer.Serialize(ingredientList);
        }

        public override string ToString()
        {
            return $"{Name}";
        }

        public List<Ingredient> GetIngredientList()
        {
            if(string.IsNullOrEmpty(ListOfIngredientsJSON))
                return new List<Ingredient>();

            return JsonSerializer.Deserialize<List<Ingredient>>(ListOfIngredientsJSON);
        }
    }
}
