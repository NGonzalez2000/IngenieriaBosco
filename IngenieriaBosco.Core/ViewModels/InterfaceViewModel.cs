using IngenieriaBosco.Core.Models.Controls;
using IngenieriaBosco.Core.Resources;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace IngenieriaBosco.Core.ViewModels
{
    public class InterfaceViewModel : BaseViewModel
    {
        public ICommand ChangeToPrimaryCommand => new RelayCommand(o => ChangeScheme(ColorScheme.Primary));
        public ICommand ChangeToSecondaryCommand => new RelayCommand(o => ChangeScheme(ColorScheme.Secondary));
        public ICommand SaveSettingCommand => new RelayCommand(SaveSettings);

        public InterfaceViewModel(ISnackbarMessageQueue snackbarMessageQueue) : base(snackbarMessageQueue)
        {
            InterfaceControls = new();
            ThemeControls = new();
        }
        public override void Load()
        {
            
        }

        public InterfaceControls? InterfaceControls { get; set; }
        public ThemeControls? ThemeControls { get; set; }

        private void SaveSettings(object? obj)
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
