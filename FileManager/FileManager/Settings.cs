using System;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    static class SettingIO
    {

        public static bool TryToReadSetting(string key, out string value)
        {
            value = null;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                value = appSettings[key];
                return !string.IsNullOrWhiteSpace(value);
            }
            catch (Exception e)
            {
                File.AppendAllText(Environment.CurrentDirectory + "\\errors\\app.log", e.ToString() + "\n\n");
                return false;
            }

        }

        public static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (Exception e)
            {
                File.AppendAllText(Environment.CurrentDirectory + "\\errors\\app.log", e.ToString() + "\n\n");
            }
        }
    }

}
