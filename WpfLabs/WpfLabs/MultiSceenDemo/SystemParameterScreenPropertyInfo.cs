using System.Reflection;

namespace WpfLabs.MultiSceenDemo
{
    public class SystemParameterScreenPropertyInfo
    {
        private readonly PropertyInfo _propertyInfo;

        public string Name => _propertyInfo.Name;

        public string Value
        {
            get
            {
                var v = _propertyInfo.GetValue(null);
                return v.ToString();
            }
        }

        public SystemParameterScreenPropertyInfo(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;
        }
    }
}
