using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathQuiz.Models.Math
{

    //This interface is implemented by all classes that are used to create a single problem.
    
    public interface IArithmetic
    {
        Problem GetProblem(Difficulty difficulty);
    }
}
