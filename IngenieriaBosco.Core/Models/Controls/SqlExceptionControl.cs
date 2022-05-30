
using IngenieriaBosco.Front.Windows;
using System.Windows;
using System.Windows.Input;

namespace IngenieriaBosco.Core.Models.Controls
{
    public class SqlExceptionControl : INotify
    {
        private string exceptionMessage;
        public string IPTextBox { get; set; }
        public string PortTextBox { get; set; }
        public string DataBaseTextBox { get; set; }
        public string UserTextBox { get; set; }
        public string PasswordTextBox { get; set; }
        public string ExceptionMessage
        {
            get => exceptionMessage;
            set => SetProperty(ref exceptionMessage, value);
        }
        public SqlWindow sqlWindow { get; set; }
        public SqlExceptionControl(string sqlExceptionMessage)
        {
            IPTextBox = DataBaseSettings.Default.IP;
            PortTextBox = DataBaseSettings.Default.Port;
            DataBaseTextBox = DataBaseSettings.Default.DataBase;
            UserTextBox = DataBaseSettings.Default.User;
            PasswordTextBox = DataBaseSettings.Default.Password;
            exceptionMessage = sqlExceptionMessage;
            sqlWindow = new ();
            sqlWindow.DataContext = this;
        }
        public void ShowDialog()
            => sqlWindow.ShowDialog();
        public ICommand AcceptCommand => new RelayCommand(_ => AcceptExecute());
        private void AcceptExecute()
        {
            DataBaseSettings.Default.IP = IPTextBox;
            DataBaseSettings.Default.Port = PortTextBox;
            DataBaseSettings.Default.DataBase = DataBaseTextBox;
            DataBaseSettings.Default.User = UserTextBox;
            DataBaseSettings.Default.Password = PasswordTextBox;

            DataBaseSettings.Default.Save();

            ExceptionMessage = DBAccess.TestConnection();

            if (string.IsNullOrEmpty(ExceptionMessage)) sqlWindow.Close();
        }
        public static ICommand CancelCommand => new RelayCommand(_ => CancelExecute());
        private static void CancelExecute()
        {
            Application.Current.Shutdown();
        }
    }
}
