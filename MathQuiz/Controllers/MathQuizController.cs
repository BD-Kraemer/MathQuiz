/*  Math Quiz Controller 
*   John C Rinker II
*   2/10/2016
*   This is the controller class that handles:
        - Creating and updateing the database tables of UserTest and Problem
        - Serving the views to the user
        - Accepting the user response  
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MathQuiz.Models;
using MathQuiz.Models.Math;
using Microsoft.AspNet.Identity;

namespace MathQuiz.Controllers
{
    public class MathQuizController : Controller
    {
        // GET: MathQuiz
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Create test and problems, store test and problems in data base, send first problem to nextproblem view
        /// </summary>
        /// <returns>Problem to NextProblem view</returns>
        [HttpPost]
        public ActionResult CreateTest()
        {
            int diff = 0;
            ProblemFactory factory = new ProblemFactory();
            DbContextModel db = new DbContextModel();
            UserTest ut;// = new UserTest();

            // create userTest with authenticated user or "guest"
            if (Request.IsAuthenticated)
            {
                ut = new UserTest(User.Identity.GetUserName());
            }
            else
            {
                ut = new UserTest("Guest");
            }

            // Get difficulty and problem types
            diff = Convert.ToInt32(Request["Difficulty"]);

            string add = "";
            add = Request["Addition"];
            string sub = "";
            sub = Request["Subtraction"];
            string mul = "";
            mul = Request["Multiplication"];
            string div = "";
            div = Request["Division"];

            string skillString = "";

            if (add != "" && add != null)
            {
                factory.AddProblemType(new Addition());
                skillString += "Addition:";
            }
            if (sub != "" && sub != null)
            {
                factory.AddProblemType(new Subtraction());
                skillString += "Subtraction:";
            }
            if (mul != "" && mul != null)
            {
                factory.AddProblemType(new Multiplication());
                skillString += "Multiplication:";
            }
            if (div != "" && div != null)
            {
                factory.AddProblemType(new Division());
                skillString += "Division:";
            }

            skillString = skillString.TrimEnd(':');

            // set difficulty 
            switch (diff)
            {
                case 1:
                    factory.Difficulty = Difficulty.Easy;
                    ut.Difficulty = 1;
                    break;
                case 2:
                    factory.Difficulty = Difficulty.Medium;
                    ut.Difficulty = 2;
                    break;
                case 3:
                    factory.Difficulty = Difficulty.Hard;
                    ut.Difficulty = 3;
                    break;
                default:
                    factory.Difficulty = Difficulty.Easy;
                    ut.Difficulty = 1;
                    break;
            }

            db.UserTests.Add(ut);
            db.SaveChanges();


            ut.DateCreated = DateTime.Now;
            ut.Skills = skillString;

            // save changes to database
            db.UserTests.Attach(ut);
            db.Entry(ut).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            // fill user test with problems from problem factory and save to database
            for (int i = 1; i < 16; i++)
            {
                ut.Problems.Add(factory.GetProblem(i));
            }

            ut.AddProblemsToDb();

            // return first problem to NextProblem view
            return View("NextProblem", ut.Problems.ToList()[0]);
        }

        public ActionResult NextProblem(Problem p)
        {
            return View(p);
        }

        /// <summary>
        /// Gets answer from user form submission and displays result
        /// </summary>
        /// <returns>Problem to DisplayAnswer View</returns>
        [HttpPost]
        public ActionResult SubmitAnswer()
        {
            DbContextModel db = new DbContextModel();
            int testId = 0;
            decimal answer = 0;
            int seq = 1;

            // Get form data
            testId = Convert.ToInt32(Request["utId"]);
            answer = Convert.ToDecimal(Request["UserAnswer"]);
            seq = Convert.ToInt32(Request["_seq"]);

            // find test in database
            UserTest uTest = db.UserTests.First(u => u.TestID == testId);
            Problem p = uTest.Problems.ToList<Problem>()[seq - 1];

            // save user answer
            p.UserAnswer = answer;
            db.SaveChanges();

            // return Problem to DisplayAnswer View
            return View("DisplayAnswer", p);
        }

        /// <summary>
        ///  Send Problem to Display Answer View
        /// </summary>
        /// <param name="p">Problem</param>
        /// <returns>View</returns>
        public ActionResult DisplayAnswer(Problem p)
        {
            return View(p);
        }

        /// <summary>
        /// Get next problem or show test results
        /// </summary>
        /// <returns>View</returns>
        [HttpPost]
        public ActionResult NextQuestion()
        {
            DbContextModel db = new DbContextModel();
            int seq = 0;
            int utId = 0;
            // Get Test ID and sequence number
            seq = Convert.ToInt32(Request["_seq"]);
            utId = Convert.ToInt32(Request["utId"]);
            UserTest utest = db.UserTests.First(u => u.TestID == utId);
            if (seq == 15)
            {
                // no more problems calulate score and return userTest to DisplayResults View
                decimal score = 0;
                int correctCount = 0;
                foreach (Problem p in utest.Problems.ToList<Problem>())
                {
                    if (p.CorrectAnswer == p.UserAnswer)
                        correctCount++;
                }
                score = (decimal)(correctCount / 15.0);
                utest.Score = score;
                db.UserTests.Attach(utest);
                db.Entry(utest).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return View("DisplayResults", utest);
            }
            else
            {
                //Get next problem and reutn to NextProblem View
                Problem p = utest.Problems.ToList<Problem>()[seq];
                return View("NextProblem", p);
            }
        }

        // Display Results View
        public ActionResult DisplayResults(UserTest ut)
        {
            return View(ut);
        }
    }
}