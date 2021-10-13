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

namespace ShoppingList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Dish> dishList = new List<Dish>();
        List<string> productList = new List<String>();
        public MainWindow()
        {
            InitializeComponent();
            dishList.Add(new Dish("Pizza", "ciasto", "salami", "ser"));
            dishList.Add(new Dish("Spaghetti", "makaron", "sos", "mięsko"));

            foreach (Dish dish in dishList)
            {
                foreach (Product product in dish.Products)
                {
                    productList.Add(product.Name);
                }
            }

            dishListView.ItemsSource = dishList;
            productListView.ItemsSource = productList;
        }
    }
}
