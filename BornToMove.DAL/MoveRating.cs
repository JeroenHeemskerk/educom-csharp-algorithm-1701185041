using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BornToMove.DAL
{
    public class MoveRating
    {
        public int Id { get; set; }
        public Move Move { get; set; }
        public double Rating { get; set; }
        public double Intensity { get; set; }

        public MoveRating()
        {

        }
        public MoveRating(int id, Move move, double rating, double intensity)
        {
            Id = id;
            Move = move;
            Rating = rating;
            Intensity = intensity;
        }

        public void DisplayRating()
        {
            Console.WriteLine("Rating: " + Rating);
            Console.WriteLine();
        }
    }
}
