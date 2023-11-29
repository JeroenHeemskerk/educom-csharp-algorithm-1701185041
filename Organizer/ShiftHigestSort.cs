using System;
using System.Collections.Generic;

namespace Organizer
{
	public class ShiftHighestSort
    {
        private List<int> List;

        public ShiftHighestSort(List<int> list)
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
            bool finished = false;
            int temp;

            while (!finished)
            {
                for (int i = low; i < high; i++)
                {
                    if (List[i] > List[i + 1])
                    {
                        temp = List[i];
                        List[i] = List[i + 1];
                        List[i + 1] = temp;
                    }
                }

                if (low == high)
                {
                    finished = true;
                }

                high--;
            }
        }    
    }
}
