using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Newtonsoft.Json;
using TestMakerFreeWebApp.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestMakerFreeWebApp.Controllers
{
    [Route("api/[controller]")]
    public class QuizController : Controller
    {
       // GET api/quiz/latest
       [HttpGet("Latest/{num}")]
       public IActionResult Latest(int num = 10)
        {
            var sampleQuizzes = new List<QuizViewModel>();

            // add a first sample quiz
            sampleQuizzes.Add(new QuizViewModel()
            {
                Id = 1,
                Title = "Which Office character are You?",
                Description = "Sitcom-related presonality test",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            });

            // add other sample quizzes
            for (int i = 2; i <= num; i++)
            {
                sampleQuizzes.Add(new QuizViewModel()
                {
                    Id = i,
                    Title = String.Format("Sample Quiz {0}", i),
                    Description = "This is a sample quiz",
                    CreatedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                });
            }

            // output the result in JSON format
            return new JsonResult(
                sampleQuizzes,
                new JsonSerializerSettings() { Formatting = Formatting.Indented });
        }

        /// <summary>
        /// GET: api/quiz/ByTitle
        /// Retrieves the {num} Quizzes sorted by Title (A to Z)
        /// (num parameter in the route must be an int and is optional)
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        [HttpGet("ByTitle/{num:int?}")]
        public IActionResult ByTitle(int num = 10)
        {
            var sampleQuizzes = ((JsonResult)Latest(num)).Value as List<QuizViewModel>;

            return new JsonResult(
                sampleQuizzes.OrderBy(t => t.Title),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        /// <summary>
        /// GET: api/quiz/mostViewed
        /// Retrieves the {num} random Quizzes
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        [HttpGet("Random/{num:int?}")]
        public IActionResult Random(int num = 10)
        {
            var sampleQuizzes = ((JsonResult)Latest(num)).Value as List<QuizViewModel>;

            return new JsonResult(
                sampleQuizzes.OrderBy(t => Guid.NewGuid()),
                new JsonSerializerSettings() { Formatting = Formatting.Indented });
        }
    }
}
