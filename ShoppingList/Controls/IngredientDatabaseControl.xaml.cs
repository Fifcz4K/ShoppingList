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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShoppingList.Controls
{
    /// <summary>
    /// Interaction logic for IngredientDatabaseControl.xaml
    /// </summary>
    public partial class IngredientDatabaseControl : UserControl
    {
        public Ingredient Ingredient
        {
            get { return (Ingredient)GetValue(IngredientProperty); }
            set { SetValue(IngredientProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Ingredient.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IngredientProperty =
            DependencyProperty.Register("Ingredient", typeof(Ingredient), typeof(IngredientDatabaseControl), new PropertyMetadata(null, SetText));

        private static void SetText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            IngredientDatabaseControl control = d as IngredientDatabaseControl;
            if(control != null)
            {
                control.ingredientNameTextblock.Text = (e.NewValue as Ingredient).Name;
                control.ingredientCategoryTextblock.Text = "Category: " + (e.NewValue as Ingredient).Category.ToString();
            }
        }

        public IngredientDatabaseControl()
        {
            InitializeComponent();
        }
    }
}
