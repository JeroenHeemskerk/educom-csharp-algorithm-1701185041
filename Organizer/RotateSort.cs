using System;
using System.Collections.Generic;

namespace Organizer
{
    public class RotateSort<T>
    {

        private List<T> List;
        private IComparer<T> Comparer;

        public RotateSort(List<T> list, IComparer<T> comparer)
        {
            List = list;
            Comparer = comparer;
        }

        public List<T> Sort()
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
            T pivot = List[high];
            int i = low - 1;

           for (int j = low; j < high; j++)
            {
                if (Comparer.Compare(List[j], pivot) < 0)
                {
                    i++;

                    T temp = List[i];
                    List[i] = List[j];
                    List[j] = temp;
                }
            }
            T temp1 = List[i + 1];
            List[i + 1] = List[high];
            List[high] = temp1;

            return i + 1;
        }
    }
}
