using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace WpfLabs.CustomPixelShaderDemo.Effects
{
    public class GrayscaleEffect : ShaderEffect
    {
        /// <summary>
        /// A refernce to the pixel shader used.
        /// </summary>
        private static PixelShader pixelShader = new PixelShader();

        static GrayscaleEffect()
        {
            pixelShader.UriSource = EffectUriHelper.GetUri(nameof(GrayscaleEffect));
        }

        public GrayscaleEffect()
        {
            PixelShader = pixelShader;
            UpdateShaderValue(InputProperty);
        }

        public static readonly DependencyProperty InputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(GrayscaleEffect), 0 /* assigned to sampler register S0 */);
        
        public Brush Input
        {
            get { return (Brush)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }
    }
}
