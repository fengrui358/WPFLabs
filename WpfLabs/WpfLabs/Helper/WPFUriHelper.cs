using System;
using System.Reflection;
using System.Text;

namespace WpfLabs.Helper
{
    public static class WPFUriHelper
    {
        public static Uri MakePackUri(string relativeFile)
        {
            StringBuilder uriString = new StringBuilder(); ;
#if !SILVERLIGHT 
            uriString.Append("pack://application:,,,");
#endif
            uriString.Append("/" + AssemblyShortName + ";component/" + relativeFile);
            return new Uri(uriString.ToString(), UriKind.RelativeOrAbsolute);
        }

        private static string AssemblyShortName
        {
            get
            {
                if (_assemblyShortName == null)
                {
                    Assembly a = typeof(WPFUriHelper).Assembly;

                    // Pull out the short name.
                    _assemblyShortName = a.ToString().Split(',')[0];
                }

                return _assemblyShortName;
            }
        }

        private static string _assemblyShortName;
    }
}
