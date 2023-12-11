using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BornToMove;

namespace BornToMove.DAL
{

    public class MoveCrud
    {
        private MoveContext context;

        public MoveCrud(MoveContext context)
        {
            this.context = context;
        }

        public void CreateMove(Move move)
        {
            try
            {
                context.Move.Add(move);
                context.SaveChanges();
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
                var selectedMove = context.Move
                    .Where(move => move.id == id)
                    .FirstOrDefault();

                if (selectedMove != null)
                {
                    context.Move.Remove(selectedMove);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }
        }
        public List<Move> ReadAllMoves()
        {
            try
            {
                var moves = context.Move.ToList();
                return moves;
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
            var selectedMove = context.Move
                .Where(move => move.id == id)
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
                context.Move.Update(move);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }            

        }
    }
}
