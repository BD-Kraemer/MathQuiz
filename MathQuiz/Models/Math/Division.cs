/*
*
*  Author: Brian Kraemer
*
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MathQuiz.Models.Math
{
    public class Division : IArithmetic
    {
        public Problem GetProblem(Difficulty difficulty)
        {
            Problem problem = new Problem();
            switch (difficulty)
            {
                case Difficulty.Easy:
                    PopulateProblem(1, 11, problem);
                    break;
                case Difficulty.Medium:
                    PopulateProblem(1, 51, problem);
                    break;
                case Difficulty.Hard:
                    PopulateProblem(1, 101, problem);
                    break;
            }
            return problem;
        }

        void PopulateProblem(int low, int high, Problem problem)
        {
            //For division problems, we must insure that the division answer is a whole number, 
            //To do this, we generate the second value and the answer first. We then
            //get the first value by multipyling the answer and the second value.

            //EG:  35 / 7 = 5.  We generate the number 7 (second value), the number 5 (answer)
            // and multiply them together to get 35 (firts value). This guaruntees whole numbers.
            //Also, value 2 cannot be 0 and the range of random numbers generated cannot be so small
            //as to only generate 0.

            if (low == 0 && (high == 1 || high == 0))
                throw new ArgumentException("Attempting to generate a division problem where the denomenator must be 0. Please adjust the range of numbers generated");


            while (problem.Value2 == 0)
            {
                problem.Value2 = NumberGen.GetNumber(low, high);
            }
            problem.Answer = NumberGen.GetNumber(low, high);
            problem.Value1 = problem.Value2*problem.Answer;

            //3 unique random answers. If low and high are positive, lowest answer can only be 0 or 1
            //if only one is negative, then lowest answer would be the negative number

            while (problem.IncorrectAnswers.Count < 3)
            {
                int value = 0;
                if ((low > 0 && high > 0) || (low < 0 && high < 0))
                {
                    value = NumberGen.GetNumber(1, high);
                }
                else
                {
                    value = NumberGen.GetNumber(low, high);
                }
                if (value != problem.Answer)
                    problem.IncorrectAnswers.Add(value);
            }
            problem.Operand = '/';
        }
    }
}