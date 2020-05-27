using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using ShareX.HelpersLib;

namespace ShareX
{
    public class Option
    {
        internal static ApplicationConfig Settings { get; set; }

        internal static TaskSettings DefaultTaskSettings { get; set; }

        public const string Name = "ShareX";

        public static readonly string DefaultPersonalFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Name);

        public static string ToolsFolder => Path.Combine(PersonalFolder, "Tools");

        public static string DefaultFFmpegFilePath => Path.Combine(ToolsFolder, "ffmpeg.exe");

        private static string CustomPersonalPath { get; set; }

        public static string PersonalFolder
        {
            get
            {
                if (!string.IsNullOrEmpty(CustomPersonalPath))
                {
                    return Helpers.ExpandFolderVariables(CustomPersonalPath);
                }

                return DefaultPersonalFolder;
            }
        }

        public static string ScreenshotsFolder
        {
            get
            {
                string subFolderName = NameParser.Parse(NameParserType.FolderPath, Settings.SaveImageSubFolderPattern);
                string folderPath = Path.Combine(ScreenshotsParentFolder, subFolderName);
                return Helpers.GetAbsolutePath(folderPath);
            }
        }

        public static string ScreenshotsParentFolder
        {
            get
            {
                if (Settings != null && Settings.UseCustomScreenshotsPath)
                {
                    string path = Settings.CustomScreenshotsPath;
                    string path2 = Settings.CustomScreenshotsPath2;

                    if (!string.IsNullOrEmpty(path))
                    {
                        path = Helpers.ExpandFolderVariables(path);

                        if (string.IsNullOrEmpty(path2) || Directory.Exists(path))
                        {
                            return path;
                        }
                    }

                    if (!string.IsNullOrEmpty(path2))
                    {
                        path2 = Helpers.ExpandFolderVariables(path2);

                        if (Directory.Exists(path2))
                        {
                            return path2;
                        }
                    }
                }

                return Path.Combine(PersonalFolder, "Screenshots");
            }
        }

        public static bool Sandbox { get; private set; }

        public const string HistoryFilename = "History.json";

        internal static Dispatcher MainForm => Application.Current.Dispatcher;

        internal static CLIManager CLI { get; private set; }

        public static string HistoryFilePath
        {
            get
            {
                if (Sandbox) return null;

                return Path.Combine(PersonalFolder, HistoryFilename);
            }
        }

        static Option()
        {
            DefaultTaskSettings = new TaskSettings();
        }
    }
}
