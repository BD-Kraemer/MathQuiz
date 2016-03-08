using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MathQuiz.Models;
using Microsoft.AspNet.Identity;

namespace MathQuiz.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.User = User.Identity.GetUserName();
            }
            else
            {
                ViewBag.User = "Guest";
            }
                return View();
        }

        /// <summary>
        /// Finds all previous quizes taken by the user
        /// And returns them in a collection to the view
        /// Added by John C Rinker II 2/17/16
        /// </summary>
        /// <returns>Action Result View</returns>
        public ActionResult PastQuizes()
        {
            DbContextModel db = new DbContextModel();
            List<UserTest> uList = new List<UserTest>();
            string userName = "";
            
            if (Request.IsAuthenticated)
            {
                userName = User.Identity.GetUserName();
                uList = db.UserTests.Where(u => u.UserId == userName).ToList<UserTest>(); //ToList<UserTest>();
            }
            
            return View(uList);
        }

        /// <summary>
        /// Sedns a quiz object to the view to display all of the problems
        /// Added by John C Rinker II
        /// </summary>
        /// <param name="id">id of the user test record</param>
        /// <returns>action result view</returns>
        public ActionResult Details(int id)
        {
            DbContextModel db = new DbContextModel();
            UserTest uTest = new UserTest();

            uTest = db.UserTests.First(u => u.TestID == id);
            return View(uTest);
        }

    }
}