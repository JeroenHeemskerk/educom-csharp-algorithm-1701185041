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
            int choice = GetChoice("Wilt u zelf een beweging uitzoeken?", "Typ 1 voor ja en 2 voor nee.");
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
            string connectionStr = "Data Source=(local;Initial Catalog=move;" + "Integrated Security=SSPI";
            string sql = "SELECT *" +
                         "FROM dbo.move;";
            using (SqlConnection connection = new SqlConnection(connectionStr));

            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format("{0}, {1}", reader[0], reader[1]));
                    }
                }
            }

            /*foreach (dbobject move in dbobjects)
            {
                move maken
            }*/
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
