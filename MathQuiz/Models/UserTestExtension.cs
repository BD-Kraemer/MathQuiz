/*  Created by John C Rinker II
*   2/12/2016
*   Extension of the UserTest Class  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MathQuiz.Models
{
    public partial class UserTest
    {
        // Default constructor needed for Linq first method
        public UserTest() { }

        // create object from database by primary key
        public UserTest(int _TestID)
        {
            DbContextModel db = new DbContextModel();
            UserTest ut = db.UserTests.First(u => u.TestID == _TestID);
            this.UserId = ut.UserId;
            this.TestID = ut.TestID;
            this.Difficulty = ut.Difficulty;
            this.Score = ut.Score;
            this.Problems = new HashSet<Problem>();
        }

        // create UserTestRecord and add to UserTest Table
        public UserTest(string userID)
        {
            DbContextModel db = new DbContextModel();
            this.UserId = userID;
            db.UserTests.Add(this);
            db.SaveChanges();
            this.Problems = new HashSet<Problem>();
        }

        // Cycle through problems collection and add each problem to the Problem Database Table
        public void AddProblemsToDb()
        {
            DbContextModel db = new DbContextModel();
            foreach (Problem p in this.Problems)
            {
                p.FirstNumber = p.Value1;
                p.SecondNumber = p.Value2;
                p.Operator = p.Operand.ToString();
                p.CorrectAnswer = p.Answer;
                p.FalseAnswer1 = p.IncorrectAnswers.ToList()[0];
                p.FalseAnswer2 = p.IncorrectAnswers.ToList()[1];
                p.FalseAnswer3 = p.IncorrectAnswers.ToList()[2];
                p.Test_FK = this.TestID;
                db.Problems.Add(p);
                
            }
            db.SaveChanges();
        }
    }
}