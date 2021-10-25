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
    /// Interaction logic for ScheduleWindow.xaml
    /// </summary>
    public partial class ScheduleWindow : Window
    {
        List<ScheduleDish> scheduledList = new List<ScheduleDish>();
        List<Ingredient> ingredientList = new List<Ingredient>();
        public ScheduleWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
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
                ingredientList.AddRange(item.GetIngredientList());
            }

            if(ingredientList != null)
            {
                ingredientListview.ItemsSource = ingredientList.OrderBy(x => x.Category).ToList();
                CollectionView ingredientView = (CollectionView)CollectionViewSource.GetDefaultView(ingredientListview.ItemsSource);
                PropertyGroupDescription ingredientGroupDescription = new PropertyGroupDescription("Category");
                ingredientView.GroupDescriptions.Add(ingredientGroupDescription);
            }
        }

        private void addDishButton_Click(object sender, RoutedEventArgs e)
        {
            ScheduleDish newDish = new ScheduleDish();
            newDish.Dish.Name = "None";
            AddScheduleDishWindow addScheduleDishWindow = new AddScheduleDishWindow(ref newDish);
            addScheduleDishWindow.ShowDialog();

            if (newDish.Dish.Name == "None")
                return;

            updateDishList(newDish);
            updateIngredientList();
        }

        private void deleteDishButton_Click(object sender, RoutedEventArgs e)
        {
            if (dishListview.SelectedItem == null)
                return;

            scheduledList.Remove(dishListview.SelectedItem as ScheduleDish);
            updateDishList(null);
            updateIngredientList();
        }
    }
}
