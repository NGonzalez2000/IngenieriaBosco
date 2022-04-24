﻿using IngenieriaBosco.Core.Models.Controls;
using IngenieriaBosco.Core.Resources;
using IngenieriaBosco.Core.ViewModels;
using IngenieriaBosco.Interface;
using MaterialDesignThemes.Wpf;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace IngenieriaBosco
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            PaletteHelper paletteHelper = new();
            paletteHelper.SetTheme(new ThemeSetUp().Init());

            if (GeneralSettings.Default.NeedsUpdate)
            {
                ThemeSettings.Default.Upgrade();
                DataBaseSettings.Default.Upgrade();
                GeneralSettings.Default.Upgrade();
                GeneralSettings.Default.NeedsUpdate = false;
                GeneralSettings.Default.Save();
            }

            Task.Factory.StartNew(() => Thread.Sleep(2500)).ContinueWith(t =>
            {
                //note you can use the message queue from any thread, but just for the demo here we 
                //need to get the message queue from the snackbar, so need to be on the dispatcher
                MainSnackbar.MessageQueue?.Enqueue("Bienvenido a ClickSell");
            }, TaskScheduler.FromCurrentSynchronizationContext());

            DataContext = new MainWindowsViewModel(MainSnackbar.MessageQueue!);

            string sqlException = DBAccess.TestConnection();
            if (sqlException != string.Empty)
                FixConnection(sqlException);

            AddDynamicResources();
        }

        

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //until we had a StaysOpen glag to Drawer, this will help with scroll bars
            var dependencyObject = Mouse.Captured as DependencyObject;

            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar) return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            MenuToggleButton.IsChecked = false;
        }
        private void OnCopy(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is string stringValue)
            {
                try
                {
                    Clipboard.SetDataObject(stringValue);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.ToString());
                }
            }
        }
        private static void FixConnection(string sqlException)
        {
            SqlExceptionControl sqlExceptionControl = new(sqlException);
            sqlExceptionControl.ShowDialog();
        }
        private void AddDynamicResources()
        {
            Resources["HeaderFontSize"] = GeneralSettings.Default.HeaderFontSize;
            Resources["SecondaryHeaderFontSize"] = GeneralSettings.Default.SecondaryHeaderFontSize;
            Resources["ButtonFontSize"] = GeneralSettings.Default.ButtonFontSize;
            Resources["GeneralFontSize"] = GeneralSettings.Default.GeneralFontSize;
            Resources["GridFontSize"] = GeneralSettings.Default.GridFontSize;
        }
    }
}
