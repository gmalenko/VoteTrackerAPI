using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoteTrackerAPI.Business
{
    public class Exceptions
    {
        public static void TestForNull(Object o, string name)
        {
            if (o == null)
            {
                throw new Exception(name + " cannot be null");
            }
        }

        public static void TestForNullEmptyString(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                throw new Exception(input + " cannot be null or empty");
            }
        }

        public static void TestForNumberGreaterThanZero(int number, string name)
        {
            if (number > 0)
            {

            }
            else
            {
                throw new Exception(name + " must be a number greater than 0");
            }
        }
    }
}
