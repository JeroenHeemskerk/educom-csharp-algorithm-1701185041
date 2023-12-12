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
        private BuMove BuMove;

        public View(BuMove buMove)
        {
            this.BuMove = buMove;
        }

        public void StartProgram()
        {
            ProgramHeader();
            BuMove.GetMoves();

            int choice = GetChoice("Wilt u zelf een beweging uitzoeken?", "Typ 1 voor 'Ja', 2 voor 'Nee' en 3 voor meer opties.", 1, 3);

            switch (choice) {
                case 1:            
                    DisplayMoves();
                    choice = GetChoice("Welke move wilt u uitvoeren?", "Geef het id op van de move die u wilt uitvoeren, of een '0' om een eigen move in te voeren.", 0, BuMove.Moves.Count);
                    if (choice == 0)
                    {
                        string newName = GetUserMoveName("U heeft opgegeven dat u graag een nieuwe 'move' wilt toevoegen. Wat is de naam van uw 'move'?", moveChecker: BuMove);
                        string newDescription = GetUserMoveDescription("Geef een beschrijving van uw 'move'.");
                        int newSweatRate = GetUserMoveSweatRate("Geef een 'Sweatrate' voor uw 'move'. Dit mag een geheel getal zijn van 1 tot en met 5.");
                        BuMove.WriteMove(newName, newDescription, newSweatRate);
                        Console.WriteLine("Dankuwel voor uw nieuwe 'move'. Uw 'move' is nu te vinden in deze applicatie!");
                    }
                    else
                    {
                        choice -= 1;
                        BuMove.Moves[choice].DisplayMove();
                        GetRatings();
                    }
                    break;
                case 2:
                    DisplayRandomMove();
                    GetRatings();
                    break;
                case 3:
                    Console.WriteLine();
                    choice = GetChoice("U heeft gekozen voor meer opties.", "Typ 1 om een 'move' te updaten of 2 om een 'move' te verwijderen.", 1, 2);
                    if (choice == 1)
                    {
                        Console.WriteLine("Welke 'move' wilt u updaten?");
                        DisplayMoves();

                        int id = GetId(BuMove.Moves.Count);
                        Console.WriteLine("De 'move' die u wilt aanpassen is:");
                        BuMove.Moves[id].DisplayMove();
                        Console.WriteLine();

                        string newName = GetUserMoveName("Welke naam wilt u opnemen voor deze 'move'?", BuMove, BuMove.Moves[id].Name);
                        string newDescription = GetUserMoveDescription("Geef een nieuwe beschrijving voor deze 'move'.");
                        int newSweatRate = GetUserMoveSweatRate("Wat is de 'SweatRate' van deze move? Dit mag een geheel getal zijn van 1 tot en met 5.");
                        int realMoveId = BuMove.Moves[id].Id;
                        BuMove.UpdateMove(id, newName, newDescription, newSweatRate);

                        Console.WriteLine("De geupdatete versie vindt u hieronder:");
                        Move move = BuMove.GetMove(realMoveId);
                        move.DisplayMove();
                    }
                    else if (choice == 2)
                    {
                        //Dit werkt nog niet, is zo te zien ook niet onderdeel van de opdracht

                        Console.WriteLine("Welke 'move' wilt u verwijderen?");
                        DisplayMoves();

                        int id = GetId(BuMove.Moves.Count);
                        int realMoveId = BuMove.Moves[id].Id;
                        Console.WriteLine(realMoveId);
                        BuMove.DeleteMove(realMoveId);
                        Console.WriteLine("'Move' " + BuMove.Moves[id].Name + " is verwijderd.");
                    }
                    break;
            }
        }

        private void DisplayMoves()
        {
            int id = 1;
            foreach (Move move in BuMove.Moves)
            {
                Console.WriteLine("Id: " + id);
                move.DisplayMove(true);
                id++;
            }
        }

        private void DisplayRandomMove()
        {
            Move randomMove = BuMove.GetRandomMove();
            Console.WriteLine("De volgende move hebben wij voor u geselecteerd:");
            Console.WriteLine();
            randomMove.DisplayMove();
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
                choice = ViewUtils.SanitizeInput(choice);
                choiceMade = ViewUtils.TestChoiceInput(choice, min, max);

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

        private int GetId(int max)
        {
            bool correctInput = false;
            string userIdInput = "";
            string error;

            while(!correctInput)
            {
                Console.WriteLine("Geef het id-nummer van de 'move' die u wilt aanpassen.");
                userIdInput = Console.ReadLine();
                Console.WriteLine();
                error = ViewUtils.CheckMoveId(userIdInput, max);
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
            int id = int.Parse(userIdInput) - 1;
            return id;
        }

        private string GetUserMoveDescription(string instruction)
        {
            bool correctInput = false;
            string newDescription = "";
            string error;

            while (!correctInput)
            {
                Console.WriteLine(instruction);
                Console.WriteLine("De beschrijving mag maximaal 200 karakters lang zijn en mag geen speciale karakters of underscores bevatten");

                newDescription = Console.ReadLine();

                error = ViewUtils.CheckUserMoveDescription(newDescription);
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

        private string GetUserMoveName(string question, IMoveChecker moveChecker, string oldName = "")
        {
            bool newInput;
            string newName = "";
            string error;

            do
            {
                bool correctInput = false;
                while (!correctInput)
                {
                    Console.WriteLine(question);
                    Console.WriteLine("De naam mag twintig letters lang zijn en mag geen speciale tekens of nummers bevatten.");
                    newName = Console.ReadLine();

                    error = ViewUtils.CheckUserMoveName(newName);

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

                //Indien er wordt geupdate wordt een andere functie aangeroepen om na te gaan of de nieuwe naam niet gelijk is aan de naam van andere moves
                if (oldName == "")
                {
                    newInput = moveChecker.CheckUniqueUserMoveName(newName);
                } 
                else
                {
                    newInput = moveChecker.CheckUniqueUserMoveName(newName, oldName);
                }

                if (!newInput)
                {
                    Console.WriteLine("Deze naam is helaas al in gebruik. Probeer het Opnieuw.");
                    Console.WriteLine();
                }
            } while (!newInput);

            Console.WriteLine("De naam van uw 'move' is: " + newName);
            Console.WriteLine();
            return newName;
        }

        private int GetUserMoveSweatRate(string instruction)
        {
            bool correctInput = false;
            string newSweatRate = "";
            string error;

            while (!correctInput)
            {
                Console.WriteLine(instruction);
                newSweatRate = Console.ReadLine();
                error = ViewUtils.CheckUserMoveSweatRate(newSweatRate);
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
