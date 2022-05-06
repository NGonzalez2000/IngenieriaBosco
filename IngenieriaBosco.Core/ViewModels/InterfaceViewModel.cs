using IngenieriaBosco.Core.Models.Controls;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace IngenieriaBosco.Core.ViewModels
{
    public class InterfaceViewModel : BaseViewModel
    {
        private int headerFontSize;
        private int secondaryHeaderFontSize;
        private int buttonFontSize;
        private int gridFontSize;
        private int generalFontSize;
        public ICommand ChangeToPrimaryCommand => new RelayCommand(o => ChangeScheme(ColorScheme.Primary));
        public ICommand ChangeToSecondaryCommand => new RelayCommand(o => ChangeScheme(ColorScheme.Secondary));
        public ICommand SaveSettingCommand => new RelayCommand(SaveSettings);
        public int HeaderFontSize
        {
            get => headerFontSize;
            set
            {
                if(SetProperty(ref headerFontSize, value))
                    Application.Current.Resources[nameof(HeaderFontSize)] = Convert.ToDouble(headerFontSize);
            }
        }
        public int SecondaryHeaderFontSize
        {
            get => secondaryHeaderFontSize;
            set
            {
                if (SetProperty(ref secondaryHeaderFontSize, value))
                    Application.Current.Resources[nameof(SecondaryHeaderFontSize)] = Convert.ToDouble(secondaryHeaderFontSize);
            }
        }
        public int ButtonFontSize
        {
            get => buttonFontSize;
            set
            {
                if (SetProperty(ref buttonFontSize, value))
                    Application.Current.Resources[nameof(ButtonFontSize)] = Convert.ToDouble(buttonFontSize);
            }
        }
        public int GridFontSize
        {
            get => gridFontSize;
            set
            {
                if (SetProperty(ref gridFontSize, value))
                    Application.Current.Resources[nameof(GridFontSize)] = Convert.ToDouble(gridFontSize);
            }
        }
        public int GeneralFontSize
        {
            get => generalFontSize;
            set
            {
                if (SetProperty(ref generalFontSize, value))
                    Application.Current.Resources[nameof(GeneralFontSize)] = Convert.ToDouble(generalFontSize);
            }
        }

        public InterfaceViewModel(ISnackbarMessageQueue snackbarMessageQueue) : base(snackbarMessageQueue)
        {
            InterfaceControls = new();
            ThemeControls = new();
            HeaderFontSize = Convert.ToInt32(GeneralSettings.Default.HeaderFontSize);
            SecondaryHeaderFontSize = Convert.ToInt32(GeneralSettings.Default.SecondaryHeaderFontSize);
            ButtonFontSize = Convert.ToInt32(GeneralSettings.Default.ButtonFontSize);
            GridFontSize = Convert.ToInt32(GeneralSettings.Default.GridFontSize);
            GeneralFontSize = Convert.ToInt32(GeneralSettings.Default.GeneralFontSize);
        }
        public override void Load()
        {
            
        }

        public InterfaceControls? InterfaceControls { get; set; }
        public ThemeControls? ThemeControls { get; set; }

        private async void SaveSettings(object? obj)
        {

            ThemeSettings.Default.PrimaryColor = ToDrawing(InterfaceControls!._primaryColor);
            ThemeSettings.Default.AccentColor = ToDrawing(InterfaceControls._secondaryColor);

            ThemeSettings.Default.DesiredContrastRatio = ThemeControls!.DesiredContrastRatio;
            ThemeSettings.Default.IsNone = ThemeControls.ContrastValue == Contrast.None;
            ThemeSettings.Default.IsLow = ThemeControls.ContrastValue == Contrast.Low;
            ThemeSettings.Default.IsMedium = ThemeControls.ContrastValue == Contrast.Medium;
            ThemeSettings.Default.IsHigh = ThemeControls.ContrastValue == Contrast.High;
            ThemeSettings.Default.IsPrimary = ThemeControls.ColorSelectionValue == ColorSelection.Primary;
            ThemeSettings.Default.IsSecondary = ThemeControls.ColorSelectionValue == ColorSelection.Secondary;
            ThemeSettings.Default.IsAll = ThemeControls.ColorSelectionValue == ColorSelection.All;
            ThemeSettings.Default.IsColorAdjusted = ThemeControls.IsColorAdjusted;
            
            ThemeSettings.Default.Save();

            GeneralSettings.Default.HeaderFontSize = Convert.ToDouble(HeaderFontSize);
            GeneralSettings.Default.SecondaryHeaderFontSize = Convert.ToDouble(SecondaryHeaderFontSize);
            GeneralSettings.Default.ButtonFontSize = Convert.ToDouble(ButtonFontSize);
            GeneralSettings.Default.GridFontSize = Convert.ToDouble(GridFontSize);
            GeneralSettings.Default.GeneralFontSize = Convert.ToDouble(GeneralFontSize);

            GeneralSettings.Default.Save();

            await AcceptCall("Guardado con éxito");
        }

        private static System.Drawing.Color ToDrawing(Color? color1)
        {
            if (!color1.HasValue) throw new ArgumentNullException("InterfaceViewModel > ToDrawing " + nameof(color1) + "is null");
            Color color = (Color)color1;
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        private void ChangeScheme(ColorScheme scheme)
        {
            InterfaceControls!.ActiveScheme = scheme;
            if (InterfaceControls.ActiveScheme == ColorScheme.Primary)
            {
                InterfaceControls.SelectedColor = InterfaceControls._primaryColor;
            }
            else if (InterfaceControls.ActiveScheme == ColorScheme.Secondary)
            {
                InterfaceControls.SelectedColor = InterfaceControls._secondaryColor;
            }
            else if (InterfaceControls.ActiveScheme == ColorScheme.PrimaryForeground)
            {
                InterfaceControls.SelectedColor = InterfaceControls._primaryForegroundColor;
            }
            else if (InterfaceControls.ActiveScheme == ColorScheme.SecondaryForeground)
            {
                InterfaceControls.SelectedColor = InterfaceControls._secondaryForegroundColor;
            }
        }
    }
}
