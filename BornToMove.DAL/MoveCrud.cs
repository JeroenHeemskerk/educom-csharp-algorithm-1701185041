using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BornToMove;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BornToMove.DAL
{
    public class MoveCrud
    {
        private MoveContext Context;

        public MoveCrud(MoveContext context)
        {
            Context = context;
            context.ApplyMigrations();
        }

        public void CreateMove(Move move)
        {
            try
            {
                Context.Move.Add(move);
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }
        }

        public void CreateMoveRating(MoveRating moveRating)
        {
            try
            {
                Context.MoveRating.Add(moveRating);
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }
        }

        public void DeleteMove(int id)
        {
            try
            {
                var selectedMove = Context.Move
                    .Where(move => move.Id == id)
                    .FirstOrDefault();

                if (selectedMove != null)
                {
                    Context.Move.Remove(selectedMove);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }
        }
        public List<Move>? ReadAllMoves()
        {
            try
            {
                var moves = Context.Move.ToList();
                return moves;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
                return null;
            }
        }

        public Dictionary<int, MoveRating>? ReadAllAverageRatings() 
        {
            try
            {
                var moves = Context.Move.ToList();

                var moveRatings = Context.MoveRating
                    .GroupBy(moveRating => moveRating.Move.Id)
                    
                    //Een dictionary wordt gemaakt om het Id van de move te koppelen aan de daadwerkelijke move en rating
                    .ToDictionary(
                        moveGroup => moveGroup.Key, //De .key van de movegroup is door de groupby de Id van de move
                        moveGroup => new MoveRating
                        { 
                            Move = moves.FirstOrDefault(Move => Move.Id == moveGroup.Key), //Hier wordt de move uit moves gezet die bij de Id hoort
                            Rating = moveGroup.Average(moveRating => moveRating.Rating), //Er wordt vervolgens een gemiddelde genomen van de ratings binnen deze groep
                        });
                return moveRatings;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
                return null;
            }
        }

        public Move? ReadMoveById(int id)
        {            
            try
            {
            var selectedMove = Context.Move
                .Where(move => move.Id == id)
                .FirstOrDefault();
            return selectedMove;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
                return null;
            }
        }

        public void UpdateMove(Move move)
        {            
            try
            {
                Context.Move.Update(move);
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }
        }
    }
}
