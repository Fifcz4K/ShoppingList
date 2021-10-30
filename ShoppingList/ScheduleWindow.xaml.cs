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
        List<ScheduleDish> scheduledList = new List<ScheduleDish>();
        List<IngredientShoppingList> ingredientList = new List<IngredientShoppingList>();
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
    }
}
