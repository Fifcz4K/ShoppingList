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
    /// Interaction logic for ScheduleWindow.xaml
    /// </summary>
    public class IngredientShoppingList : Ingredient
    {
        public UInt16 Counter { get; set; }
        
        public IngredientShoppingList(Ingredient ingredient) : base(ingredient.Name, ingredient.Category)
        {
            Counter = 1;
        }


    }
    public partial class ScheduleWindow : Window
    {
        List<Dish> databaseDishList = new List<Dish>();
        List<ScheduleDish> scheduledList = new List<ScheduleDish>();
        List<IngredientShoppingList> ingredientList = new List<IngredientShoppingList>();
        public ScheduleWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            ReadDatabase();

            ingredientCategoryCombobox.ItemsSource = Enum.GetValues(typeof(IngredientCategory)).Cast<IngredientCategory>();
            dayCombobox.ItemsSource = Enum.GetValues(typeof(Days)).Cast<Days>();
            scheduleDishCombobox.ItemsSource = databaseDishList;
        }

        private void ReadDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Dish>();
                databaseDishList = connection.Table<Dish>().ToList();
            }
        }

        private void updateDishList(ScheduleDish newDish)
        {
            if(newDish != null)
                scheduledList.Add(newDish);

            if (scheduledList != null)
            {
                dishListview.ItemsSource = scheduledList.OrderBy(x => x.Day).ToList();

                CollectionView dayView = (CollectionView)CollectionViewSource.GetDefaultView(dishListview.ItemsSource);
                PropertyGroupDescription dayGroupDescription = new PropertyGroupDescription("Day");
                dayView.GroupDescriptions.Add(dayGroupDescription);
            }
        }

        private void updateIngredientList()
        {
            if (ingredientList != null)
            {
                ingredientListview.ItemsSource = ingredientList.OrderBy(x => x.Category).ToList();
                CollectionView ingredientView = (CollectionView)CollectionViewSource.GetDefaultView(ingredientListview.ItemsSource);
                PropertyGroupDescription ingredientGroupDescription = new PropertyGroupDescription("Category");
                ingredientView.GroupDescriptions.Add(ingredientGroupDescription);
            }
        }

        private void updateIngredientListFromDishList()
        {
            if(scheduledList.Count == 0)
            {
                ingredientListview.ItemsSource = null;
                return;
            }

            List<Dish> tempDishList = new List<Dish>();
            tempDishList = scheduledList.Select(x => x.Dish).ToList();
            ingredientList.Clear();
            foreach (var item in tempDishList)
            {
                foreach (var ingredient in item.GetIngredientList())
                {
                    int index = ingredientList.FindIndex(x => x.Name == ingredient.Name);
                    if (index == -1)
                    {
                        ingredientList.Add(new IngredientShoppingList(ingredient));
                    }
                    else
                    {
                        ingredientList[index].Counter++;
                    }
                }
            }

            updateIngredientList();
        }

        private void ingredientListview_Loaded(object sender, RoutedEventArgs e)
        {
            ListView listView = sender as ListView;
            GridView gView = listView.View as GridView;

            var workingWidth = listView.ActualWidth;
            var col1 = 0.76;
            var col2 = 0.15;

            gView.Columns[0].Width = workingWidth * col1;
            gView.Columns[1].Width = workingWidth * col2;
        }

        private bool checkIngredientInputs()
        {
            if (ingredientCategoryCombobox.SelectedItem == null)
            {
                MessageBox.Show("You have to choose an item category", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

            if (string.IsNullOrEmpty(ingredientNameTextbox.Text) || !char.IsLetter(ingredientNameTextbox.Text[0]))
            {
                MessageBox.Show("Item needs a valid name", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

            if (string.IsNullOrEmpty(ingredientCountTextbox.Text) || !ingredientCountTextbox.Text.All(char.IsDigit))
            {
                MessageBox.Show("Item needs a valid count", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

            return false;
        }

        private void addIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            if(checkIngredientInputs())
                return;

            Ingredient tempIngredient = new Ingredient(ingredientNameTextbox.Text, (IngredientCategory)ingredientCategoryCombobox.SelectedItem);
            IngredientShoppingList tempShoppingIngredient = new IngredientShoppingList(tempIngredient);
            tempShoppingIngredient.Counter = ushort.Parse(ingredientCountTextbox.Text);

            ingredientList.Add(tempShoppingIngredient);

            updateIngredientList();
        }

        private void editIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            if(ingredientListview.SelectedItem == null)
            {
                MessageBox.Show("You have to choose an item", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (checkIngredientInputs())
                return;

            int index = ingredientList.IndexOf(ingredientListview.SelectedItem as IngredientShoppingList);
            ingredientList[index].Name = ingredientNameTextbox.Text;
            ingredientList[index].Counter = ushort.Parse(ingredientCountTextbox.Text);
            ingredientList[index].Category = (IngredientCategory)ingredientCategoryCombobox.SelectedItem;

            updateIngredientList();
        }

        private void deleteIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            if (ingredientListview.SelectedItem == null)
            {
                MessageBox.Show("You have to choose an item", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ingredientList.Remove(ingredientListview.SelectedItem as IngredientShoppingList);
            updateIngredientList();
        }

        private void addDishButton_Click(object sender, RoutedEventArgs e)
        {
            if (scheduleDishCombobox.SelectedItem == null)
            {
                MessageBox.Show("You have to choose a dish", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (dayCombobox.SelectedItem == null)
            {
                MessageBox.Show("You have to choose a day", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ScheduleDish newDish = new ScheduleDish();
            newDish.Dish = scheduleDishCombobox.SelectedItem as Dish;
            newDish.Day = (Days)dayCombobox.SelectedItem;

            updateDishList(newDish);
            updateIngredientListFromDishList();
        }

        private void deleteDishButton_Click(object sender, RoutedEventArgs e)
        {
            if (dishListview.SelectedItem == null)
                return;

            scheduledList.Remove(dishListview.SelectedItem as ScheduleDish);
            updateDishList(null);
            updateIngredientListFromDishList();
        }

        private void ingredientListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if((sender as ListView).SelectedItem != null)
            {
                IngredientShoppingList temp = (sender as ListView).SelectedItem as IngredientShoppingList;
                ingredientNameTextbox.Text = temp.Name;
                ingredientCountTextbox.Text = temp.Counter.ToString();
                ingredientCategoryCombobox.SelectedItem = temp.Category;
            }
        }
    }
}
