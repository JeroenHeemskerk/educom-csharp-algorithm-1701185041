using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using BornToMove.DAL;

namespace BornToMove.Business
{
    public class BuMove : IMoveChecker
    {
        private MoveCrud MoveCrud;
        public List<Move> Moves { get; set; }

        public BuMove(MoveCrud crud)
        {
            MoveCrud = crud;
        }        

        public bool CheckUniqueUserMoveName(string newName)
        {
            foreach (Move move in Moves)
            {
                if (move.Name == newName)
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckUniqueUserMoveName(string newName, string oldName)
        {
            foreach (Move move in Moves)
            {
                if (move.Name == newName && move.Name != oldName)
                {
                    return false;
                }
            }
            return true;
        }       

        public void DeleteMove(int id)
        {
            MoveCrud.DeleteMove(id);
        }

        public Move GetMove(int id)
        {
            return MoveCrud.ReadMoveById(id);
        }

        public void GetMoves()
        {            
            Moves = MoveCrud.ReadAllMoves();         
        }

        public Move GetRandomMove()
        {
            Random random = new Random();
            int randomIndex = random.Next(0, Moves.Count);
            return Moves[randomIndex];
        }       

        public void UpdateMove(int id, string newName, string newDescription, int newSweatRate)
        {
            Moves[id].Name = newName;
            Moves[id].Description = newDescription;
            Moves[id].SweatRate = newSweatRate;
            MoveCrud.UpdateMove(Moves[id]);
        }

        public void WriteMove(string name, string description, int sweatRate)
        {
            Move newMove = new Move(0, name, description, sweatRate);
            MoveCrud.CreateMove(newMove);
        }
    }
}
