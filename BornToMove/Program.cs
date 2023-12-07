using System;
using System.Data;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;


namespace BornToMove
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("BORNTOMOVE");
            Console.WriteLine("Tijd om te bewegen!");
            List<Move> moves = GetMoves();
            int choice = GetChoice("Wilt u zelf een beweging uitzoeken?", "Typ 1 voor 'Ja' en 2 voor 'Nee'.", 1, 2);

            if (choice == 1)
            {
                DisplayMoves(moves);
                choice = GetChoice("Welke move wilt u uitvoeren?", "Geef het id op van de move die u wilt uitvoeren, of een '0' om een eigen move in te voeren.", 0, moves.Count);
                if (choice == 0)
                {
                    string newName = CheckUserMoveName(moves);
                    string newDescription = CheckUserMoveDescription();
                    int newSweatRate = CheckUserMoveSweatRate();
                    WriteMove(newName, newDescription, newSweatRate);
                    Console.WriteLine("Dankuwel voor uw nieuwe 'move'. Uw 'move' is nu te vinden in deze applicatie!");
                } else
                {
                    choice -= 1;
                    moves[choice].DisplayMove(false);
                    GetRatings();
                }
            } else if (choice == 2) 
            {
                DisplayRandomMove(moves);
                GetRatings();
            }
        }

        private static int GetChoice(string question, string option, int min, int max)
        {
            bool choiceMade = false;
            string choice = "";
            int convertedChoice;

            Console.WriteLine(question);
            Console.WriteLine(option);

            while (!choiceMade)
            {
                choice = Console.ReadLine();
                choice = SanitizeInput(choice);
                choiceMade = TestChoiceInput(choice, min, max);

                if (!choiceMade)
                {
                    Console.WriteLine();
                    Console.WriteLine("Ongeldige input, probeer het opnieuw.");
                    Console.WriteLine();
                    Console.WriteLine(question);
                    Console.WriteLine(option);
                }
            }

            Console.WriteLine();
            convertedChoice = int.Parse(choice);
            return convertedChoice;
        }

        private static List<Move> GetMoves()
        {
            List<Move> moves = new List<Move>();
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = BornToMove;Integrated Security=True;";
            string sqlQuery = "SELECT *" +
                              "FROM dbo.move;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {   
                                int id = reader.GetInt32("id");
                                string name = reader["name"].ToString();
                                string description = reader["description"].ToString();
                                int sweatRate = reader.GetInt32("sweatRate");
                                moves.Add(new Move(id, name, description, sweatRate));
                            }
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine($"Exception: {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception: {e.Message}");
                }

                return moves;
            }
        }

        private static void DisplayMoves(List<Move> moves)
        {
            int i = 1;
            foreach (Move move in moves)
            {
                Console.WriteLine("Id: " + i);
                move.DisplayMove(true);
                i++;
            }
        }

        private static void DisplayRandomMove(List<Move> moves)
        {
            Random random = new Random();
            int randomIndex = random.Next(0, moves.Count);
            Move randomMove = moves[randomIndex];
            Console.WriteLine();
            Console.WriteLine("De volgende move hebben wij voor u geselecteerd:");
            Console.WriteLine();
            randomMove.DisplayMove(false);
            Console.WriteLine("Veel plezier!");
            Console.WriteLine();
        }

        private static string CheckUserMoveDescription()
        {
            bool correctInput = false;
            string newDescription = "";
            string error;
            //De regex laat enkel uppercase letters, lowercase letters, punten, komma's en spaties toe
            Regex regex = new Regex(@"[^a-zA-Z0-9., ]");

            while (!correctInput)
            {
                Console.WriteLine("Geef een beschrijving van uw 'move'.");
                Console.WriteLine("De beschrijving mag maximaal 200 karakters lang zijn en mag geen speciale karakters of underscores bevatten");

                //newDescription wordt eerst getrimd en krijgt een hoofdletter
                newDescription = Console.ReadLine();
                newDescription = newDescription.Trim();

                newDescription = char.ToUpper(newDescription[0]) + newDescription.Substring(1);
                error = TestInput(newDescription, "De invoer mag geen speciale karakters of underscores bevatten", 200, regex);
                if (error != "")
                {
                    Console.WriteLine(error + " Probeer het opnieuw.");
                    Console.WriteLine();
                } else
                {
                    correctInput = true;
                }
            }
            Console.WriteLine("De beschrijving van uw 'move' is: ");
            Console.WriteLine(newDescription);
            Console.WriteLine();
            return newDescription;
        }

        private static string CheckUserMoveName(List<Move> moves)
        {
            bool newInput;
            string newName = "";
            string error;
            //De regex laat enkel uppercase letters, lowercase letters en spaties toe
            Regex regex = new Regex(@"[^a-zA-Z ]");

            do
            {
                bool correctInput = false;
                while (!correctInput)
                {
                    Console.WriteLine("U heeft opgegeven dat u graag een nieuwe 'move' wilt toevoegen. Wat is de naam van uw 'move'?");
                    Console.WriteLine("De naam mag twintig letters lang zijn en mag geen speciale tekens of nummers bevatten.");
                    newName = Console.ReadLine();

                    //newName wordt eerst getrimd en krijgt een hoofdletter
                    newName = newName.Trim();
                    newName = char.ToUpper(newName[0]) + newName.Substring(1);

                    error = TestInput(newName, "De invoer mag enkel letters bevatten.", 20, regex);
                    if (error != "")
                    {
                        Console.WriteLine(error + " Probeer het opnieuw.");
                        Console.WriteLine();
                    } else
                    {
                        correctInput = true;
                    }
                }

                newInput = true;
                foreach (Move move in moves)
                {
                    if (move.name == newName)
                    {
                        newInput = false;
                        Console.WriteLine("Deze naam is helaas al in gebruik. Probeer het Opnieuw.");
                        Console.WriteLine();
                        break;
                    }
                }
            } while (!newInput);
            Console.WriteLine("De naam van uw 'move' is: " + newName);
            Console.WriteLine();
            return newName;
        }

        private static int CheckUserMoveSweatRate()
        {
            bool correctInput = false;
            string newSweatRate = "";
            string error;

            while (!correctInput)
            {
                Console.WriteLine("Geef een 'Sweatrate' voor uw 'move'. Dit mag een geheel getal zijn van 1 tot en met 5");
                newSweatRate = Console.ReadLine();
                error = TestInput(newSweatRate);
                if (error != "")
                {
                    Console.WriteLine(error + " Probeer het opnieuw.");
                    Console.WriteLine();
                }
                else
                {
                    correctInput = true;
                }
            }
            int sweatRate = int.Parse(newSweatRate);
            Console.WriteLine("De sweatrate van uw 'move' is: " + sweatRate);
            Console.WriteLine();
            return sweatRate;
        }

        private static void WriteMove(string name, string description, int sweatRate)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = BornToMove;Integrated Security=True;";
            string sqlQuery = $"INSERT INTO dbo.move (name, description, sweatRate) VALUES ('{name}', '{description}', {sweatRate})";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sqlQuery,connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery(); //command.ExecuteNonQuery voert daadwerkelijk de query uit. ExecuteNonQuery is voor non select queries.

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Nieuwe move succesvol toegevoegd.");
                        } else
                        {
                            Console.WriteLine("Geen move toegevoegd. Misschien is er iets mis gegaan");
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine($"Exception: {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception: {e.Message}");
                }
            }

        }

        private static void GetRatings()
        {
            int rating, intensityRating;
            rating = GetChoice("Hoe vond u deze oefening?", "Vul een rating in van '1' tot en met '5'", 1, 5);
            intensityRating = GetChoice("Hoe intens was deze oefening?", "Vul een rating in van '1' tot en met '5'", 1, 5);
            Console.WriteLine("U gaf deze oefening een rating van: " + rating + " en een intensiteitsrating van: " + intensityRating + ".");
        }

        private static string SanitizeInput(string input)
        {
            input = input.Trim();
            input = Regex.Replace(input, @"/W", "");
            return input;
        }

        private static bool TestChoiceInput(string choice, int min, int max)
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

        private static string TestInput(string name, string error, int maxChars, Regex regex)
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

        private static string TestInput(string rate)
        {
            if (string.IsNullOrEmpty(rate))
            {
                return "U heeft niets ingevoegd.";
            }
            if (!int.TryParse(rate, out int convertedRate) || convertedRate < 1 || convertedRate > 5)
            {
                return "U kunt enkel een getal van 1-5 als rating opgeven.";
            }
            return "";
        }
    }
}
