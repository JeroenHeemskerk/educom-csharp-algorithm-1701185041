using BornToMove.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BornToMove
{
    internal class Presenter
    {
        View View;

        public Presenter(View view)
        {
            this.View = view;
        }

        public void StartProgram()
        {
            View.DisplayProgramHeader();
            int choice = View.GetChoice("Wilt u zelf een beweging uitzoeken?", "Typ 1 voor 'Ja', 2 voor 'Nee' en 3 voor meer opties.", 1, 3);

            switch (choice)
            {
                case 1:
                    View.DisplayMoves();
                    choice = View.GetChoice("Welke move wilt u uitvoeren?", "Geef het id op van de move die u wilt uitvoeren, of een '0' om een eigen move in te voeren.", 0, View.GetMaximumMoves());
                    if (choice == 0)
                    {
                        View.HandleUserInput();
                        View.DisplayThanksForNewMove();
                    }
                    else
                    {
                        choice -= 1;
                        View.DisplayChosenMove(choice);
                        View.GetRatings();
                    }
                    break;
                case 2:
                    View.DisplayRandomMove();
                    View.GetRatings();
                    break;
                case 3:
                    Console.WriteLine();
                    choice = View.GetChoice("U heeft gekozen voor meer opties.", "Typ 1 om een 'move' te updaten of 2 om een 'move' te verwijderen.", 1, 2);
                    if (choice == 1)
                    {
                        View.DisplayRequestMoveToUpdate();
                        View.DisplayMoves();

                        int id = View.GetId("Geef het id-nummer van de 'move' die u wilt aanpassen.");
                        View.DisplayMoveToUpdate(id);

                        int realMoveId = View.HandleUserInput(id);
                        View.DisplayUpdatedMove(realMoveId);
                    }
                    else if (choice == 2)
                    {
                        //Dit werkt nog niet, is zo te zien ook niet onderdeel van de opdracht

                        View.DisplayRequestMoveToDelete();
                        View.DisplayMoves();

                        int id = View.GetId("Geef het id-nummer van de 'move' die u wilt verwijderen.");
                        View.DeleteMove(id);
                    }
                    break;
            }
        }
    }
}
