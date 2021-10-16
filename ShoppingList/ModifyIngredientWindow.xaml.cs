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
    /// Interaction logic for ModifyIngredientWindow.xaml
    /// </summary>
    public partial class ModifyIngredientWindow : Window
    {
        Ingredient ingredient;
        Dish dish;
        public ModifyIngredientWindow(Ingredient ingredient, Dish dish)
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            categoryCombobox.ItemsSource = Enum.GetValues(typeof(IngredientCategory)).Cast<IngredientCategory>();
            this.dish = dish;
            this.ingredient = ingredient;
            categoryCombobox.SelectedItem = ingredient.Category;
            ingredientNameTextbox.Text = ingredient.Name;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ingredientNameTextbox.Text) || !char.IsLetter(ingredientNameTextbox.Text[0]))
            {
                MessageBox.Show("Ingredient needs a valid name", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dish.UpdateIngredient(ingredient, new Ingredient(ingredientNameTextbox.Text, (IngredientCategory)categoryCombobox.SelectedItem));

            Close();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            dish.RemoveIngredient(ingredient);
            Close();
        }
    }
}
