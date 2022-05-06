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
    internal class CuilTextBox : TextBox
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
                    Binding binding = new(bindingPath);
                    binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    binding.ValidatesOnDataErrors = true;
                    binding.ValidatesOnExceptions = true;
                    binding.Converter = new CuilConverter();
                    SetBinding(TextProperty, binding);
                };
            }
        }
        public CuilTextBox()
        {
            Text = "00-00000000-0";
            bindingPath = string.Empty;
            TextChanged += CuilTextBox_TextChanged;
            SelectionChanged += CuilTextBox_SelectionChanged;
            PreviewTextInput += CuilTextBox_PreviewTextInput;
        }
        private void CuilTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            SelectionChanged -= CuilTextBox_SelectionChanged;
            CaretIndex = int.MaxValue;
            SelectionChanged += CuilTextBox_SelectionChanged;
        }
        private void CuilTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]))
            {
                e.Handled = true;
            }
        }
        private void CuilTextBox_TextChanged(object sender, EventArgs e)
        {
            string text = Text.Replace("-", "").TrimStart('0');
            if (text.Length == 12) text = text.Remove(11);
            text = StrReverse(text);
            while (text.Length < 11) text += "0";
            text = StrReverse(text);
            text = $"{text[..2]}-{text[2..10]}-{text[^1..]}";
            TextChanged -= CuilTextBox_TextChanged;
            Text = text;
            TextChanged += CuilTextBox_TextChanged;

        }

        private static string StrReverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
