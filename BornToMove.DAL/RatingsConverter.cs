using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BornToMove.DAL
{
    public class RatingsConverter : IComparer<MoveRating>
    {
        public int Compare(MoveRating x, MoveRating y)
        {
            return y.Rating.CompareTo(x.Rating);
        }
    }
}
