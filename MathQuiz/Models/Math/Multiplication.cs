using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MathQuiz.Models.Math
{
    public class Multiplication : IArithmetic
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
            //Populate the values based on the low and high value set by the difficulty
            problem.Value1 = NumberGen.GetNumber(low, high);
            problem.Value2 = NumberGen.GetNumber(low, high);

            //Populate correct answer
            problem.Answer = problem.Value1 * problem.Value2;

            //Add 3 unique wrong answers. 
            //This depends on whether the lowest number is negative or not
            //if it is NOT negative, low * low will givest the lowest poissble value
            //If it IS negative, low * high will give the lowest possible value
            while (problem.IncorrectAnswers.Count < 3)
            {
                int value = 0;
                if (low > 0)
                    value = NumberGen.GetNumber(low * low, high * high);
                else if (low < 0)
                    value = NumberGen.GetNumber(low * high, high * high);
                else if (low == 0)
                    value = NumberGen.GetNumber(0, high * high);

                if (value != problem.Answer)
                    problem.IncorrectAnswers.Add(value);
            }

            problem.Operand = '*';
        }
    }
}