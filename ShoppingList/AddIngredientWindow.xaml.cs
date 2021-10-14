﻿using ShoppingList.Classes;
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
            try
            {
                dish.AddIngredient(new Ingredient(ingredientNameTextbox.Text, (IngredientCategory)categoryCombobox.SelectedItem));
            }
            catch (Exception)
            {
                throw;
            }

            Close();
        }
    }
}
