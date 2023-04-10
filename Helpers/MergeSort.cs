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
        public List<int> Sort(List<int> arr)
        {
            if (arr.Count > 1)
            {
                int mid = arr.Count / 2;
                List<int> leftHalf = new List<int>();
                List<int> rightHalf = new List<int>();


                // Copy elements into the left and right sub-arrays
                for (int i = 0; i < mid; i++)
                {
                    leftHalf.Add(arr[i]);

                }
                for (int i = mid; i < arr.Count; i++)
                {
                    rightHalf.Add(arr[i]);
                }

                // Recursively sort the left and right sub-arrays
                Sort(leftHalf);
                Sort(rightHalf);

                // Merge the sorted sub-arrays

                arr.Clear();
                arr.AddRange(Merge(leftHalf, rightHalf)); ;
            }
            return arr;
        }

        private static List<int> Merge(List<int> left, List<int> right)
        {
            List<int> result = new List<int>();
            int leftPosition = 0;
            int rightPositon = 0;

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
