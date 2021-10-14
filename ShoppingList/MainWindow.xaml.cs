using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ShoppingList.Classes;

namespace ShoppingList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static List<Dish> dishList = new List<Dish>();
        static List<Ingredient> allIngredients = new List<Ingredient>();
        public MainWindow()
        {
            InitializeComponent();
            dishList.Add(new Dish("Pizza", new Ingredient("Ciasto", IngredientCategory.Bread), new Ingredient("Salami", IngredientCategory.Meat), new Ingredient("Ser", IngredientCategory.Dairy)));
            dishList.Add(new Dish("Burger", new Ingredient("Bułki", IngredientCategory.Bread), new Ingredient("Mięso wołowe", IngredientCategory.Meat), new Ingredient("Ser", IngredientCategory.Dairy)));

            dishListView.ItemsSource = dishList;
            ingredientListView.ItemsSource = allIngredients;
        }

        static private void updateIngredientList()
        {
            foreach (Dish dish in dishList)
            {
                foreach (Ingredient ingredient in dish.GetIngredientList())
                {
                    allIngredients.Add(ingredient);
                }
            }
        }
    }
}
