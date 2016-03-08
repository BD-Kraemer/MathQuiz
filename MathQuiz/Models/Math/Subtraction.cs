using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MathQuiz.Models.Math
{
    public class Subtraction : IArithmetic
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

            //Disallow negative answers
            if (problem.Value1 < problem.Value2)
            {
                int temp = problem.Value2;
                problem.Value2 = problem.Value1;
                problem.Value1 = temp;
            }

            //Populate correct answer
            problem.Answer = problem.Value1 - problem.Value2;

            //Add 3 unique wrong answers. In this case, the lowest incorrect answer cannot
            //be lower than the lowest number minus the highest number combined and no larger than the 
            //highest number minus the lowest combined
            while (problem.IncorrectAnswers.Count < 3)
            {
                int value = NumberGen.GetNumber(low, high - low);
                if (value != problem.Answer)
                    problem.IncorrectAnswers.Add(value);
            }

            problem.Operand = '-';
        }
    }
}