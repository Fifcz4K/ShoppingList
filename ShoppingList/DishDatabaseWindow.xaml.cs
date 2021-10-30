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
            if (dishListView.SelectedItem != null)
            {
                Dish dish = (dishListView.SelectedItem as Dish);
                ingredientListView.ItemsSource = dish.GetIngredientList();
                dishNameTextbox.Text = dish.Name;
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

            ReadDatabase();
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
            UpdateDatabase(dishListView.SelectedItem as Dish);
            ingredientListView.ItemsSource = (dishListView.SelectedItem as Dish).GetIngredientList();
        }

        private void ingredientListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ingredientListView.SelectedItem == null)
                return;

            ModifyIngredientWindow modifyIngredientWindow = new ModifyIngredientWindow(ingredientListView.SelectedItem as Ingredient, dishListView.SelectedItem as Dish);
            modifyIngredientWindow.ShowDialog();
            UpdateDatabase(dishListView.SelectedItem as Dish);
            ingredientListView.ItemsSource = (dishListView.SelectedItem as Dish).GetIngredientList();
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
