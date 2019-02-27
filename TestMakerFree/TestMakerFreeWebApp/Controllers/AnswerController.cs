using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestMakerFreeWebApp.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestMakerFreeWebApp.Controllers
{
    [Route("api/[controller]")]
    public class AnswerController : Controller
    {

        #region RESTful conventions methods
        /// <summary>
        /// Retrieves the answer with the given {id}
        /// </summary>
        /// <param name="id">The {id} of the answer to retrieve</param>
        /// <returns>The Answer with the given {id}</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Content("Not implemented yet!");
        }

        /// <summary>
        /// Add a new answer to the database.
        /// </summary>
        /// <param name="m">The view model containing the data to insert</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put(AnswerViewModel m)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Edit the answer with the given {id}
        /// </summary>
        /// <param name="m">The view model containing the data to update</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(AnswerViewModel m)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the answer with the given id from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        #endregion

        // GET api/answer/all
        [HttpGet("All/{questionId}")]
        public IActionResult All(int questionId)
        {
            var sampleAnswers = new List<AnswerViewModel>();

            sampleAnswers.Add(new AnswerViewModel()
            {
                Id = 1,
                QuestionId = questionId,
                Text = "Friends and family",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            });

            // add a bunch of other sample answers
            for (int i = 2; i <= 5; i++)
            {
                sampleAnswers.Add(new AnswerViewModel()
                {
                    Id = i,
                    QuestionId = questionId,
                    Text = String.Format("Sample Answer {0}", i),
                    CreatedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                });
            }

            // output result in JSON format
            return new JsonResult(
                sampleAnswers,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
    }
}
