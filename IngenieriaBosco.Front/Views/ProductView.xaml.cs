using System.Windows;
using System.Windows.Controls;

namespace IngenieriaBosco.Front.Views
{
    /// <summary>
    /// Interaction logic for ProductView.xaml
    /// </summary>
    public partial class ProductView : UserControl
    {
        public ProductView()
        {
            InitializeComponent();
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            FilterPopupBox.IsPopupOpen = false;
        }

        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            SortPopupBox.IsPopupOpen = false;
        }
    }
}
