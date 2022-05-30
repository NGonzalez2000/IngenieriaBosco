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

namespace IngenieriaBosco.Front.Views
{
    /// <summary>
    /// Interaction logic for ClientView.xaml
    /// </summary>
    public partial class ClientView : UserControl
    {
        public ClientView()
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
