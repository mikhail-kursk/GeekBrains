using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    class DisplayForms
    {
        static bool isDefault = true;
        public static int _page = 1;
        public static int linePerPage = 20;

        static string customForm = "";

        public DisplayForms()
        {

        }

        public static void RecalculateForm()
        {

        }

        public static string GetForm()
        {
            if (isDefault)
            {
                string displayedPath = "";

                if (FileSystem._path.Length > 60)
                    displayedPath = FileSystem._path.Substring(0, 60);
                else
                    displayedPath = FileSystem._path;

                string offset = new string(' ', 81 - displayedPath.Length);
                var defaultFormUpperPart = $"" +
                                   "╔══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗" +
                                   "║ Path = " + displayedPath + offset +                                  "Date = " + DateTime.Now +                  "   ║" +
                                   "╠═════════════════════════════════════════════════════════════════════════════════════╦════════════════════════════════╣" +
                                   "║ Folder content                                                                      ║  Additional information        ║" +
                                   "║                                                                                     ║                                ║" +
                                   "║ " + GetFolderContentWithOffset(1) +       "                                         ║                                ║" +
                                   "║ " + GetFolderContentWithOffset(2) +       "                                         ║                                ║" +
                                   "║ " + GetFolderContentWithOffset(3) +       "                                         ║                                ║" +
                                   "║ " + GetFolderContentWithOffset(4) +       "                                         ║                                ║" +
                                   "║ " + GetFolderContentWithOffset(5) +       "                                         ║                                ║" +
                                   "║ " + GetFolderContentWithOffset(6) +       "                                         ║                                ║" +
                                   "║ " + GetFolderContentWithOffset(7) +       "                                         ║                                ║" +
                                   "║ " + GetFolderContentWithOffset(8) +       "                                         ║                                ║" +
                                   "║ " + GetFolderContentWithOffset(9) +       "                                         ║                                ║" +
                                   "║ " + GetFolderContentWithOffset(10) +      "                                         ║                                ║" +
                                   "║ " + GetFolderContentWithOffset(11) +      "                                         ║                                ║" +
                                   "║ " + GetFolderContentWithOffset(12) +      "                                         ║                                ║" +
                                   "║ " + GetFolderContentWithOffset(13) +      "                                         ║                                ║" +
                                   "║ " + GetFolderContentWithOffset(14) +      "                                         ║                                ║" +
                                   "║ " + GetFolderContentWithOffset(15) +      "                                         ║                                ║" +
                                   "║ " + GetFolderContentWithOffset(16) +      "                                         ║                                ║" +
                                   "║ " + GetFolderContentWithOffset(17) +      "                                         ║                                ║" +
                                   "║ " + GetFolderContentWithOffset(18) +      "                                         ║                                ║" +
                                   "║ " + GetFolderContentWithOffset(19) +      "                                         ║                                ║" +
                                   "║ " + GetFolderContentWithOffset(20) +      "                                         ║                                ║" +
                                   "║ " + GetFolderContentWithOffset(21) +      "                                         ║                                ║" +
                                   "║ " + GetFolderContentWithOffset(22) +      "                                         ║                                ║" +
                                   "╚═════════════════════════════════════════════════════════════════════════════════════╩════════════════════════════════╝";
                return defaultFormUpperPart;
            }
            else
                return customForm;
        }

        public static string GetFolderContentWithOffset(int line)
        {
            var result = "";

            if (line + ((_page - 1) * linePerPage) <= FileSystem.directories.Count)
                result = FileSystem.directories[line + ((_page - 1) * linePerPage) - 1];

            if ((line + ((_page - 1) * linePerPage) >= FileSystem.directories.Count + 1) && (line + ((_page - 1) * linePerPage) <= FileSystem.directories.Count + FileSystem.files.Count))
                result = FileSystem.files[line + ((_page - 1) * linePerPage) - FileSystem.directories.Count - 1];

            if (!String.IsNullOrEmpty(result))
                result = result.Remove(0, FileSystem._path.Length);

            if (result.Length < 43)
                result = result + new string(' ', 43 - result.Length);

            if (result.Length > 43)
                result = result.Substring(0, 43);

            return result;
        }
    }
}
