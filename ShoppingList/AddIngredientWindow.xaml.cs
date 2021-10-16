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
    /// Interaction logic for AddIngredientWindow.xaml
    /// </summary>
    public partial class AddIngredientWindow : Window
    {
        Dish dish;
        public AddIngredientWindow(Dish dish)
        {
            InitializeComponent();
            this.dish = dish;
            categoryCombobox.ItemsSource = Enum.GetValues(typeof(IngredientCategory)).Cast<IngredientCategory>();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(ingredientNameTextbox.Text) || !char.IsLetter(ingredientNameTextbox.Text[0]))
            {
                MessageBox.Show("Ingredient needs a valid name", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(categoryCombobox.SelectedItem == null)
            {
                MessageBox.Show("You have to choose a category of an ingredient", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dish.AddIngredient(new Ingredient(ingredientNameTextbox.Text, (IngredientCategory)categoryCombobox.SelectedItem));

            Close();
        }
    }
}
