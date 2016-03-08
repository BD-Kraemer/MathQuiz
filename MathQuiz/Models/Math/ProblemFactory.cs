using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace MathQuiz.Models.Math
{
    public class ProblemFactory
    {

        //Stores the problem types that are currently possible.
        private Dictionary<Type, IArithmetic> problemTypes = new Dictionary<Type, IArithmetic>();
        
        //Set the difficult of the problems generated. 
        public Difficulty Difficulty { get; set; } = Difficulty.Easy;

        /// <summary>
        /// Adds a problem type to the problems that can be generated.
        /// </summary>
        /// <param name="arithmetic">The type of problem to be added.</param>
        public void AddProblemType(IArithmetic arithmetic)
        {
            if (!problemTypes.ContainsKey(arithmetic.GetType()))
            {
                problemTypes.Add(arithmetic.GetType(), arithmetic);
            }
        }

        /// <summary>
        /// Removes a problem type to the problems that can be generated.
        /// </summary>
        /// <param name="arithmetic">The type of problem to be removed.</param>
        public void RemoveProblemType(IArithmetic arithmetic)
        {
            if (problemTypes.ContainsKey(arithmetic.GetType()))
            {
                problemTypes.Remove(arithmetic.GetType());
            }
        }

      
        //Brian Kraemer - 2/26 - Removed duplicate method, used default value.
        public Problem GetProblem(int seq = 0)
        {
            //Gets a random problem type (addition, subtraction, etc) from the types currently available.
            //We do this by selecting a random element in the dictionary, getting it's value and then calling
            //.GetProblem() to get the problem.
            if (problemTypes.Count == 0)
                throw new InvalidOperationException("A problem cannot be generated if no problem types have been added. Please add a problem type and try again");
            Problem p = problemTypes.ElementAt(NumberGen.GetNumber(0, problemTypes.Count)).Value.GetProblem(Difficulty);      
            p.seq = seq;
            return p;
        }
    }
}