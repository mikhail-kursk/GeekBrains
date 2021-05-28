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
                                   "║ " + GetFolderContentWithOffset(1) +       "                                         ║  Attributes:                   ║" +
                                   "║ " + GetFolderContentWithOffset(2) +       "                                         ║  "+GetAttrWithOffset("atr")+"  ║" +
                                   "║ " + GetFolderContentWithOffset(3) +       "                                         ║  Extension:                    ║" +
                                   "║ " + GetFolderContentWithOffset(4) +       "                                         ║  "+GetAttrWithOffset("ext")+"  ║" +
                                   "║ " + GetFolderContentWithOffset(5) +       "                                         ║  Created:                      ║" +
                                   "║ " + GetFolderContentWithOffset(6) +       "                                         ║  "+GetAttrWithOffset("cre")+"  ║" +
                                   "║ " + GetFolderContentWithOffset(7) +       "                                         ║  LastModified:                 ║" +
                                   "║ " + GetFolderContentWithOffset(8) +       "                                         ║  "+GetAttrWithOffset("lmd")+"  ║" +
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
            var tempResult = "";
            var isActive = false;

            if (line + ((_page - 1) * linePerPage) <= FileSystem.directories.Count)
                tempResult = FileSystem.directories[line + ((_page - 1) * linePerPage) - 1];

            if ((line + ((_page - 1) * linePerPage) >= FileSystem.directories.Count + 1) && (line + ((_page - 1) * linePerPage) <= FileSystem.directories.Count + FileSystem.files.Count))
                tempResult = FileSystem.files[line + ((_page - 1) * linePerPage) - FileSystem.directories.Count - 1];

            if (tempResult == FileSystem._currentFileOrDirectory)
                isActive = true;

            if (!String.IsNullOrEmpty(result))
                tempResult = result.Remove(0, FileSystem._path.Length);

            if (tempResult.Length < 42)
                tempResult = tempResult + new string(' ', 42 - tempResult.Length);

            if (tempResult.Length > 42)
                tempResult = tempResult.Substring(0, 42);

            if (isActive)
                result = ">" + tempResult;
            else
                result = " " + tempResult;

            return result;
        }

        public static string GetAttrWithOffset(string attributeName)
        {
            var temp = "";

            if (String.IsNullOrEmpty(FileSystem._currentFileOrDirectory))
                return new string(' ', 28);

            switch (attributeName)
            {
                case "atr":
                    temp = FileSystem._fileAttributes.Attributes.ToString();
                    break;
                case "ext":
                    temp = FileSystem._fileAttributes.Extension.ToString();
                    break;
                case "cre":
                    temp = FileSystem._fileAttributes.CreationTime.ToString();
                    break;
                case "lmd":
                    temp = FileSystem._fileAttributes.LastWriteTime.ToString();
                    break;
            }

            if (temp.Length < 28)
                temp = temp + new string(' ', 28 - temp.Length);

            if (temp.Length > 28)
                temp = temp.Substring(0, 28);

            return temp;
        }
    }
}
