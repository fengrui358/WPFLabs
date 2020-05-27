﻿#region License Information (GPL v3)

/*
    ShareX - A program that allows you to take screenshots and share any file type
    Copyright (c) 2007-2020 ShareX Team

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v3)

using ShareX.HelpersLib;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WindowState = System.Windows.WindowState;

namespace ShareX
{
    public abstract class CaptureBase
    {
        private ImageInfo _imageInfo = null;

        public bool AllowAutoHideForm { get; set; } = true;
        public bool AllowAnnotation { get; set; } = true;

        public void Capture(bool autoHideForm)
        {
            Capture(null, autoHideForm);
        }

        public void Capture(TaskSettings taskSettings = null, bool autoHideForm = false)
        {
            if (taskSettings == null) taskSettings = TaskSettings.GetDefaultTaskSettings();

            if (taskSettings.CaptureSettings.ScreenshotDelay > 0)
            {
                int delay = (int)(taskSettings.CaptureSettings.ScreenshotDelay * 1000);

                Task.Delay(delay).ContinueInCurrentContext(() =>
                {
                    CaptureInternal(taskSettings, autoHideForm);
                });
            }
            else
            {
                CaptureInternal(taskSettings, autoHideForm);
            }
        }

        /// <summary>
        /// 获取最后的捕获图片
        /// </summary>
        /// <returns></returns>
        public ImageInfo GetLastCapture()
        {
            return _imageInfo;
        }

        protected abstract ImageInfo Execute(TaskSettings taskSettings);

        private void CaptureInternal(TaskSettings taskSettings, bool autoHideForm)
        {
            if (autoHideForm && AllowAutoHideForm)
            {
                //Application.Current.MainWindow.WindowState = WindowState.Minimized;
                Application.Current.MainWindow.Hide();
                //Program.MainForm.Hide();
                Thread.Sleep(250);
            }

            try
            {
                AllowAnnotation = true;
                _imageInfo = Execute(taskSettings);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException(ex);
            }
            finally
            {
                if (autoHideForm && AllowAutoHideForm)
                {
                    //Application.Current.MainWindow.WindowState = WindowState.Normal;
                    Application.Current.MainWindow.Show();
                }

                AfterCapture(_imageInfo, taskSettings);
            }
        }

        private void AfterCapture(ImageInfo imageInfo, TaskSettings taskSettings)
        {
            if (imageInfo != null && imageInfo.Image != null)
            {
                if (taskSettings.GeneralSettings.PlaySoundAfterCapture)
                {
                    TaskHelpers.PlayCaptureSound(taskSettings);
                }

                if (taskSettings.AfterCaptureJob.HasFlag(AfterCaptureTasks.AnnotateImage) && !AllowAnnotation)
                {
                    taskSettings.AfterCaptureJob = taskSettings.AfterCaptureJob.Remove(AfterCaptureTasks.AnnotateImage);
                }

                if (taskSettings.ImageSettings.ImageEffectOnlyRegionCapture &&
                    GetType() != typeof(CaptureRegion) && GetType() != typeof(CaptureLastRegion))
                {
                    taskSettings.AfterCaptureJob = taskSettings.AfterCaptureJob.Remove(AfterCaptureTasks.AddImageEffects);
                }
            }
        }

        protected ImageInfo CreateImageInfo()
        {
            return CreateImageInfo(Rectangle.Empty, null);
        }

        protected ImageInfo CreateImageInfo(Rectangle insideRect)
        {
            return CreateImageInfo(insideRect, "explorer");
        }

        protected ImageInfo CreateImageInfo(Rectangle insideRect, string ignoreProcess)
        {
            ImageInfo imageInfo = new ImageInfo();

            IntPtr handle = NativeMethods.GetForegroundWindow();
            WindowInfo windowInfo = new WindowInfo(handle);

            if ((ignoreProcess == null || !windowInfo.ProcessName.Equals(ignoreProcess, StringComparison.InvariantCultureIgnoreCase)) &&
                (insideRect.IsEmpty || windowInfo.Rectangle.Contains(insideRect)))
            {
                imageInfo.UpdateInfo(windowInfo);
            }

            return imageInfo;
        }
    }
}