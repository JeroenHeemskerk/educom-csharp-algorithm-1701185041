using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using BornToMove.DAL;

namespace BornToMove.Business
{
    public class BuMove
    {
        private MoveCrud moveCrud;
        public List<Move> moves { get; set; }

        public BuMove(MoveCrud crud)
        {
            this.moveCrud = crud;
            List<Move> moves = new List<Move>();
        }

        public string CheckUserMoveName(string newName)
        {
            string error;
            //De regex laat enkel uppercase letters, lowercase letters en spaties toe
            Regex regex = new Regex(@"[^a-zA-Z ]");

            //newName wordt eerst getrimd en krijgt een hoofdletter
            newName = newName.Trim();
            newName = char.ToUpper(newName[0]) + newName.Substring(1);

            error = TestInput(newName, "De invoer mag enkel letters bevatten.", 20, regex);

            return error;
        }

        public bool CheckUniqueUserMoveName(string newName)
        {
            foreach (Move move in this.moves)
            {
                if (move.name == newName)
                {
                    return false;
                }
            }
            return true;
        }

        public string CheckUserMoveDescription(string newDescription)
        {
            //newDescription wordt eerst getrimd en krijgt een hoofdletter
            newDescription = newDescription.Trim();
            newDescription = char.ToUpper(newDescription[0]) + newDescription.Substring(1);

            //De regex laat enkel uppercase letters, lowercase letters, punten, komma's en spaties toe
            Regex regex = new Regex(@"[^a-zA-Z0-9., ]");
            string error = TestInput(newDescription, "De invoer mag geen speciale karakters of underscores bevatten", 200, regex);

            return error;
        }

        public string CheckUserMoveSweatRate(string newSweatRate)
        {
            string error = TestInput(newSweatRate);
            return error;
        }

        public void GetMoves()
        {            
            moves = moveCrud.ReadAllMoves();         
        }

        public Move GetRandomMove()
        {
            Random random = new Random();
            int randomIndex = random.Next(0, moves.Count);
            return moves[randomIndex];
        }

        public string SanitizeInput(string input)
        {
            input = input.Trim();
            input = Regex.Replace(input, @"/W", "");
            return input;
        }

        public bool TestChoiceInput(string choice, int min, int max)
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

        public string TestInput(string name, string error, int maxChars, Regex regex)
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

        public string TestInput(string rate)
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

        public void WriteMove(string name, string description, int sweatRate)
        {
            Move newMove = new Move(null, name, description, sweatRate);
            moveCrud.CreateMove(newMove);
        }
    }
}
