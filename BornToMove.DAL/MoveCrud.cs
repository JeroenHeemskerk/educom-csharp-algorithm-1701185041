using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BornToMove.DAL
{
    internal class MoveCrud
    {
        public static List<Move> readAllMoves()
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

        public static Move readMoveById(int id)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = BornToMove;Integrated Security=True;";
            string sqlQuery = "SELECT * " +
                              "FROM dbo.move" +
                              $"WHERE Id = ({id});";
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

        public static void CreateMove(Move move)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = BornToMove;Integrated Security=True;";
            string sqlQuery = $"INSERT INTO dbo.move (name, description, sweatRate) VALUES ('{move.name}', '{move.description}', {move.sweatRate})";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery(); //command.ExecuteNonQuery voert daadwerkelijk de query uit. ExecuteNonQuery is voor non select queries.

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Nieuwe move succesvol toegevoegd.");
                        }
                        else
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

        public static void UpdateMove(Move updatedMove)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = BornToMove;Integrated Security=True;";
            string sqlQuery = $"UPDATE dbo.move (name, description, sweatRate) VALUES ('{updatedMove.name}', '{updatedMove.description}', {updatedMove.sweatRate})";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery(); //command.ExecuteNonQuery voert daadwerkelijk de query uit. ExecuteNonQuery is voor non select queries.

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Move succesvol geupdate.");
                        }
                        else
                        {
                            Console.WriteLine("Move niet geupdate. Misschien is er iets mis gegaan");
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
    }
}
