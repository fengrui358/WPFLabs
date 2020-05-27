﻿#region License Information (GPL v3)

/*
    ShareX - A Option that allows you to take screenshots and share any file type
    Copyright (c) 2007-2020 ShareX Team

    This Option is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This Option is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this Option; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v3)

using Newtonsoft.Json;
using ShareX.HelpersLib;
using ShareX.ScreenCaptureLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;

namespace ShareX
{
    public class TaskSettings
    {
        [JsonIgnore]
        public TaskSettings TaskSettingsReference { get; private set; }

        public string Description = "";

        public HotkeyType Job = HotkeyType.None;

        public bool UseDefaultAfterCaptureJob = true;
        public AfterCaptureTasks AfterCaptureJob = AfterCaptureTasks.CopyImageToClipboard | AfterCaptureTasks.SaveImageToFile;

        public bool UseDefaultAfterUploadJob = true;
        public AfterUploadTasks AfterUploadJob = AfterUploadTasks.CopyURLToClipboard;

        public bool UseDefaultDestinations = true;

        public bool OverrideFTP = false;
        public int FTPIndex = 0;

        public bool OverrideCustomUploader = false;
        public int CustomUploaderIndex = 0;

        public bool OverrideScreenshotsFolder = false;
        public string ScreenshotsFolder = "";

        public string GetScreenshotsFolder()
        {
            if (OverrideScreenshotsFolder && !string.IsNullOrEmpty(ScreenshotsFolder))
            {
                string screenshotsFolderPath = NameParser.Parse(NameParserType.FolderPath, ScreenshotsFolder);
                return Helpers.GetAbsolutePath(screenshotsFolderPath);
            }

            return Option.ScreenshotsFolder;
        }

        public bool UseDefaultGeneralSettings = true;
        public TaskSettingsGeneral GeneralSettings = new TaskSettingsGeneral();

        public bool UseDefaultImageSettings = true;
        public TaskSettingsImage ImageSettings = new TaskSettingsImage();

        [JsonIgnore]
        public TaskSettingsImage ImageSettingsReference
        {
            get
            {
                if (UseDefaultImageSettings)
                {
                    return Option.DefaultTaskSettings.ImageSettings;
                }

                return TaskSettingsReference.ImageSettings;
            }
        }

        public bool UseDefaultCaptureSettings = true;
        public TaskSettingsCapture CaptureSettings = new TaskSettingsCapture();

        [JsonIgnore]
        public TaskSettingsCapture CaptureSettingsReference
        {
            get
            {
                if (UseDefaultCaptureSettings)
                {
                    return Option.DefaultTaskSettings.CaptureSettings;
                }

                return TaskSettingsReference.CaptureSettings;
            }
        }

        public bool UseDefaultUploadSettings = true;
        public TaskSettingsUpload UploadSettings = new TaskSettingsUpload();

        public bool UseDefaultActions = true;
        public List<ExternalProgram> ExternalPrograms = new List<ExternalProgram>();

        public bool UseDefaultToolsSettings = true;
        public TaskSettingsTools ToolsSettings = new TaskSettingsTools();

        [JsonIgnore]
        public TaskSettingsTools ToolsSettingsReference
        {
            get
            {
                if (UseDefaultToolsSettings)
                {
                    return Option.DefaultTaskSettings.ToolsSettings;
                }

                return TaskSettingsReference.ToolsSettings;
            }
        }

        public bool UseDefaultAdvancedSettings = true;
        public TaskSettingsAdvanced AdvancedSettings = new TaskSettingsAdvanced();

        public bool WatchFolderEnabled = false;

        public override string ToString()
        {
            return !string.IsNullOrEmpty(Description) ? Description : Job.GetLocalizedDescription();
        }

        public bool IsUsingDefaultSettings
        {
            get
            {
                return UseDefaultAfterCaptureJob && UseDefaultAfterUploadJob && UseDefaultDestinations && !OverrideFTP && !OverrideCustomUploader && UseDefaultGeneralSettings &&
                    UseDefaultImageSettings && UseDefaultCaptureSettings && UseDefaultUploadSettings && UseDefaultActions && UseDefaultToolsSettings &&
                    UseDefaultAdvancedSettings && !WatchFolderEnabled;
            }
        }

        public static TaskSettings GetDefaultTaskSettings()
        {
            TaskSettings taskSettings = new TaskSettings();
            taskSettings.SetDefaultSettings();
            taskSettings.TaskSettingsReference = Option.DefaultTaskSettings;
            return taskSettings;
        }

        public static TaskSettings GetSafeTaskSettings(TaskSettings taskSettings)
        {
            TaskSettings safeTaskSettings;

            if (taskSettings.IsUsingDefaultSettings && Option.DefaultTaskSettings != null)
            {
                safeTaskSettings = Option.DefaultTaskSettings.Copy();
                safeTaskSettings.Description = taskSettings.Description;
                safeTaskSettings.Job = taskSettings.Job;
            }
            else
            {
                safeTaskSettings = taskSettings.Copy();
                safeTaskSettings.SetDefaultSettings();
            }

            safeTaskSettings.TaskSettingsReference = taskSettings;
            return safeTaskSettings;
        }

        private void SetDefaultSettings()
        {
            if (Option.DefaultTaskSettings != null)
            {
                TaskSettings defaultTaskSettings = Option.DefaultTaskSettings.Copy();

                if (UseDefaultAfterCaptureJob)
                {
                    AfterCaptureJob = defaultTaskSettings.AfterCaptureJob;
                }

                if (UseDefaultAfterUploadJob)
                {
                    AfterUploadJob = defaultTaskSettings.AfterUploadJob;
                }

                if (UseDefaultGeneralSettings)
                {
                    GeneralSettings = defaultTaskSettings.GeneralSettings;
                }

                if (UseDefaultImageSettings)
                {
                    ImageSettings = defaultTaskSettings.ImageSettings;
                }

                if (UseDefaultCaptureSettings)
                {
                    CaptureSettings = defaultTaskSettings.CaptureSettings;
                }

                if (UseDefaultUploadSettings)
                {
                    UploadSettings = defaultTaskSettings.UploadSettings;
                }

                if (UseDefaultActions)
                {
                    ExternalPrograms = defaultTaskSettings.ExternalPrograms;
                }

                if (UseDefaultToolsSettings)
                {
                    ToolsSettings = defaultTaskSettings.ToolsSettings;
                }

                if (UseDefaultAdvancedSettings)
                {
                    AdvancedSettings = defaultTaskSettings.AdvancedSettings;
                }
            }
        }
    }

    public class TaskSettingsGeneral
    {
        public bool PlaySoundAfterCapture = true;
        public bool ShowAfterCaptureTasksForm = false;
        public bool ShowBeforeUploadForm = false;
        public bool PlaySoundAfterUpload = true;
        public PopUpNotificationType PopUpNotification = PopUpNotificationType.ToastNotification;
        public bool ShowAfterUploadForm = false;
    }

    public class TaskSettingsImage
    {
        #region Image / General

        public EImageFormat ImageFormat = EImageFormat.PNG;
        public PNGBitDepth ImagePNGBitDepth = PNGBitDepth.Default;
        public int ImageJPEGQuality = 90;
        public GIFQuality ImageGIFQuality = GIFQuality.Default;
        public bool ImageAutoUseJPEG = true;
        public int ImageAutoUseJPEGSize = 2048;
        public FileExistAction FileExistAction = FileExistAction.Ask;

        #endregion Image / General

        #region Image / Effects

        public int SelectedImageEffectPreset = 0;

        public bool ShowImageEffectsWindowAfterCapture = false;
        public bool ImageEffectOnlyRegionCapture = false;

        #endregion Image / Effects

        #region Image / Thumbnail

        public int ThumbnailWidth = 200;
        public int ThumbnailHeight = 0;
        public string ThumbnailName = "-thumbnail";
        public bool ThumbnailCheckSize = false;

        #endregion Image / Thumbnail
    }

    public class TaskSettingsCapture
    {
        #region Capture / General

        public bool ShowCursor = true;
        public decimal ScreenshotDelay = 0;
        public bool CaptureTransparent = false;
        public bool CaptureShadow = true;
        public int CaptureShadowOffset = 20;
        public bool CaptureClientArea = false;
        public bool CaptureAutoHideTaskbar = false;
        public Rectangle CaptureCustomRegion = new Rectangle(0, 0, 0, 0);

        #endregion Capture / General

        #region Capture / Region capture

        public RegionCaptureOptions SurfaceOptions = new RegionCaptureOptions();

        #endregion Capture / Region capture

        #region Capture / Screen recorder

        public FFmpegOptions FFmpegOptions = new FFmpegOptions(Option.DefaultFFmpegFilePath);
        public int ScreenRecordFPS = 30;
        public int GIFFPS = 15;
        public bool ScreenRecordShowCursor = true;
        public bool ScreenRecordAutoStart = true;
        public float ScreenRecordStartDelay = 0f;
        public bool ScreenRecordFixedDuration = false;
        public float ScreenRecordDuration = 3f;
        public bool ScreenRecordTwoPassEncoding = false;
        public bool ScreenRecordAskConfirmationOnAbort = false;
        public bool ScreenRecordTransparentRegion = false;

        #endregion Capture / Screen recorder

        #region Capture / Scrolling capture

        public ScrollingCaptureOptions ScrollingCaptureOptions = new ScrollingCaptureOptions();

        #endregion Capture / Scrolling capture
    }

    public class TaskSettingsUpload
    {
        #region Upload / File naming

        public bool UseCustomTimeZone = false;
        public TimeZoneInfo CustomTimeZone = TimeZoneInfo.Utc;
        public string NameFormatPattern = "%ra{10}";
        public string NameFormatPatternActiveWindow = "%pn_%ra{10}";
        public bool RegionCaptureUseWindowPattern = true;
        public bool FileUploadUseNamePattern = false;
        public bool FileUploadReplaceProblematicCharacters = false;

        #endregion Upload / File naming

        #region Upload / Clipboard upload

        public bool ClipboardUploadURLContents = false;
        public bool ClipboardUploadShortenURL = false;
        public bool ClipboardUploadShareURL = false;
        public bool ClipboardUploadAutoIndexFolder = false;

        #endregion Upload / Clipboard upload
    }

    public class TaskSettingsTools
    {
        public string ScreenColorPickerFormat = "$hex";
    }

    public class TaskSettingsAdvanced
    {
        [Category("General"), DefaultValue(false), Description("Allow after capture tasks for image files by loading them as bitmap when files are handled during file upload, clipboard file upload, drag && drop file upload, watch folder and other image file tasks.")]
        public bool ProcessImagesDuringFileUpload { get; set; }

        [Category("General"), DefaultValue(false), Description("Use after capture tasks for clipboard image upload.")]
        public bool ProcessImagesDuringClipboardUpload { get; set; }

        [Category("General"), DefaultValue(true), Description("Allows file related after capture tasks (\"Perform actions\", \"Copy file to clipboard\" etc.) to be used when doing file upload.")]
        public bool UseAfterCaptureTasksDuringFileUpload { get; set; }

        [Category("General"), DefaultValue(true), Description("Save text as file for tasks such as clipboard text upload, drag and drop text upload, index folder etc.")]
        public bool TextTaskSaveAsFile { get; set; }

        [Category("General"), DefaultValue(false), Description("If task contains upload job then this setting will clear clipboard when task start.")]
        public bool AutoClearClipboard { get; set; }

        [Category("Sound"), DefaultValue(false), Description("Enable/disable custom capture sound.")]
        public bool UseCustomCaptureSound { get; set; }

        [Category("Sound"), DefaultValue(""), Description("Capture sound file path."),
        Editor(typeof(WavFileNameEditor), typeof(UITypeEditor))]
        public string CustomCaptureSoundPath { get; set; }

        [Category("Sound"), DefaultValue(false), Description("Enable/disable custom task complete sound.")]
        public bool UseCustomTaskCompletedSound { get; set; }

        [Category("Sound"), DefaultValue(""), Description("Task complete sound file path."),
        Editor(typeof(WavFileNameEditor), typeof(UITypeEditor))]
        public string CustomTaskCompletedSoundPath { get; set; }

        [Category("Sound"), DefaultValue(false), Description("Enable/disable custom error sound.")]
        public bool UseCustomErrorSound { get; set; }

        [Category("Sound"), DefaultValue(""), Description("Error sound file path."),
        Editor(typeof(WavFileNameEditor), typeof(UITypeEditor))]
        public string CustomErrorSoundPath { get; set; }

        [Category("Capture"), DefaultValue(false), Description("Disable annotation support in region capture.")]
        public bool RegionCaptureDisableAnnotation { get; set; }

        [Category("Upload"), Description("Files with these file extensions will be uploaded using image uploader."),
        Editor("System.Windows.Forms.Design.StringCollectionEditor,System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public List<string> ImageExtensions { get; set; }

        [Category("Upload"), DefaultValue(false), Description("Copy URL before start upload. Only works for FTP, FTPS, SFTP, Amazon S3, Google Cloud Storage and Azure Storage.")]
        public bool EarlyCopyURL { get; set; }

        [Category("Upload"), Description("Files with these file extensions will be uploaded using text uploader."),
        Editor("System.Windows.Forms.Design.StringCollectionEditor,System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public List<string> TextExtensions { get; set; }

        [Category("After upload"), DefaultValue(false), Description("If result URL starts with \"http://\" then replace it with \"https://\".")]
        public bool ResultForceHTTPS { get; set; }

        [Category("After upload"), DefaultValue("$result"),
        Description("Clipboard content format after uploading. Supported variables: $result, $url, $shorturl, $thumbnailurl, $deletionurl, $filepath, $filename, $filenamenoext, $folderpath, $foldername, $uploadtime and other variables such as %y-%mo-%d etc.")]
        public string ClipboardContentFormat { get; set; }

        [Category("After upload"), DefaultValue("$result"), Description("Balloon tip content format after uploading. Supported variables: $result, $url, $shorturl, $thumbnailurl, $deletionurl, $filepath, $filename, $filenamenoext, $folderpath, $foldername, $uploadtime and other variables such as %y-%mo-%d etc.")]
        public string BalloonTipContentFormat { get; set; }

        [Category("After upload"), DefaultValue("$result"), Description("After upload task \"Open URL\" format. Supported variables: $result, $url, $shorturl, $thumbnailurl, $deletionurl, $filepath, $filename, $filenamenoext, $folderpath, $foldername, $uploadtime and other variables such as %y-%mo-%d etc.")]
        public string OpenURLFormat { get; set; }

        [Category("After upload"), DefaultValue(0), Description("Automatically shorten URL if the URL is longer than the specified number of characters. 0 means automatic URL shortening is not active.")]
        public int AutoShortenURLLength { get; set; }

        [Category("Notifications"), DefaultValue(false), Description("Disable notifications.")]
        public bool DisableNotifications { get; set; }

        [Category("Notifications"), DefaultValue(false), Description("If active window is fullscreen then toast window or balloon tip won't be shown.")]
        public bool DisableNotificationsOnFullscreen { get; set; }

        private float toastWindowDuration;

        [Category("Notifications"), DefaultValue(3f), Description("Specify how long should toast notification window will stay on screen (in seconds).")]
        public float ToastWindowDuration
        {
            get
            {
                return toastWindowDuration;
            }
            set
            {
                toastWindowDuration = value.Clamp(0, 30);
            }
        }

        private float toastWindowFadeDuration;

        [Category("Notifications"), DefaultValue(1f), Description("After toast window duration end, toast window will start fading, specify duration of this fade animation (in seconds).")]
        public float ToastWindowFadeDuration
        {
            get
            {
                return toastWindowFadeDuration;
            }
            set
            {
                toastWindowFadeDuration = value.Clamp(0, 30);
            }
        }

        [Category("Notifications"), DefaultValue(ContentAlignment.BottomRight), Description("Specify where should toast notification window appear on the screen.")]
        public ContentAlignment ToastWindowPlacement { get; set; }

        [Category("Notifications"), DefaultValue(ToastClickAction.OpenUrl), Description("Specify action after toast notification window is left clicked."), TypeConverter(typeof(EnumDescriptionConverter))]
        public ToastClickAction ToastWindowClickAction { get; set; }

        [Category("Notifications"), DefaultValue(ToastClickAction.CloseNotification), Description("Specify action after toast notification window is right clicked."), TypeConverter(typeof(EnumDescriptionConverter))]
        public ToastClickAction ToastWindowRightClickAction { get; set; }

        [Category("Notifications"), DefaultValue(ToastClickAction.AnnotateImage), Description("Specify action after toast notification window is middle clicked."), TypeConverter(typeof(EnumDescriptionConverter))]
        public ToastClickAction ToastWindowMiddleClickAction { get; set; }

        private Size toastWindowSize;

        [Category("Notifications"), DefaultValue(typeof(Size), "400, 300"), Description("Maximum toast notification window size.")]
        public Size ToastWindowSize
        {
            get
            {
                return toastWindowSize;
            }
            set
            {
                toastWindowSize = new Size(Math.Max(value.Width, 100), Math.Max(value.Height, 100));
            }
        }

        [Category("After upload"), DefaultValue(false), Description("After upload form will be automatically closed after 60 seconds.")]
        public bool AutoCloseAfterUploadForm { get; set; }

        [Category("Upload text"), DefaultValue("txt"), Description("File extension when saving text to the local hard disk.")]
        public string TextFileExtension { get; set; }

        [Category("Upload text"), DefaultValue("text"), Description("Text format e.g. csharp, cpp, etc.")]
        public string TextFormat { get; set; }

        //[Category("Upload text"), DefaultValue(""), Description("Custom text input. Use %input for text input. Example you can create web page with your text in it."),
        //Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string TextCustom { get; set; }

        [Category("Upload text"), DefaultValue(true), Description("HTML encode custom text input.")]
        public bool TextCustomEncodeInput { get; set; }

        [Category("Name pattern"), DefaultValue(100), Description("Maximum name pattern length for file name.")]
        public int NamePatternMaxLength { get; set; }

        [Category("Name pattern"), DefaultValue(50), Description("Maximum name pattern title (%t) length for file name.")]
        public int NamePatternMaxTitleLength { get; set; }

        // TEMP: For backward compatibility
        public string CapturePath;

        public TaskSettingsAdvanced()
        {
            this.ApplyDefaultPropertyValues();
            ImageExtensions = Helpers.ImageFileExtensions.ToList();
            TextExtensions = Helpers.TextFileExtensions.ToList();
        }
    }
}