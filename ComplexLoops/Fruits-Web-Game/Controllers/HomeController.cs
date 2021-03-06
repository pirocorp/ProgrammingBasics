﻿using System;
using System.Web.Mvc;

namespace Fruits_Web_Game.Controllers
{
    public class HomeController : Controller
    {
        static int rowsCount = 3;
        static int colsCount = 9;
        static string[,] fruits = GenerateRandomFruits();
        static int score = 0;
        static bool gameOver = false;

        public ActionResult Index()
        {
            ViewBag.rowsCount = rowsCount;
            ViewBag.colsCount = colsCount;
            ViewBag.fruits = fruits;
            ViewBag.score = score;
            ViewBag.gameOver = gameOver;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        static string[,] GenerateRandomFruits()
        {
            var rand = new Random();
            fruits = new string[rowsCount, colsCount];
            for (int row = 0; row < rowsCount; row++)
            {
                for (int col = 0; col < colsCount; col++)
                {
                    var r = rand.Next(9);
                    if (r < 4) fruits[row, col] = "apple";
                    else if (r < 5) fruits[row, col] = "banana";
                    else if (r < 6) fruits[row, col] = "orange";
                    else if (r < 8) fruits[row, col] = "kiwi";
                    else fruits[row, col] = "dynamite";
                }
            }
            return fruits;
        }

        public ActionResult Reset()
        {
            fruits = GenerateRandomFruits();
            score = 0;
            gameOver = false;
            return RedirectToAction("Index");
        }

        public ActionResult FireTop(int position)
        {
            return Fire(position, 0, 1);
        }

        public ActionResult FireBottom(int position)
        {
            return Fire(position, rowsCount - 1, -1);
        }

        private ActionResult Fire(int position, int startRow, int step)
        {
            bool empty = true;
            var col = position * (colsCount - 1) / 100;
            var row = startRow;
            while (row >= 0 && row < rowsCount)
            {
                var fruit = fruits[row, col];
                if (fruit == "apple" || fruit == "banana" || fruit == "orange" || fruit == "kiwi")
                {
                    switch (fruit)
                    {
                        case "apple": score += 1; break;
                        case "banana": score += 4; break;
                        case "orange": score += 4; break;
                        case "kiwi": score += 2; break;

                    }
                    //score += 10; //TODO: give different scores for different fruits
                    empty = false;
                    fruits[row, col] = "empty";
                    break;
                }
                else if (fruit == "dynamite")
                {
                    gameOver = true;
                    break;
                }
                row = row + step;
            }

            if (empty)
            {
                score -= 3;
            }
            if (score < 0)
            {
                gameOver = true;                
            }

            return RedirectToAction("Index");
        }
    }
}