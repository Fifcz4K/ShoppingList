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
        static List<Dish> dishList = new List<Dish>();
        public DishDatabaseWindow()
        {
            InitializeComponent();
            ReadDatabase();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            dishListView.ItemsSource = dishList;
        }

        void ReadDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Dish>();
                dishList = connection.Table<Dish>().ToList();
            }
        }

        private void dishListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dishListView.SelectedItem != null)
                ingredientListView.ItemsSource = (dishListView.SelectedItem as Dish).GetIngredientList();
        }

        private void addDishButton_Click(object sender, RoutedEventArgs e)
        {
            AddDishWindow addDishWindow = new AddDishWindow(ref dishList);
            addDishWindow.ShowDialog();
            dishListView.ItemsSource = null;
            dishListView.ItemsSource = dishList;
        }

        private void dishListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dishListView.SelectedItem == null)
                return;

            int index = dishList.FindIndex(x => x.Name == (dishListView.SelectedItem as Dish).Name);
            ModifyDishWindow modifyDishWindow = new ModifyDishWindow(ref dishList, index);
            modifyDishWindow.ShowDialog();
            dishListView.ItemsSource = null;
            dishListView.ItemsSource = dishList;

            if (dishListView.SelectedItem == null)
                ingredientListView.ItemsSource = null;
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

        private void ingredientListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ingredientListView.SelectedItem == null)
                return;

            ModifyIngredientWindow modifyIngredientWindow = new ModifyIngredientWindow(ingredientListView.SelectedItem as Ingredient, dishListView.SelectedItem as Dish);
            modifyIngredientWindow.ShowDialog();
            ingredientListView.ItemsSource = (dishListView.SelectedItem as Dish).GetIngredientList();
        }

        private void saveDatabaseButton_Click(object sender, RoutedEventArgs e)
        {
            using(SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Dish>();
                connection.DeleteAll<Dish>();
                foreach (Dish dish in dishList)
                {
                    connection.Insert(dish);
                }
            }

            Close();
        }
    }
}
