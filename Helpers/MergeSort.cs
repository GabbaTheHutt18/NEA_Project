using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA_Project.Helpers
{
    public class MergeSort
    {
        public MergeSort()
        {
        }
        public List<int> Sort(List<int> Unsorted)
        {
            if (Unsorted.Count > 1)
            {
                int mid = Unsorted.Count / 2;
                List<int> leftHalf = new List<int>();
                List<int> rightHalf = new List<int>();


                // Copy elements into the left and right
                for (int i = 0; i < mid; i++)
                {
                    leftHalf.Add(Unsorted[i]);

                }
                for (int i = mid; i < Unsorted.Count; i++)
                {
                    rightHalf.Add(Unsorted[i]);
                }

                // Recursively sort the left and right 
                Sort(leftHalf);
                Sort(rightHalf);

                // Merge the sorted lists

                Unsorted.Clear();
                Unsorted.AddRange(Merge(leftHalf, rightHalf)); ;
            }
            return Unsorted;
        }

        private static List<int> Merge(List<int> left, List<int> right)
        {
            List<int> result = new List<int>();
            int leftPosition = 0;
            int rightPositon = 0;

            //Keep adding to the result list until either the left or right list has finished
            while (left.Count > leftPosition && right.Count > rightPositon)
            {
                if (left[leftPosition] < right[rightPositon])
                {
                    result.Add(left[leftPosition]);
                    leftPosition += 1;
                }
                else
                {
                    result.Add(right[rightPositon]);
                    rightPositon += 1;
                }
            }
            //These for loops make sure that the remaining values are added to the result list
            for (int i = leftPosition; i < left.Count; i++)
            {
                result.Add(left[i]);
            }
            for (int i = rightPositon; i < right.Count; i++)
            {
                result.Add(right[i]);
            }
            return result;

        }
    }
}
