using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMakerFreeWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TestMakerFreeWebApp.Controllers
{
    [Route("api/[controller]")]
    public class QuestionController : Controller
    {
        #region RESTful conventions methods
        /// <summary>
        /// Retrieves the question with the given {id}
        /// </summary>
        /// <param name="id">The {id} of the question to retrieve</param>
        /// <returns>The Question with the given {id}</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Content("Not implemented yet!");
        }

        /// <summary>
        /// Add a new question to the database.
        /// </summary>
        /// <param name="m">The view model containing the data to insert</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put(QuestionViewModel m)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Edit the question with the given {id}
        /// </summary>
        /// <param name="m">The view model containing the data to update</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(QuestionViewModel m)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the question with the given id from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        #endregion

        // GET api/question/all
        [HttpGet("All/{quizId}")]
       public IActionResult All(int quizId)
        {
            var sampleQuestions = new List<QuestionViewModel>();

            // add a first sample question
            sampleQuestions.Add(new QuestionViewModel()
            {
                Id = 1,
                QuizId = quizId,
                Text = "What do you value most in your life?",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            });

            // add a bunch of other sample questions
            for (int i = 2; i <= 5; i++)
            {
                sampleQuestions.Add(new QuestionViewModel()
                {
                    Id = i,
                    QuizId = quizId,
                    Text = String.Format("Sample Question {0}", i),
                    CreatedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                });
            }

            // output the result in JSON format
            return new JsonResult(
                sampleQuestions,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
    }
}
