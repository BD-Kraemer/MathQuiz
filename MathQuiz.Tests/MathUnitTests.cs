/*
*
*  Author: Brian Kraemer
*
*/

using System;
using System.Collections.Generic;
using MathQuiz.Models.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathQuiz.Models;

namespace MathQuiz.Tests
{
    [TestClass]
    public class MathUnitTests
    {
        [TestMethod]
        public void AdditionTest()
        {
            //Aranage
            ProblemFactory factory = new ProblemFactory();
            factory.Difficulty = Difficulty.Easy;
            factory.AddProblemType(new Addition());

            //Act
            Problem problem = factory.GetProblem();
            int answer = problem.Value1 + problem.Value2;

            //Assert
            //Verify we get a + opearand
            Assert.IsTrue(problem.Operand == '+');
            //Make sure our calculated answer matches the answer provided.
            Assert.IsTrue(answer == problem.Answer);
        }

        [TestMethod]
        public void SubtractionTest()
        {
            //Aranage
            ProblemFactory factory = new ProblemFactory();
            factory.Difficulty = Difficulty.Easy;
            factory.AddProblemType(new Subtraction());

            //Act
            Problem problem = factory.GetProblem();
            int answer = problem.Value1 - problem.Value2;

            //Assert
            //Verify we get a - opearand
            Assert.IsTrue(problem.Operand == '-');
            //Make sure our calculated answer matches the answer provided.
            Assert.IsTrue(answer == problem.Answer);
        }

        [TestMethod]
        public void SubtractionTestDoesNotHaveNegativeAnswers()
        {
            //Arrange
            bool answersArePositive = true;

            ProblemFactory factory = new ProblemFactory();
            factory.Difficulty = Difficulty.Easy;
            factory.AddProblemType(new Subtraction());

            //Act
            for (int i = 0; i < 100; i++)
            {
                var problem = factory.GetProblem();
                if (problem.Value2 > problem.Value1)
                {
                    answersArePositive = false;
                    break;
                }
            }

            //Assert
            Assert.IsTrue(answersArePositive);
        }

        [TestMethod]
        public void MultiplicationTest()
        {
            //Aranage
            ProblemFactory factory = new ProblemFactory();
            factory.Difficulty = Difficulty.Easy;
            factory.AddProblemType(new Multiplication());

            //Act
            Problem problem = factory.GetProblem();
            int answer = problem.Value1 * problem.Value2;

            //Assert
            //Verify we get a * opearand
            Assert.IsTrue(problem.Operand == '*');
            //Make sure our calculated answer matches the answer provided.
            Assert.IsTrue(answer == problem.Answer);
        }

        [TestMethod]
        public void DivisionTest()
        {
            //Aranage
            ProblemFactory factory = new ProblemFactory();
            factory.Difficulty = Difficulty.Easy;
            factory.AddProblemType(new Division());

            //Act
            Problem problem = factory.GetProblem();
            int answer = problem.Value1 / problem.Value2;

            //Assert
            //Verify we get a / opearand
            Assert.IsTrue(problem.Operand == '/');
            //Make sure our calculated answer matches the answer provided.
            Assert.IsTrue(answer == problem.Answer);
            //Make sure that answer results in a whole number value.
            Assert.AreEqual(0, problem.Value1 % problem.Value2);
        }

        [TestMethod]
        public void AllArithmeticTypesAccountedFor()
        {
            //Verify that if we add Addition, Multiplication, Division and Subtraction
            //To the factory that we get at least one type of each problem after producing 1000 problems
            //It's near impossible to not get at least one of each after producing 1000 problems.

            //Arrange
            ProblemFactory factory = new ProblemFactory();
            factory.AddProblemType(new Addition());
            factory.AddProblemType(new Subtraction());
            factory.AddProblemType(new Multiplication());
            factory.AddProblemType(new Division());

            bool hasAddition = false;
            bool hasSubtraction = false;
            bool hasDivision = false;
            bool hasMultiplication = false;
            bool allTypesAccountedFor = true;
            int counter = 0;

            //Act
            while (!hasAddition || !hasSubtraction || !hasDivision || !hasMultiplication)
            {              
                    var problem = factory.GetProblem();

                    if (problem.Operand == '+')
                    {
                        hasAddition = true;
                    }
                    else if (problem.Operand == '-')
                    {
                        hasSubtraction = true;
                    }
                    else if (problem.Operand == '/')
                    {
                        hasDivision = true;
                    }
                    else if (problem.Operand == '*')
                    {
                         hasMultiplication = true;
                    }

                counter++;
                if (counter > 1000)
                {
                    allTypesAccountedFor = false;
                    break;
                }
            }
            //Assert          
            Assert.IsTrue(allTypesAccountedFor);
        }

    }
}
