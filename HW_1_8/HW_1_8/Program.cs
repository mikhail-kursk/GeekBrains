using System;
using System.Configuration;

namespace _1_8_HomeWork
{
    class Program
    {

        static void Main(string[] args)
        {

            Greating();
            AskInfoAndSave();
            AskToChangeGreating();
        }

        static void Greating()
        {
            if (TryToReadSetting("HelloString", out string helloString))
                Console.WriteLine(helloString);
            else
                Console.WriteLine("Добрый день, пожалуйста представтесь");

            if (TryToReadSetting("Name", out string name))
                Console.WriteLine($"name = {name}");

            if (TryToReadSetting("Age", out string age))
                Console.WriteLine($"age = {age}");

            if (TryToReadSetting("Profession", out string profession))
                Console.WriteLine($"profession = {profession}");
        }

        static void AskInfoAndSave()
        {
            Console.WriteLine("\nУкажите своё имя");
            var name = Console.ReadLine();
            if (String.IsNullOrEmpty(name))
            {
                Console.WriteLine("Не может быть пустым, данные не будут сохранены");
                return;
            }

            Console.WriteLine("\nУкажите свой возраст");
            var age = Console.ReadLine();
            if (String.IsNullOrEmpty(age))
            {
                Console.WriteLine("Не может быть пустым, данные не будут сохранены");
                return;
            }

            Console.WriteLine("\nУкажите свой род деятельности");
            var profession = Console.ReadLine();
            if (String.IsNullOrEmpty(profession))
            {
                Console.WriteLine("Не может быть пустым, данные не будут сохранены");
                return;
            }

            AddUpdateAppSettings("Name", name);
            AddUpdateAppSettings("Age", age);
            AddUpdateAppSettings("Profession", profession);
        }

        static void AskToChangeGreating()
        {
            Console.WriteLine("\nЕсли хотите введите новую строку приветствия, для выхода оставьте строку пустой");
            var newGreating = Console.ReadLine();

            if (!String.IsNullOrEmpty(newGreating))
                AddUpdateAppSettings("HelloString", newGreating);
        }



        static bool TryToReadSetting(string key, out string result)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                result = appSettings[key] ?? "Not Found";
                if (result != "Not Found")
                    return true;
                else
                    return false;
            }
            catch (ConfigurationErrorsException)
            {
                result = null;
                return false;
            }
        }

        static void AddUpdateAppSettings(string key, string value)
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
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
    }
}
