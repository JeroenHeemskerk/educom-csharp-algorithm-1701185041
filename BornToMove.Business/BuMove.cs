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
        public Dictionary<int, MoveRating> MoveRatings { get; set; }
        public Move CurrentMove { get; set; }

        private MoveRating MoveRating;

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

        public string GetMoveName(int id)
        {
            return Moves[id].Name;
        }

        public void GetMoveRatings()
        {
            MoveRatings = MoveCrud.ReadAllAverageRatings();
        }

        public void GetMoves()
        {            
            Moves = MoveCrud.ReadAllMoves();         
        }

        public MoveRating GetRandomMove()
        {
            GetMoveRatings();
            Random random = new Random();
            int randomIndex = random.Next(0, MoveRatings.Count);
            SetCurrentMove(randomIndex);
            return MoveRatings[randomIndex];
        }
        
        public int GetRealMoveId(int id)
        {
            return Moves[id].Id;
        }

        public void SetCurrentMove(int index)
        {
            CurrentMove = MoveRatings[index].Move;
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

        public void WriteMoveRating(double rating, double intensityRating)
        {
            MoveRating = new MoveRating(0, CurrentMove, rating, intensityRating);
            MoveCrud.CreateMoveRating(MoveRating);
        }

        public void WriteMoveRating(Move newMove, double rating, double intensityRating)
        {
            MoveRating = new MoveRating(0, newMove, rating, intensityRating);
            MoveCrud.CreateMoveRating(MoveRating);
        }
    }
}
