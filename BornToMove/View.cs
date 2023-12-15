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

        public void DeleteMove(int id)
        {
            int realMoveId = BuMove.GetRealMoveId(id);
            BuMove.DeleteMove(realMoveId);
            Console.WriteLine("'Move' " + BuMove.GetMoveName(id) + " is verwijderd.");
        }

        public void DisplayChosenMove(int id)
        {
            BuMove.SetCurrentMove(id);
            BuMove.CurrentMove.DisplayMove();
            BuMove.MoveRatings[id].DisplayRating();
        }

        public void DisplayMoves()
        {
            BuMove.GetMoveRatings();
            foreach (var moveAndRatings in BuMove.MoveRatings)
            {
                Console.WriteLine("Id: " + moveAndRatings.Key);
                moveAndRatings.Value.Move.DisplayMove(true);
                moveAndRatings.Value.DisplayRating();
            }
        }

        public void DisplayMoveToUpdate(int id)
        {
            Console.WriteLine("De 'move' die u wilt aanpassen is:");
            BuMove.Moves[id].DisplayMove();
            Console.WriteLine();
        }

        public void DisplayProgramHeader()
        {
            Console.WriteLine("BORNTOMOVE");
            Console.WriteLine("Tijd om te bewegen!");
        }

        public void DisplayRandomMove()
        {
            MoveRating randomMoveRating = BuMove.GetRandomMove();
            Console.WriteLine("De volgende move hebben wij voor u geselecteerd:");
            Console.WriteLine();
            randomMoveRating.Move.DisplayMove();
            randomMoveRating.DisplayRating();
            Console.WriteLine("Veel plezier!");
            Console.WriteLine();
        }

        public void DisplayRequestMoveToDelete()
        {
            Console.WriteLine("Welke 'move' wilt u verwijderen?");
        }

        public void DisplayRequestMoveToUpdate()
        {
            Console.WriteLine("Welke 'move' wilt u updaten?");
        }

        public void DisplayThanksForNewMove()
        {
            Console.WriteLine("Dankuwel voor uw nieuwe 'move'. Uw 'move' zal na beoordeling van onze experts mogelijk in de toekomst terug te vinden in deze applicatie!");
        }

        public void DisplayUpdatedMove(int realMoveId)
        {
            Console.WriteLine("De geupdatete versie vindt u hieronder:");
            Move move = BuMove.GetMove(realMoveId);
            move.DisplayMove();
        }

        public int GetChoice(string question, string option, int min, int max)
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

        public int GetId(string instruction)
        {
            bool correctInput = false;
            string userIdInput = "";
            string error;

            while(!correctInput)
            {
                Console.WriteLine(instruction);
                userIdInput = Console.ReadLine();
                Console.WriteLine();
                error = ViewUtils.CheckMoveId(userIdInput, GetMaximumMoves());
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

        public int GetMaximumMoves()
        {
            return BuMove.MoveRatings.Count;
        }

        public string GetUserMoveDescription(string instruction)
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

        public string GetUserMoveName(string question, IMoveChecker moveChecker, string oldName = "")
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

        public int GetUserMoveSweatRate(string instruction)
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

        public void HandleRatings()
        {
            double rating = GetChoice("Hoe vond u deze oefening?", "Vul een rating in van '1' tot en met '5'", 1, 5);
            double intensityRating = GetChoice("Hoe intens was deze oefening?", "Vul een rating in van '1' tot en met '5'", 1, 5);
            BuMove.WriteMoveRating(rating, intensityRating);
            Console.WriteLine("U gaf deze oefening een rating van: " + rating + " en een intensiteitsrating van: " + intensityRating + ".");
            Console.WriteLine("Dankuwel voor uw input!");
        }

        public void HandleUserInputForCreatingMove()
        {
            //Moves worden opgehaald uit de database om te kunnen controleren op nieuwe input
            BuMove.GetMoves();

            string newName = GetUserMoveName("U heeft opgegeven dat u graag een nieuwe 'move' wilt toevoegen. Wat is de naam van uw 'move'?", moveChecker: BuMove);
            string newDescription = GetUserMoveDescription("Geef een beschrijving van uw 'move'.");
            int newSweatRate = GetUserMoveSweatRate("Geef een 'Sweatrate' voor uw 'move'. Dit mag een geheel getal zijn van 1 tot en met 5.");
            BuMove.WriteMove(newName, newDescription, newSweatRate);
        }

        public int HandleUserInputForUpdatingMove(int id)
        {
            //Moves worden opgehaald uit de database om te kunnen controleren op nieuwe input
            BuMove.GetMoves();

            string newName = GetUserMoveName("Welke naam wilt u opnemen voor deze 'move'?", BuMove, BuMove.Moves[id].Name);
            string newDescription = GetUserMoveDescription("Geef een nieuwe beschrijving voor deze 'move'.");
            int newSweatRate = GetUserMoveSweatRate("Wat is de 'SweatRate' van deze move? Dit mag een geheel getal zijn van 1 tot en met 5.");
            int realMoveId = BuMove.GetRealMoveId(id);
            BuMove.UpdateMove(id, newName, newDescription, newSweatRate);
            return realMoveId;
        }
    }
}
