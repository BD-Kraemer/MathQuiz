using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MathQuiz.Models.Math
{

    //Static class to provide random numbers.
    public static class NumberGen
    {
        private static readonly Random _random = new Random();

        public static int GetNumber(int low, int high)
        {
            return _random.Next(low, high);
        }
    }
}