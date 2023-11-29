using System;
using System.Collections;
using System.Collections.Generic;

namespace Organizer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<int> myList;
            myList = CreateList(10);
            ShowList("Lijst met willekeurige nummers", myList);

            ShiftHighestSort sorter = new ShiftHighestSort(myList);
            myList = sorter.Sort();
            ShowList("Lijst met gesorteerde nummers", myList);

            Console.WriteLine();

            CheckList(myList);

            Console.WriteLine();

            List<int> myOtherList = new List<int> { 4, 5, 2, 1, 6, 3 };
            RotateSort rotateSort = new RotateSort(myOtherList);
            myList = rotateSort.Sort();
            ShowList("De andere lijst", myList);

            Console.WriteLine();
            CheckList(myList);

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


        public static void ShowList(string label, List<int> theList)
        {
            int count = theList.Count;
            if (count > 100)
            {
                count = 300; // Do not show more than 300 numbers
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
    }
}
