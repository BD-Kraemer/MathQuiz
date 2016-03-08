using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MathQuiz.Models
{
    public partial class Problem
    {
        [NotMapped]
        public int Value1 { get; set; }
        [NotMapped]
        public int Value2 { get; set; }
        [NotMapped]
        public char Operand {get; set; }
        [NotMapped]
        public int Answer { get; set; }
        //Use a HashSet because the values must be unique, that way we cannot have
        //duplicate Incorrect Answers
        [NotMapped]
        public HashSet<int> IncorrectAnswers { get; set; } = new HashSet<int>();
        

    }

    
}