using ShoppingList.Classes;
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

namespace ShoppingList
{
    /// <summary>
    /// Interaction logic for AddDishWindow.xaml
    /// </summary>
    public partial class AddDishWindow : Window
    {
        List<Dish> dishList;
        public AddDishWindow(ref List<Dish> dishList)
        {
            InitializeComponent();
            this.dishList = dishList;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(dishNameTextbox.Text) || !char.IsLetter(dishNameTextbox.Text[0]))
            {
                MessageBox.Show("Dish needs a valid name", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dishList.Add(new Dish(dishNameTextbox.Text, null));
            Close();
        }
    }
}
