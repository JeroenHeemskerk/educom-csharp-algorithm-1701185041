using System;
using System.Data;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using BornToMove.DAL;
using BornToMove.Business;


namespace BornToMove.Index
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            MoveContext context = new MoveContext();
            MoveCrud moveCrud = new MoveCrud(context);
            BuMove buMove = new BuMove(moveCrud);
            View view = new View(buMove);
            view.StartProgram();
        }
    }
}
