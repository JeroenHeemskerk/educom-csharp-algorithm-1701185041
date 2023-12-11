using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BornToMove
{
    public class Move
    {
        public int? id { get; }
        public string name { get; }
        public string description { get; }
        public int sweatRate { get; }

        public Move (int? id, string name, string description, int sweatRate)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.sweatRate = sweatRate;           
        }

        public void DisplayMove(bool userChoosingMove)
        {
            Console.WriteLine("Naam: " + name);
            if (!userChoosingMove)
            {
                Console.WriteLine("Beschrijving: " + description);
                Console.WriteLine("Sweatrate: " + sweatRate);
            }
            Console.WriteLine();
        }
    }
}
