using System;
using WpfLabs.Helper;

namespace WpfLabs.CustomPixelShaderDemo.Effects
{
    public static class EffectUriHelper
    {
        private const string Effect = "Effect";

        public static Uri GetUri(string typeName)
        {
            var realName = typeName.Substring(0, typeName.LastIndexOf(Effect));
            return WPFUriHelper.MakePackUri($"CustomPixelShaderDemo/ShaderSource/{realName}.ps");
        }
    }
}
