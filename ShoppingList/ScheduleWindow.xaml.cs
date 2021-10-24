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
        public ScheduleWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void updateDishList(ScheduleDish newDish)
        {
            scheduledList.Add(newDish);
            if (scheduledList != null)
                dishListview.ItemsSource = scheduledList.Select(x => x.Dish).ToList();
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
        }
    }
}
