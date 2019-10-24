using System;
using System.Collections.ObjectModel;
using System.Windows;
using GongSolutions.Wpf.DragDrop;
using NLog;

namespace WpfLabs.DragableTabControlDemo
{
    public class DragDropViewModel : IDropTarget
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public ObservableCollection<DragableTabItemModel> LeftItems1 { get; set; }
        public ObservableCollection<DragableTabItemModel> RightItems1 { get; set; }

        public ObservableCollection<DragableTabItemModel> LeftItems2 { get; set; }
        public ObservableCollection<DragableTabItemModel> RightItems2 { get; set; }

        private static readonly Lazy<DragDropViewModel> _dragDropViewModel = new Lazy<DragDropViewModel>(() => new DragDropViewModel());

        public static DragDropViewModel Instance => _dragDropViewModel.Value;

        private DragDropViewModel()
        {
            LeftItems1 = new ObservableCollection<DragableTabItemModel>();
            RightItems1 = new ObservableCollection<DragableTabItemModel>();

            LeftItems2 = new ObservableCollection<DragableTabItemModel>();
            RightItems2 = new ObservableCollection<DragableTabItemModel>();

            for (int i = 0; i < 3; i++)
            {
                LeftItems1.Add(new DragableTabItemModel { Name = $"Left1_{i + 1}" });
                RightItems1.Add(new DragableTabItemModel { Name = $"Right1_{i + 1}" });

                LeftItems2.Add(new DragableTabItemModel { Name = $"Left2_{i + 1}" });
                RightItems2.Add(new DragableTabItemModel { Name = $"Right2_{i + 1}" });
            }
        }

        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.Data is DragableTabItemModel && dropInfo.TargetItem is DragableTabItemModel)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                dropInfo.Effects = DragDropEffects.Move;
            }
            //_logger.Info(JsonConvert.SerializeObject(dropInfo));
        }

        void IDropTarget.Drop(IDropInfo dropInfo)
        {
            if (dropInfo.Data is DragableTabItemModel sourceItem && dropInfo.TargetItem is DragableTabItemModel targetItem && dropInfo.Data != dropInfo.TargetItem)
            {
                //从来源中移除
                if (LeftItems1.Contains(sourceItem))
                {
                    LeftItems1.Remove(sourceItem);
                }
                else if (RightItems1.Contains(sourceItem))
                {
                    RightItems1.Remove(sourceItem);
                }
                else if (LeftItems2.Contains(sourceItem))
                {
                    LeftItems2.Remove(sourceItem);
                }
                else if (RightItems2.Contains(sourceItem))
                {
                    RightItems2.Remove(sourceItem);
                }

                var targetIndex = LeftItems1.IndexOf(targetItem);
                if (targetIndex >= 0)
                {
                    LeftItems1.Insert(targetIndex + 1, sourceItem);
                }
                else
                {
                    targetIndex = RightItems1.IndexOf(targetItem);
                    if (targetIndex >= 0)
                    {
                        RightItems1.Insert(targetIndex + 1, sourceItem);
                    }
                    else
                    {
                        targetIndex = LeftItems2.IndexOf(targetItem);
                        if (targetIndex >= 0)
                        {
                            LeftItems2.Insert(targetIndex + 1, sourceItem);
                        }
                        else
                        {
                            targetIndex = RightItems2.IndexOf(targetItem);
                            if (targetIndex >= 0)
                            {
                                RightItems2.Insert(targetIndex + 1, sourceItem);
                            }
                        }
                    }
                }

                _logger.Info($"Source:{sourceItem.Name} TargetItem:{targetItem.Name}");
            }

            //_logger.Info(JsonConvert.SerializeObject(dropInfo));
        }

        //void IDragSource.StartDrag(IDragInfo dragInfo)
        //{
        //    //_logger.Info(JsonConvert.SerializeObject(dragInfo));
        //}

        //bool IDragSource.CanStartDrag(IDragInfo dragInfo)
        //{
        //    //_logger.Info(JsonConvert.SerializeObject(dragInfo));
        //    return true;
        //}

        //void IDragSource.Dropped(IDropInfo dropInfo)
        //{
        //    //_logger.Info(JsonConvert.SerializeObject(dropInfo));
        //}

        //void IDragSource.DragDropOperationFinished(DragDropEffects operationResult, IDragInfo dragInfo)
        //{
        //    //_logger.Info($"{nameof(DragDropEffects)}:{operationResult},{JsonConvert.SerializeObject(dragInfo)}");
        //}

        //void IDragSource.DragCancelled()
        //{
        //    //_logger.Info(nameof(IDragSource.DragCancelled));
        //}

        //bool IDragSource.TryCatchOccurredException(Exception exception)
        //{
        //    //_logger.Info(exception);
        //    return true;
        //}
    }
}
