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
            this.Context = context;
            context.CreateTableMoveIfTableMoveIsEmpty();
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
        public List<Move> ReadAllMoves()
        {
            try
            {
                var moves = Context.Move.Include(m => m.Ratings).ToList();
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
