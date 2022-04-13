using IngenieriaBosco.Front.Converters;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;

namespace IngenieriaBosco.Front.Controls.TextBoxs
{
    public class MoneyTextBox : TextBox
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
                    binding.Converter = new EsARConverter();
                    binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    SetBinding(TextProperty, binding);
                };
            }
        }
        public MoneyTextBox()
        {
            bindingPath = string.Empty;
            TextChanged += MoneyTextBox_TextChanged;
            SelectionChanged += MoneyTextBox_SelectionChanged;
            PreviewTextInput += MoneyTextBox_PreviewTextInput;
        }

        private void MoneyTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            SelectionChanged -= MoneyTextBox_SelectionChanged;
            CaretIndex = int.MaxValue;
            SelectionChanged += MoneyTextBox_SelectionChanged;
        }

        private void MoneyTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]))
            {
                e.Handled = true;
            }
        }


        private void MoneyTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox MoneyTextBox = (TextBox)sender;
            //Remove previous formatting, or the decimal check will fail including leading zeros
            string value = MoneyTextBox.Text.Replace(".", "")
                .Replace("$", "").Replace(",", "").TrimStart('0');

            //Check we are indeed handling a number
            if (decimal.TryParse(value, out decimal ul))
            {
                ul /= 100;
                //Unsub the event so we don't enter a loop
                MoneyTextBox.TextChanged -= MoneyTextBox_TextChanged;
                //Format the text as currency
                MoneyTextBox.Text = string.Format(CultureInfo.CreateSpecificCulture("es-AR"), "{0:C2}", ul);
                MoneyTextBox.TextChanged += MoneyTextBox_TextChanged;
                MoneyTextBox.Select(MoneyTextBox.Text.Length, 0);
            }
            bool goodToGo = TextisValid(MoneyTextBox.Text);

            //enterButton.Enabled = goodToGo;

            if (!goodToGo)
            {
                MoneyTextBox.Text = "$ 0,00";
                MoneyTextBox.Select(MoneyTextBox.Text.Length, 0);
            }
            MoneyTextBox.CaretIndex = int.MaxValue;
        }

        private bool TextisValid(string text)
        {
            Regex money = new(@"^\$ (\d{1,3}(\.\d{3})*|(\d+))(\,\d{2})?$");
            return money.IsMatch(text);
        }

    }
}
