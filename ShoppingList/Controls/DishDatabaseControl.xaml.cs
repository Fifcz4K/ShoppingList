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
using ShoppingList.Classes;

namespace ShoppingList.Controls
{
    /// <summary>
    /// Interaction logic for DishDatabaseControl.xaml
    /// </summary>
    public partial class DishDatabaseControl : UserControl
    {
        public Dish Dish
        {
            get { return (Dish)GetValue(DishProperty); }
            set { SetValue(DishProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Dish.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DishProperty =
            DependencyProperty.Register("Dish", typeof(Dish), typeof(DishDatabaseControl), new PropertyMetadata(null, SetText));

        private static void SetText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DishDatabaseControl control = d as DishDatabaseControl;
            if(control != null)
            {
                control.dishNameTextblock.Text = (e.NewValue as Dish).Name;
            }
        }

        public DishDatabaseControl()
        {
            InitializeComponent();
        }
    }
}
