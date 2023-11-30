using System;
using System.Collections.Generic;

namespace Organizer
{
    public class RotateSort
    {

        private List<int> List;

        public RotateSort(List<int> list)
        {
            List = list;
        }

        public List<int> Sort()
        {
            SortFunction(0, List.Count - 1);
            return List;
        }

        private void SortFunction(int low, int high)
        {
            if (List.Count <= 1)
            {
                return;
            }

            if (low < high)
            {
                int splitPoint = Partitioning(low, high);

                SortFunction(low, splitPoint - 1);
                SortFunction(splitPoint + 1, high);
            }
        }

        private int Partitioning(int low, int high)
        {
            int pivot = List[high];
            int i = low - 1;

           for (int j = low; j < high; j++)
            {
                if (List[j] < pivot)
                {
                    i++;

                    int temp = List[i];
                    List[i] = List[j];
                    List[j] = temp;
                }
            }
            int temp1 = List[i + 1];
            List[i + 1] = List[high];
            List[high] = temp1;

            return i + 1;
        }
    }
}
