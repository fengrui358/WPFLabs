using BOL_Model.Enum;
using GalaSoft.MvvmLight;

namespace BOL_Model
{
    /// <summary>
    /// 消防专业警情。
    /// </summary>
    public class IncidentFireControlModel : ObservableObject
    {

        /// <summary>
        /// 楼房层数。
        /// </summary>
        private int _buildingFloors;
        /// <summary>
        /// 楼房层数。
        /// </summary>
        public int BuildingFloors
        {
            get { return _buildingFloors; }
            set { Set(ref _buildingFloors, value); }
        }

        /// <summary>
        /// 燃烧楼层。
        /// </summary>
        private int _burningStorey;
        /// <summary>
        /// 燃烧楼层。
        /// </summary>
        public int BurningStorey
        {
            get { return _burningStorey; }
            set { Set(ref _burningStorey, value); }
        }



        /// <summary>
        /// 是否有人员被困。
        /// </summary>
        private bool _isTrapped;
        /// <summary>
        /// 是否有人员被困。
        /// </summary>
        public bool IsTrapped
        {
            get { return _isTrapped; }
            set { Set(ref _isTrapped, value); }
        }

        /// <summary>
        /// 是否有人员受伤。
        /// </summary>
        private bool _hasInjured;
        /// <summary>
        /// 是否有人员受伤。
        /// </summary>
        public bool HasInjured
        {
            get { return _hasInjured; }
            set { Set(ref _hasInjured, value); }
        }
        
    }
}