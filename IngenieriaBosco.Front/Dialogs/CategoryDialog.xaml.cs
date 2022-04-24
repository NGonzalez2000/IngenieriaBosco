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

namespace IngenieriaBosco.Front.Dialogs
{
    /// <summary>
    /// Interaction logic for CategoryDialog.xaml
    /// </summary>
    public partial class CategoryDialog : UserControl
    {
        public CategoryDialog(string header)
        {
            InitializeComponent();
            HeaderTextBlock.Text = header;
        }

        private void TrueButton_Click(object sender, RoutedEventArgs e)
        {
            BindingExpression binding = NameTextBox.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
        }
    }
}
