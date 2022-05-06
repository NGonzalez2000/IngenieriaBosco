﻿using System;
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
    /// Interaction logic for ProductDialog.xaml
    /// </summary>
    public partial class ProductDialog : UserControl
    {
        public ProductDialog(string header, SelectionChangedEventHandler selectionChanged)
        {
            InitializeComponent();
            HeaderTextBlock.Text = header;
            CategoryComboBox.SelectionChanged += selectionChanged;
            
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text.Last())) e.Handled = true;
        }

        
    }
}