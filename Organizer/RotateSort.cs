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
            SortFunction();
            return List;
        }

        private void SortFunction()
        {
            if (List.Count - 1 <= 1)
            {
                return;
            }

            int splitPoint;

            splitPoint = Partitioning(0, List.Count - 1);
            Partitioning(0, splitPoint);
            Partitioning(splitPoint, List.Count - 1);
        }

        private int Partitioning(int low, int high)
        {
            int pivot = (high / 2);
            int splitPoint = -1;
            bool finished = false;

            while (!finished)
            {
                for (int L = low; L < high; L++)
                {
                    if (List[L] > List[pivot])
                    {                    
                        for (int H = high; H > L; H--)
                        {
                            if (H <= L)
                            {
                                splitPoint = L;
                                finished = true;
                                break;
                            }
                            if (List[H] <= List[pivot])
                            {
                                List[L] = List[H];
                                break;
                            }
                        }
                    }
                }
            }
            return splitPoint;
        }
    }
}
