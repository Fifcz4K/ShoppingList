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
    /// Interaction logic for ModifyDishWindow.xaml
    /// </summary>
    public partial class ModifyDishWindow : Window
    {
        List<Dish> dishList;
        int index;
        public ModifyDishWindow(ref List<Dish> dishList, int index)
        {
            InitializeComponent();
            this.dishList = dishList;
            this.index = index;
            dishNameTextbox.Text = dishList[index].Name;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(dishNameTextbox.Text) || !char.IsLetter(dishNameTextbox.Text[0]))
            {
                MessageBox.Show("Dish needs a valid name", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dishList[index].Name = dishNameTextbox.Text;
            Close();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            dishList.RemoveAt(index);
            Close();
        }
    }
}
