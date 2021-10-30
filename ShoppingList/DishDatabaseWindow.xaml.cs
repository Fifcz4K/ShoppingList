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
using SQLite;

namespace ShoppingList
{
    /// <summary>
    /// Interaction logic for DishDatabaseWindow.xaml
    /// </summary>
    public partial class DishDatabaseWindow : Window
    {
        List<Dish> dishList;
        public DishDatabaseWindow()
        {
            InitializeComponent();
            dishList = new List<Dish>();
            ReadDatabase();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ingredientCategoryCombobox.ItemsSource = Enum.GetValues(typeof(IngredientCategory)).Cast<IngredientCategory>();
        }

        void ReadDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Dish>();
                dishList = connection.Table<Dish>().ToList();
            }

            if (dishList != null)
                dishListView.ItemsSource = dishList;

            if (dishListView.SelectedItem == null)
                ingredientListView.ItemsSource = null;
        }
        void UpdateDatabase(Dish dish)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Dish>();
                connection.Update(dish);
            }
        }
        private void dishListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListView).SelectedItem != null)
            {
                Dish dish = (dishListView.SelectedItem as Dish);
                ingredientListView.ItemsSource = dish.GetIngredientList().OrderBy(x => x.Category);
                dishNameTextbox.Text = dish.Name;
            }
        }

        private void ingredientListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListView).SelectedItem != null)
            {
                Ingredient ingredient = (ingredientListView.SelectedItem as Ingredient);
                ingredientNameTextbox.Text = ingredient.Name;
                ingredientCategoryCombobox.SelectedItem = ingredient.Category;
            }
        }

        private void addDishButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(dishNameTextbox.Text) || !char.IsLetter(dishNameTextbox.Text[0]))
            {
                MessageBox.Show("Dish needs a valid name", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Dish dish = new Dish()
            {
                Name = dishNameTextbox.Text
            };

            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Dish>();
                connection.Insert(dish);
            }

            ReadDatabase();
        }

        private void editDishButton_Click(object sender, RoutedEventArgs e)
        {
            if (dishListView.SelectedItem == null)
            {
                MessageBox.Show("You have to choose a dish", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(dishNameTextbox.Text) || !char.IsLetter(dishNameTextbox.Text[0]))
            {
                MessageBox.Show("Dish needs a valid name", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Dish dish = (dishListView.SelectedItem as Dish);
            dish.Name = dishNameTextbox.Text;

            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Dish>();
                connection.Update(dish);
            }

            ReadDatabase();
        }

        private void deleteDishButton_Click(object sender, RoutedEventArgs e)
        {
            if (dishListView.SelectedItem == null)
            {
                MessageBox.Show("You have to choose a dish", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Dish dish = (dishListView.SelectedItem as Dish);
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Dish>();
                connection.Delete(dish);
            }

            ingredientNameTextbox.Text = "";
            ingredientCategoryCombobox.SelectedItem = null;

            ReadDatabase();
        }

        private void addIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            if(dishListView.SelectedItem == null)
            {
                MessageBox.Show("You have to choose a dish", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(ingredientNameTextbox.Text) || !char.IsLetter(ingredientNameTextbox.Text[0]))
            {
                MessageBox.Show("Ingredient needs a valid name", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ingredientCategoryCombobox.SelectedItem == null)
            {
                MessageBox.Show("You have to choose a category of an ingredient", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Dish dish = (dishListView.SelectedItem as Dish);
            dish.AddIngredient(new Ingredient(ingredientNameTextbox.Text, (IngredientCategory)ingredientCategoryCombobox.SelectedItem));

            UpdateDatabase(dishListView.SelectedItem as Dish);
            ingredientListView.ItemsSource = (dishListView.SelectedItem as Dish).GetIngredientList().OrderBy(x => x.Category);
        }

        private void editIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            if (dishListView.SelectedItem == null)
            {
                MessageBox.Show("You have to choose a dish", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ingredientListView.SelectedItem == null)
            {
                MessageBox.Show("You have to choose an ingredient", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(ingredientNameTextbox.Text) || !char.IsLetter(ingredientNameTextbox.Text[0]))
            {
                MessageBox.Show("Ingredient needs a valid name", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ingredientCategoryCombobox.SelectedItem == null)
            {
                MessageBox.Show("You have to choose a category of an ingredient", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Dish dish = dishListView.SelectedItem as Dish;
            Ingredient ingredient = ingredientListView.SelectedItem as Ingredient;
            dish.UpdateIngredient(ingredient, new Ingredient(ingredientNameTextbox.Text, (IngredientCategory)ingredientCategoryCombobox.SelectedItem));

            UpdateDatabase(dishListView.SelectedItem as Dish);
            ingredientListView.ItemsSource = (dishListView.SelectedItem as Dish).GetIngredientList().OrderBy(x => x.Category);
        }

        private void deleteIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            if (dishListView.SelectedItem == null)
            {
                MessageBox.Show("You have to choose a dish", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ingredientListView.SelectedItem == null)
            {
                MessageBox.Show("You have to choose an ingredient", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Dish dish = dishListView.SelectedItem as Dish;
            Ingredient ingredient = ingredientListView.SelectedItem as Ingredient;
            dish.RemoveIngredient(ingredient);

            UpdateDatabase(dishListView.SelectedItem as Dish);
            ingredientListView.ItemsSource = (dishListView.SelectedItem as Dish).GetIngredientList().OrderBy(x => x.Category);
        }

        private void closeDatabaseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void dishFilterTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Dish> filteredList = new List<Dish>();

            filteredList = dishList.Where(x => x.Name.ToLower().Contains(dishFilterTextbox.Text.ToLower())).ToList();
            dishListView.ItemsSource = filteredList;
        }

        private void ingredientFilterTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Ingredient> filteredList = new List<Ingredient>();

            filteredList = (dishListView.SelectedItem as Dish).GetIngredientList().Where(x => x.Name.ToLower().Contains(ingredientFilterTextbox.Text.ToLower())).ToList();
            ingredientListView.ItemsSource = filteredList;
        }
    }
}
