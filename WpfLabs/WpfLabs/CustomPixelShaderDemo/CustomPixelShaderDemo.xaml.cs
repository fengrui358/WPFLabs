using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfLabs.CustomPixelShaderDemo.Effects;

namespace WpfLabs.CustomPixelShaderDemo
{
    /// <summary>
    /// CustomPixelShaderDemo.xaml 的交互逻辑
    /// </summary>
    public partial class CustomPixelShaderDemo : Window, INotifyPropertyChanged
    {
        private List<string> _paints;

        public List<string> Paints
        {
            get { return _paints; }
            set
            {
                _paints = value;
                OnPropertyChanged();
            }
        }

        private List<ShaderEffect> _effects;

        public List<ShaderEffect> Effects
        {
            get { return _effects; }
            set
            {
                _effects = value;
                OnPropertyChanged();
            }
        }

        private ShaderEffect _selectedEffect;

        public ShaderEffect SelectedEffect
        {
            get { return _selectedEffect; }
            set
            {
                _selectedEffect = value;
                Img.Effect = _selectedEffect;
                OnPropertyChanged();
            }
        }

        public CustomPixelShaderDemo()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void CustomPixelShaderDemo_OnLoaded(object sender, RoutedEventArgs e)
        {
            var paints = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                paints.Add($"0{i+1}");
            }

            Paints = paints;

            var effects = new List<ShaderEffect>();

            var shaderEffectTypes = GetType().Assembly.GetTypes().Where(s => typeof(ShaderEffect).IsAssignableFrom(s));

            foreach (var shaderEffectType in shaderEffectTypes)
            {
                effects.Add((ShaderEffect)Activator.CreateInstance(shaderEffectType));
            }

            Effects = effects;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CleanBtn_OnClick(object sender, RoutedEventArgs e)
        {
            SelectedEffect = null;
        }
    }
}
