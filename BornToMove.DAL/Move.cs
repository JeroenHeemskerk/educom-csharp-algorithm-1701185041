using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BornToMove
{
    public class Move
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int SweatRate { get; set; }

        private Move()
        {

        }

        public Move (int id, string name, string description, int sweatRate)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.SweatRate = sweatRate;          
        }

        public void DisplayMove(bool userChoosingMove = false)
        {
            Console.WriteLine("Naam: " + Name);
            if (!userChoosingMove)
            {
                Console.WriteLine("Beschrijving: " + Description);
                Console.WriteLine("Sweatrate: " + SweatRate);
            }
            Console.WriteLine();
        }
    }
}
