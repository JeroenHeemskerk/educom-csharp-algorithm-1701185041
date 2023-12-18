using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Organizer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<int> myList;
            int size = RequestListSize();
            myList = CreateList(size);
            ShowList("Lijst met willekeurige nummers", myList);

            TimeSpan ts;
            ts = SortAndTime(myList, "shiftHighestSort");
            Console.WriteLine("Het duurde: " + ts.ToString());

            ts = SortAndTime(myList, "rotateSort");
            Console.WriteLine("Het duurde: " + ts.ToString());
        }

        private static void CheckList(List<int> list)
        {
            //list.Count wordt met 1 gereduceerd nu amount 1 groter is dan de daadwerkelijke hoeveelheid indexen
            //De index begint immers op 0
            int amount = list.Count - 1;
            bool isListSorted = true;

            for (int i = 0; i < amount; i++) 
            {
                if (list[i] > list[i + 1])
                {
                    isListSorted = false;
                    break;
                }
            }

            if (isListSorted)
            {
                Console.WriteLine("De lijst is correct gesorteerd");
            }
            else
            {
                Console.WriteLine("De lijst is niet correct gesorteerd");
            }
        }

        private static List<int> CreateList(int n)
        {
            List<int> tempList = new List<int>();
            Random random = new Random();
            int number;

            for (int i = 0; i < n; i++)
            {
                number = random.Next(-99, 100);
                tempList.Add(number);
            }

            return tempList;
        }

        private static int RequestListSize()
        {
            Console.WriteLine("Geef hieronder aan hoe lang de lijst moet zijn");
            int listSize = Convert.ToInt32(Console.ReadLine());
            return listSize;
        }

        public static void ShowList(string label, List<int> theList)
        {
            int count = theList.Count;
            if (count > 200)
            {
                count = 200; // Do not show more than 200 numbers
            }
            Console.WriteLine();
            Console.Write(label);
            Console.Write(':');
            for (int index = 0; index < count; index++)
            {
                if (index % 20 == 0) // when index can be divided by 20 exactly, start a new line
                {
                    Console.WriteLine();
                }
                Console.Write(string.Format("{0,3}, ", theList[index]));  // Show each number right aligned within 3 characters, with a comma and a space
            }
            Console.WriteLine();
        }

        private static TimeSpan SortAndTime(List<int> list, string sortMethod)
        {
            Stopwatch stopwatch = new Stopwatch();

            switch (sortMethod)
            {
                case "shiftHighestSort":
                    ShiftHighestSort sorter = new ShiftHighestSort(list);
                    stopwatch.Start();
                    list = sorter.Sort();
                    stopwatch.Stop();
                    ShowList("Lijst met gesorteerde nummers op basis van ShiftHighgestSort", list);
                    break;
                case "rotateSort":
                    RotateSort<int> rotateSort = new RotateSort<int>();
                    stopwatch.Start();
                    list = rotateSort.Sort(list, Comparer<int>.Default);
                    stopwatch.Stop();
                    ShowList("Lijst met gesorteerde nummers op basis van RotateSort", list);
                    break;
            }
            CheckList(list);
            return stopwatch.Elapsed;
        }
    }
}
