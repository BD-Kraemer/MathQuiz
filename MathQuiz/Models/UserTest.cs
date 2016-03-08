using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
/* 
 Created by Zain Hatim
 On Feb/09/2016
*/
namespace MathQuiz.Models
{
    [Table("UserTest",Schema = "CMSC495")]
    public partial class UserTest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TestID { get; set; }
        public string UserId { get; set; }
        public Nullable<short> Difficulty { get; set; }
        public Nullable<decimal> Score { get; set; }
        public Nullable<DateTime> DateCreated { get; set; }
        public string Skills { get; set; }
                    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Problem> Problems { get; set; }
    }

    [Table("Problem", Schema = "CMSC495")]
    public partial class Problem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProbId { get; set; }
        public Nullable<decimal> FirstNumber { get; set; }
        public Nullable<decimal> SecondNumber { get; set; }
        public string Operator { get; set; }
        public Nullable<decimal> CorrectAnswer { get; set; }
        public Nullable<decimal> FalseAnswer1 { get; set; }
        public Nullable<decimal> FalseAnswer2 { get; set; }
        public Nullable<decimal> FalseAnswer3 { get; set; }
        public Nullable<decimal> UserAnswer { get; set; }
        public Nullable<int> Test_FK { get; set; }
        [ForeignKey("Test_FK")]
        public virtual UserTest UserTest { get; set; }
        public Nullable<int> seq { get; set; }
        
    }
}