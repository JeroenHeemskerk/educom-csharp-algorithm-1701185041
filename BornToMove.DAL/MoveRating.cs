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
        public double Vote { get; set; }
    }
}
