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
using System.Windows.Shapes;
using ShoppingList.Classes;

namespace ShoppingList
{
    /// <summary>
    /// Interaction logic for DishDatabaseWindow.xaml
    /// </summary>
    public partial class DishDatabaseWindow : Window
    {
        static List<Dish> dishList = new List<Dish>();
        public DishDatabaseWindow()
        {
            InitializeComponent();

            dishList.Add(new Dish("Pizza", new Ingredient("Ciasto", IngredientCategory.Bread), new Ingredient("Salami", IngredientCategory.Meat), new Ingredient("Ser", IngredientCategory.Dairy)));
            dishList.Add(new Dish("Burger", new Ingredient("Bułki", IngredientCategory.Bread), new Ingredient("Mięso wołowe", IngredientCategory.Meat), new Ingredient("Ser", IngredientCategory.Dairy)));
            dishList.Add(new Dish("Kanapka", new Ingredient("chleb", IngredientCategory.Bread), new Ingredient("Szyneczka", IngredientCategory.Meat)));

            dishListView.ItemsSource = dishList;
        }

        //static private void updateIngredientList()
        //{
        //    foreach (Dish dish in dishList)
        //    {
        //        foreach (Ingredient ingredient in dish.GetIngredientList())
        //        {
        //            allIngredients.Add(ingredient);
        //        }
        //    }
        //}

        private void dishListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dishListView.SelectedItem == null)
                return;

            ingredientListView.ItemsSource = (dishListView.SelectedItem as Dish).GetIngredientList();
        }

        private void addDishButton_Click(object sender, RoutedEventArgs e)
        {
            AddDishWindow addDishWindow = new AddDishWindow(ref dishList);
            addDishWindow.ShowDialog();
            dishListView.ItemsSource = null;
            dishListView.ItemsSource = dishList;
        }

        private void ingredientListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ingredientListView.SelectedItem == null)
                return;

            ModifyIngredientWindow modifyIngredientWindow = new ModifyIngredientWindow(ingredientListView.SelectedItem as Ingredient, dishListView.SelectedItem as Dish);
            modifyIngredientWindow.ShowDialog();
            ingredientListView.ItemsSource = (dishListView.SelectedItem as Dish).GetIngredientList();
        }

        private void addIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            if(dishListView.SelectedItem == null)
            {
                MessageBox.Show("You have to choose a dish", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }    

            AddIngredientWindow addIngredientWindow = new AddIngredientWindow(dishListView.SelectedItem as Dish);
            addIngredientWindow.ShowDialog();
            ingredientListView.ItemsSource = (dishListView.SelectedItem as Dish).GetIngredientList();
        }

        private void saveDatabaseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
