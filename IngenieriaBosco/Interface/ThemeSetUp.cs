using IngenieriaBosco.Core.Resources;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace IngenieriaBosco.Interface
{
    public class ThemeSetUp
    {
        public ITheme Init()
        {
            ITheme theme = new PaletteHelper().GetTheme();

            // Setea el tipo de Theme
            theme.SetBaseTheme(ThemeSettings.Default.IsDarkCheck ? Theme.Dark : Theme.Light);

            // Setea el color primario
            System.Drawing.Color color = ThemeSettings.Default.PrimaryColor;
            theme.SetPrimaryColor(Color.FromArgb(color.A, color.R, color.G, color.B));

            // Setea el color secundario
            color = ThemeSettings.Default.AccentColor;
            theme.SetSecondaryColor(Color.FromArgb(color.A,color.R, color.G, color.B));

            //Setea el valor de contraste
            float DesiredContrastRatio = ThemeSettings.Default.DesiredContrastRatio;
            if (theme is Theme internalTheme && internalTheme.ColorAdjustment != null)
                internalTheme.ColorAdjustment.DesiredContrastRatio = DesiredContrastRatio;

            //Setea el tipo de contraste
            Contrast ContrastValue = new();
            if (ThemeSettings.Default.IsNone) ContrastValue = Contrast.None;
            else if (ThemeSettings.Default.IsLow) ContrastValue = Contrast.Low;
            else if (ThemeSettings.Default.IsMedium) ContrastValue = Contrast.Medium;
            else if (ThemeSettings.Default.IsHigh) ContrastValue = Contrast.High;
            if (theme is Theme internalTheme2 && internalTheme2.ColorAdjustment != null)
                internalTheme2.ColorAdjustment.Contrast = ContrastValue;

            //Setea los colores a los que ponerle el contraste
            ColorSelection ColorSelectionValue = new();
            if (ThemeSettings.Default.IsAll) ColorSelectionValue = ColorSelection.All;
            else if (ThemeSettings.Default.IsPrimary) ColorSelectionValue = ColorSelection.Primary;
            else if (ThemeSettings.Default.IsSecondary) ColorSelectionValue = ColorSelection.Secondary;
            else ColorSelectionValue = ColorSelection.None;
            if (theme is Theme internalTheme3)
            {
                internalTheme3.ColorAdjustment = ThemeSettings.Default.IsColorAdjusted
                    ? new ColorAdjustment
                    {
                        DesiredContrastRatio = DesiredContrastRatio,
                        Contrast = ContrastValue,
                        Colors = ColorSelectionValue
                    }
                    : null;
            }
            return theme;
        }

        
    }
}
