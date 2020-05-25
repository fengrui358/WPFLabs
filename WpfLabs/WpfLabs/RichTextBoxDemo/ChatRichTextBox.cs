using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace WpfLabs.RichTextBoxDemo
{
    public class ChatRichTextBox : RichTextBox
    {
        static ChatRichTextBox()
        {
            CommandManager.RegisterClassCommandBinding(typeof(ChatRichTextBox), new CommandBinding(ApplicationCommands.Paste, PasteHandle));
            CommandManager.RegisterClassCommandBinding(typeof(ChatRichTextBox), new CommandBinding(ApplicationCommands.Cut, CutHandle));
        }

        #region Event

        #region SendFile

        //public event RoutedEventHandler SendFile
        //{
        //    add { AddHandler(SendFileEvent, value); }
        //    remove { RemoveHandler(SendFileEvent, value); }
        //}

        //public static readonly RoutedEvent SendFileEvent = FileDropControl.SendFileEvent.AddOwner(typeof(ChatRichTextBox));

        //private void RaiseSendFile(string file)
        //{
        //    var args = new SendFileEventArgs(SendFileEvent, this, file);
        //    RaiseEvent(args);
        //}

        private void RaiseSendFile(string[] files)
        {
        }

        //private void RaiseSendFile(List<string> fileList)
        //{
        //    var args = new SendFileEventArgs(SendFileEvent, this, fileList);
        //    RaiseEvent(args);
        //}

        #endregion

        #endregion

        /// <summary>
        /// 粘贴剪贴板中对象。
        /// </summary>
        public new void Paste()
        {
            OnPaste();
        }

        private static void CutHandle(object sender, ExecutedRoutedEventArgs e)
        {
            var ctrl = (ChatRichTextBox)sender;
            ctrl.OnCut();
        }

        private static void PasteHandle(object sender, ExecutedRoutedEventArgs e)
        {
            var ctrl = (ChatRichTextBox)sender;
            ctrl.OnPaste();
        }

        private void OnCut()
        {
            Cut();
        }

        private void OnPaste()
        {
            if (Clipboard.ContainsImage())
            {
                var imgFile = ProcessClipboardImage();
                if (!string.IsNullOrEmpty(imgFile))
                {
                    var image = new Rectangle
                    {
                        Width = 100,
                        Height = 50,
                        Fill = Brushes.Red
                    };
                    var uiContainer = new InlineUIContainer(image, Selection.Start);
                    var para = Document.Blocks.LastBlock as Paragraph ?? new Paragraph();
                    para.Inlines.Add(uiContainer);
                    Document.Blocks.Add(para);
                    return;
                }
            }
            else if (Clipboard.ContainsFileDropList())
            {
                //var files = Clipboard.GetFileDropList();
                //RaiseSendFile(files.Cast<string>().ToList());
            }

            base.Paste();
        }

        /// <summary>
        /// 处理剪贴板中的图像。
        /// </summary>
        private static string ProcessClipboardImage()
        {
            if (!Clipboard.ContainsImage())
                return null;

            try
            {
                var bitmap = Clipboard.GetImage();
                if (bitmap == null)
                    return null;

                var tempFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{Guid.NewGuid()}.png");
                using (FileStream fs = new FileStream(tempFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    BitmapEncoder encoder = new BmpBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmap));
                    encoder.Save(fs);
                }

                return tempFile;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
