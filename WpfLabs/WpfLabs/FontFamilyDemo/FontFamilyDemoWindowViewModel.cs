using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using FontStyle = System.Windows.FontStyle;

namespace WpfLabs.FontFamilyDemo
{
    public class FontFamilyDemoWindowViewModel : ObservableObject
    {
        public string AliFontPath { get; set; } = "pack://application:,,,/WpfLabs;component/FontFamilyDemo/#阿里巴巴普惠体 R";

        private Dictionary<string, FontWeight> _fontWeights;

        public Dictionary<string, FontWeight> FontWeights
        {
            get { return _fontWeights; }
            set { Set(ref _fontWeights, value); }
        }

        private KeyValuePair<string, FontWeight> _selectedFontWeight;

        public KeyValuePair<string, FontWeight> SelectedFontWeight
        {
            get { return _selectedFontWeight; }
            set { Set(ref _selectedFontWeight, value); }
        }

        private Dictionary<string, FontStyle> _fontStyles;

        public Dictionary<string, FontStyle> FontStyles
        {
            get { return _fontStyles; }
            set { Set(ref _fontStyles, value); }
        }

        private KeyValuePair<string, FontStyle> _selectedFontStyle;

        public KeyValuePair<string, FontStyle> SelectedFontStyle
        {
            get { return _selectedFontStyle; }
            set { Set(ref _selectedFontStyle, value); }
        }

        private Dictionary<string, FontStretch> _fontStretches;

        public Dictionary<string, FontStretch> FontStretches
        {
            get { return _fontStretches; }
            set { Set(ref _fontStretches, value); }
        }

        private KeyValuePair<string, FontStretch> _selectedFontStretch;

        public KeyValuePair<string, FontStretch> SelectedFontStretch
        {
            get { return _selectedFontStretch; }
            set { Set(ref _selectedFontStretch, value); }
        }

        private List<FontFamily> _systemFontFamilies;

        public List<FontFamily> SystemFontFamilies
        {
            get { return _systemFontFamilies; }
            set { Set(ref _systemFontFamilies, value); }
        }

        public FontFamilyDemoWindowViewModel()
        {
            var tempFontWeights = new Dictionary<string, FontWeight>();
            var fontWeightProperties =
                typeof(System.Windows.FontWeights).GetProperties(BindingFlags.Public | BindingFlags.Static);
            foreach (var propertyInfo in fontWeightProperties)
            {
                tempFontWeights.Add(propertyInfo.Name, (FontWeight) propertyInfo.GetValue(null));
            }

            FontWeights = tempFontWeights;
            SelectedFontWeight = FontWeights
                .FirstOrDefault(s => s.Key.Equals("Normal", StringComparison.OrdinalIgnoreCase));

            var tempFontStyles = new Dictionary<string, FontStyle>();
            var fontStylesProperties =
                typeof(System.Windows.FontStyles).GetProperties(BindingFlags.Public | BindingFlags.Static);
            foreach (var propertyInfo in fontStylesProperties)
            {
                tempFontStyles.Add(propertyInfo.Name, (FontStyle) propertyInfo.GetValue(null));
            }

            FontStyles = tempFontStyles;
            SelectedFontStyle = FontStyles
                .FirstOrDefault(s => s.Key.Equals("Normal", StringComparison.OrdinalIgnoreCase));

            var tempFontStretches = new Dictionary<string, FontStretch>();
            var FontStretchsProperties =
                typeof(System.Windows.FontStretches).GetProperties(BindingFlags.Public | BindingFlags.Static);
            foreach (var propertyInfo in FontStretchsProperties)
            {
                tempFontStretches.Add(propertyInfo.Name, (FontStretch) propertyInfo.GetValue(null));
            }

            FontStretches = tempFontStretches;
            SelectedFontStretch = FontStretches
                .FirstOrDefault(s => s.Key.Equals("Normal", StringComparison.OrdinalIgnoreCase));

            var tempSystemFontFamilies = new List<FontFamily>();

            //增加自定义字体
            var aileron = new FontFamily(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                @"FontFamilyDemo\FontFiles\#Aileron"));

            var aliFont = new FontFamily(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                @"FontFamilyDemo\FontFiles\#阿里巴巴普惠体 R"));

            tempSystemFontFamilies.Add(aileron);
            tempSystemFontFamilies.Add(aliFont);

            foreach (var systemFontFamily in Fonts.SystemFontFamilies)
            {
                tempSystemFontFamilies.Add(systemFontFamily);
            }

            SystemFontFamilies = tempSystemFontFamilies;
        }
    }
}