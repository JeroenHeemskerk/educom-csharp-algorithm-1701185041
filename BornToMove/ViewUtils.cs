﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BornToMove
{
    internal class ViewUtils
    {
        public static string CheckUserMoveName(string newName)
        {
            string error;

            //newName wordt eerst getrimd en krijgt een hoofdletter
            newName = newName.Trim();
            newName = char.ToUpper(newName[0]) + newName.Substring(1);

            //De regex laat enkel uppercase letters, lowercase letters en spaties toe
            Regex regex = new Regex(@"[^a-zA-Z ]");

            error = TestInput(newName, "De invoer mag enkel letters bevatten.", 20, regex);

            return error;
        }
        public static string CheckUserMoveDescription(string newDescription)
        {
            //newDescription wordt eerst getrimd en krijgt een hoofdletter
            newDescription = newDescription.Trim();
            newDescription = char.ToUpper(newDescription[0]) + newDescription.Substring(1);

            //De regex laat enkel uppercase letters, lowercase letters, punten, komma's en spaties toe
            Regex regex = new Regex(@"[^a-zA-Z0-9., ]");

            string error = TestInput(newDescription, "De invoer mag geen speciale karakters of underscores bevatten", 200, regex);

            return error;
        }

        public static string CheckUserMoveSweatRate(string newSweatRate)
        {
            string error = TestInputRate(newSweatRate);
            return error;
        }

        public static string CheckMoveId(string userIdInput, int max)
        {
            if (string.IsNullOrEmpty(userIdInput))
            {
                return "U heeft niets ingevoerd.";
            }

            int id;
            if (!int.TryParse(userIdInput, out id))
            {
                return "U kunt enkel hele getallen opgeven als id.";
            }

            if (id >= 1 && id <= max)
            {
                return "";
            }
            else
            {
                return "Het opgegeven id kan niet worden teruggevonden.";
            }
        }

        public static string SanitizeInput(string input)
        {
            input = input.Trim();
            input = Regex.Replace(input, @"/W", "");
            return input;
        }

        public static bool TestChoiceInput(string choice, int min, int max)
        {
            if ((int.TryParse(choice, out int convertedChoice)) && (convertedChoice >= min && convertedChoice <= max))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string TestInput(string name, string error, int maxChars, Regex regex)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "U heeft niets ingevoerd.";
            }
            if (regex.IsMatch(name))
            {
                return error;
            }
            if (name.Length > maxChars)
            {
                return "De opgegeven invoer is te lang.";
            }
            return "";
        }

        public static string TestInputRate(string rate)
        {
            if (string.IsNullOrEmpty(rate))
            {
                return "U heeft niets ingevoerd.";
            }
            if (!int.TryParse(rate, out int convertedRate) || convertedRate < 1 || convertedRate > 5)
            {
                return "U kunt enkel een getal van 1-5 als rating opgeven.";
            }
            return "";
        }
    }
}
