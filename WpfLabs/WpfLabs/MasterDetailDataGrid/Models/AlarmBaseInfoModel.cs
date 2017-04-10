using BOL_Model.Enum;
using GalaSoft.MvvmLight;

namespace BOL_Model
{
    /// <summary>
    /// 报警记录的基础三元信息。
    /// </summary>
    public class AlarmBaseInfoModel : ObservableObject
    {
        private string _name;
        /// <summary>
        /// 报警人姓名。
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        private string _phoneNumber;
        /// <summary>
        /// 报警人电话。
        /// </summary>
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                RaisePropertyChanged(() => PhoneNumber);
            }
        }


        private string _address;
        /// <summary>
        /// 报警人地址。
        /// </summary>
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                RaisePropertyChanged(() => Address);
            }
        }
    }
}