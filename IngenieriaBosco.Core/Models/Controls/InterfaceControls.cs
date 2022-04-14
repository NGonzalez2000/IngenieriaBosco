using IngenieriaBosco.Core.Resources;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace IngenieriaBosco.Core.Models.Controls
{
    public class InterfaceControls : INotify
    {
        private readonly PaletteHelper _paletteHelper = new();

        private ColorScheme _activeScheme;
        public Color? _selectedColor; 
        public Color? _primaryColor;
        public Color? _secondaryColor;
        public Color? _primaryForegroundColor;
        public Color? _secondaryForegroundColor;

        public InterfaceControls()
        {
            PaletteHelper paletteHelper = new();
            ITheme theme = paletteHelper.GetTheme();
            _primaryColor = theme.PrimaryMid.Color;
            _secondaryColor = theme.SecondaryMid.Color;

            SelectedColor = _primaryColor;
        }
        public ColorScheme ActiveScheme
        {
            get => _activeScheme;
            set
            {
                if (_activeScheme != value)
                {
                    _activeScheme = value;
                    OnPropertyChanged();
                }
            }
        }
        public Color? SelectedColor
        {
            get => _selectedColor;
            set
            {
                if (_selectedColor != value)
                {
                    _selectedColor = value;
                    OnPropertyChanged();

                    // if we are triggering a change internally its a hue change and the colors will match
                    // so we don't want to trigger a custom color change.
                    var currentSchemeColor = ActiveScheme switch
                    {
                        ColorScheme.Primary => _primaryColor,
                        ColorScheme.Secondary => _secondaryColor,
                        ColorScheme.PrimaryForeground => _primaryForegroundColor,
                        ColorScheme.SecondaryForeground => _secondaryForegroundColor,
                        _ => throw new NotSupportedException($"{ActiveScheme} is not a handled ColorScheme.. Ye daft programmer!")
                    };

                    if (_selectedColor != currentSchemeColor && value is Color color)
                    {
                        ChangeCustomColor(color);
                    }
                }
            }
        }

        private void ChangeCustomColor(object obj)
        {
            var color = (Color)obj;

            if (ActiveScheme == ColorScheme.Primary)
            {
                _paletteHelper.ChangePrimaryColor(color);
                _primaryColor = color;
            }
            else if (ActiveScheme == ColorScheme.Secondary)
            {
                _paletteHelper.ChangeSecondaryColor(color);
                _secondaryColor = color;
            }
            else if (ActiveScheme == ColorScheme.PrimaryForeground)
            {
                SetPrimaryForegroundToSingleColor(color);
                _primaryForegroundColor = color;
            }
            else if (ActiveScheme == ColorScheme.SecondaryForeground)
            {
                SetSecondaryForegroundToSingleColor(color);
                _secondaryForegroundColor = color;
            }
        }
        private void SetPrimaryForegroundToSingleColor(Color color)
        {
            ITheme theme = _paletteHelper.GetTheme();

            theme.PrimaryLight = new ColorPair(theme.PrimaryLight.Color, color);
            theme.PrimaryMid = new ColorPair(theme.PrimaryMid.Color, color);
            theme.PrimaryDark = new ColorPair(theme.PrimaryDark.Color, color);

            _paletteHelper.SetTheme(theme);
        }

        private void SetSecondaryForegroundToSingleColor(Color color)
        {
            ITheme theme = _paletteHelper.GetTheme();

            theme.SecondaryLight = new ColorPair(theme.SecondaryLight.Color, color);
            theme.SecondaryMid = new ColorPair(theme.SecondaryMid.Color, color);
            theme.SecondaryDark = new ColorPair(theme.SecondaryDark.Color, color);

            _paletteHelper.SetTheme(theme);
        }
    }
}
