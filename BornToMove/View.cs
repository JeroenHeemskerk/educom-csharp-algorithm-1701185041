using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BornToMove.Business;
using BornToMove.DAL;

namespace BornToMove
{
    public class View
    {
        private BuMove buMove;

        public View(BuMove buMove)
        {
            this.buMove = buMove;
        }

        public void StartProgram()
        {
            ProgramHeader();
            buMove.GetMoves();

            int choice = GetChoice("Wilt u zelf een beweging uitzoeken?", "Typ 1 voor 'Ja' en 2 voor 'Nee'.", 1, 2);

            if (choice == 1)
            {
                DisplayMoves();
                choice = GetChoice("Welke move wilt u uitvoeren?", "Geef het id op van de move die u wilt uitvoeren, of een '0' om een eigen move in te voeren.", 0, buMove.moves.Count);
                if (choice == 0)
                {
                    string newName = GetUserMoveName();
                    string newDescription = GetUserMoveDescription();
                    int newSweatRate = GetUserMoveSweatRate();
                    buMove.WriteMove(newName, newDescription, newSweatRate);
                    Console.WriteLine("Dankuwel voor uw nieuwe 'move'. Uw 'move' is nu te vinden in deze applicatie!");
                }
                else
                {
                    choice -= 1;
                    buMove.moves[choice].DisplayMove(false);
                    GetRatings();
                }
            }
            else if (choice == 2)
            {
                DisplayRandomMove();
                GetRatings();
            }
        }

        private void DisplayMoves()
        {
            int id = 1;
            foreach (Move move in buMove.moves)
            {
                Console.WriteLine("Id: " + id);
                move.DisplayMove(true);
                id++;
            }
        }

        private void DisplayRandomMove()
        {
            Move randomMove = buMove.GetRandomMove();
            Console.WriteLine();
            Console.WriteLine("De volgende move hebben wij voor u geselecteerd:");
            Console.WriteLine();
            randomMove.DisplayMove(false);
            Console.WriteLine("Veel plezier!");
            Console.WriteLine();
        }

        private int GetChoice(string question, string option, int min, int max)
        {
            bool choiceMade = false;
            string choice = "";
            int convertedChoice;

            Console.WriteLine(question);
            Console.WriteLine(option);

            while (!choiceMade)
            {
                choice = Console.ReadLine();
                choice = buMove.SanitizeInput(choice);
                choiceMade = buMove.TestChoiceInput(choice, min, max);

                if (!choiceMade)
                {
                    Console.WriteLine();
                    Console.WriteLine("Ongeldige input, probeer het opnieuw.");
                    Console.WriteLine();
                    Console.WriteLine(question);
                    Console.WriteLine(option);
                }
            }

            Console.WriteLine();
            convertedChoice = int.Parse(choice);
            return convertedChoice;
        }

        private string GetUserMoveDescription()
        {
            bool correctInput = false;
            string newDescription = "";
            string error;

            while (!correctInput)
            {
                Console.WriteLine("Geef een beschrijving van uw 'move'.");
                Console.WriteLine("De beschrijving mag maximaal 200 karakters lang zijn en mag geen speciale karakters of underscores bevatten");

                newDescription = Console.ReadLine();

                error = buMove.CheckUserMoveDescription(newDescription);
                if (error != "")
                {
                    Console.WriteLine(error + " Probeer het opnieuw.");
                    Console.WriteLine();
                }
                else
                {
                    correctInput = true;
                }
            }
            Console.WriteLine("De beschrijving van uw 'move' is: ");
            Console.WriteLine(newDescription);
            Console.WriteLine();
            return newDescription;
        }

        private string GetUserMoveName()
        {
            bool newInput;
            string newName = "";
            string error;

            do
            {
                bool correctInput = false;
                while (!correctInput)
                {
                    Console.WriteLine("U heeft opgegeven dat u graag een nieuwe 'move' wilt toevoegen. Wat is de naam van uw 'move'?");
                    Console.WriteLine("De naam mag twintig letters lang zijn en mag geen speciale tekens of nummers bevatten.");
                    newName = Console.ReadLine();

                    error = buMove.CheckUserMoveName(newName);

                    if (error != "")
                    {
                        Console.WriteLine(error + " Probeer het opnieuw.");
                        Console.WriteLine();
                    }
                    else
                    {
                        correctInput = true;
                    }
                }

                newInput = buMove.CheckUniqueUserMoveName(newName);
                if (newInput)
                {
                    Console.WriteLine("Deze naam is helaas al in gebruik. Probeer het Opnieuw.");
                    Console.WriteLine();
                }
            } while (!newInput);

            Console.WriteLine("De naam van uw 'move' is: " + newName);
            Console.WriteLine();
            return newName;
        }

        private int GetUserMoveSweatRate()
        {
            bool correctInput = false;
            string newSweatRate = "";
            string error;

            while (!correctInput)
            {
                Console.WriteLine("Geef een 'Sweatrate' voor uw 'move'. Dit mag een geheel getal zijn van 1 tot en met 5");
                newSweatRate = Console.ReadLine();
                error = buMove.CheckUserMoveSweatRate(newSweatRate);
                if (error != "")
                {
                    Console.WriteLine(error + " Probeer het opnieuw.");
                    Console.WriteLine();
                }
                else
                {
                    correctInput = true;
                }
            }
            int sweatRate = int.Parse(newSweatRate);
            Console.WriteLine("De sweatrate van uw 'move' is: " + sweatRate);
            Console.WriteLine();
            return sweatRate;
        }

        private void GetRatings()
        {
            int rating, intensityRating;
            rating = GetChoice("Hoe vond u deze oefening?", "Vul een rating in van '1' tot en met '5'", 1, 5);
            intensityRating = GetChoice("Hoe intens was deze oefening?", "Vul een rating in van '1' tot en met '5'", 1, 5);
            Console.WriteLine("U gaf deze oefening een rating van: " + rating + " en een intensiteitsrating van: " + intensityRating + ".");
        }

        private void ProgramHeader()
        {
            Console.WriteLine("BORNTOMOVE");
            Console.WriteLine("Tijd om te bewegen!");
        }
    }
}
