using IngenieriaBosco.Front.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace IngenieriaBosco.Front.Controls.TextBoxs
{
    internal class IntTextBox : TextBox
    {
        private string bindingPath;
        public string BindingPath
        {
            private get => bindingPath;
            set
            {
                if (value != null)
                {
                    bindingPath = value;
                    Binding binding = new(bindingPath)
                    {
                        Converter = new StringToInt32Converter(),
                        ValidatesOnDataErrors = true,
                        ValidatesOnExceptions = true,
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                    };
                    SetBinding(TextProperty, binding);
                };
            }
        }
        public IntTextBox()
        {
            bindingPath = string.Empty;
            SelectionChanged += IntTextBox_SelectionChanged;
            PreviewTextInput += IntTextBox_PreviewTextInput;
            Style = (Style)Application.Current.Resources["MaterialDesignFloatingHintTextBox"];

        }


        private void IntTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            SelectionChanged -= IntTextBox_SelectionChanged;
            CaretIndex = int.MaxValue;
            SelectionChanged += IntTextBox_SelectionChanged;
        }

        private void IntTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]))
            {
                e.Handled = true;
            }
        }
    }
}
