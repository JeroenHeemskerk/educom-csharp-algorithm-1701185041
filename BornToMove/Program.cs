using System;
using System.Data;
using Microsoft.Data.SqlClient;


namespace BornToMove
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Tijd om te bewegen!");
            WriteMove("Squat", "Ga staan met gestrekte armen. Zak door de knieën tot de billen de grond bijna raken. Ga weer volledig gestrekt staan. Herhaal dit 20 keer zonder tussenpauzes.", 5);
            GetMoves();
            int choice = GetChoice("Wilt u zelf een beweging uitzoeken?", "Typ 1 voor 'Ja' en 2 voor 'Nee'.");
        }

        private static int GetChoice(string question, string option)
        {
            bool choiceMade = false;
            string choice = "";
            int convertedChoice;

            Console.WriteLine(question);
            Console.WriteLine(option);

            while (!choiceMade)
            {
                choice = Console.ReadLine();
                choiceMade = TestInput(choice);

                if (!choiceMade)
                {
                    Console.WriteLine();
                    Console.WriteLine("De opgegeven input was niet correct, probeer het opnieuw.");
                    Console.WriteLine();
                    Console.WriteLine(question);
                    Console.WriteLine(option);
                }
            }

            convertedChoice = int.Parse(choice);
            return convertedChoice;
        }

        private static void GetMoves()
        {
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
                                string name = reader["name"].ToString();
                                string description = reader["description"].ToString();
                                string sweatRate = reader["sweatRate"].ToString();
                                Console.WriteLine($"Name: {name}, Description: {description}, Sweat Rate: {sweatRate}");
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
            }
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
                            Console.WriteLine($"Nieuwe move succesvol toegevoegd. Hoeveelheid rijen toegevoegd: {rowsAffected}");
                        } else
                        {
                            Console.WriteLine("Er zijn geen rijen toegevoegd. Misschien is er iets mis gegaan");
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

        private static bool TestInput(string choice)
        {
            if ((int.TryParse(choice, out int convertedChoice)) && (convertedChoice == 1 || convertedChoice == 2)) 
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
