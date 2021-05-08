using System;
using System.Collections.Generic;
using System.Text;

namespace ForthHomeWork
{
    class Persons
    {
        public static string GetFullName(string firstName, string lastName, string patronymic)
        {
            if (firstName != null && lastName != null && patronymic != null)
               if (firstName != "" && lastName != "" && patronymic != "")
                    return lastName + " " + firstName + " " + patronymic;

            return "Неверый ввод";
        }
    }
}
