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
    /// Interaction logic for AddScheduleDishWindow.xaml
    /// </summary>
    public partial class AddScheduleDishWindow : Window
    {
        List<Dish> dishList = new List<Dish>();
        ScheduleDish scheduleDish = new ScheduleDish();
        public AddScheduleDishWindow(ref ScheduleDish outDish)
        {
            InitializeComponent();
            ReadDatabase();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            dayCombobox.ItemsSource = Enum.GetValues(typeof(Days)).Cast<Days>();
            dishCombobox.ItemsSource = dishList;
            scheduleDish = outDish;
        }

        void ReadDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Dish>();
                dishList = connection.Table<Dish>().ToList();
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (dayCombobox.SelectedItem == null)
                return;
            if (dishCombobox.SelectedItem == null)
                return;

            scheduleDish.Day = (Days)dayCombobox.SelectedItem;
            scheduleDish.Dish = (Dish)dishCombobox.SelectedItem;
            Close();
        }
    }
}
