using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestMakerFreeWebApp.ViewModels;
using TestMakerFreeWebApp.Data;
using Mapster;
using TestMakerFreeWebApp.Data.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestMakerFreeWebApp.Controllers
{
    [Route("api/[controller]")]
    public class AnswerController : Controller
    {
        #region Private Fields
        private ApplicationDbContext DbContext;
        #endregion

        #region Constructor
        public AnswerController(ApplicationDbContext context)
        {
            // Instantiate a DB context through DI
            DbContext = context;
        }
        #endregion

        #region RESTful conventions methods
        /// <summary>
        /// Retrieves the answer with the given {id}
        /// </summary>
        /// <param name="id">The {id} of the answer to retrieve</param>
        /// <returns>The Answer with the given {id}</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var answer = DbContext.Answers.Where(i => i.Id == id).FirstOrDefault();

            // handle requests asking for non-existing answers
            if (answer == null)
            {
                return NotFound(new
                {
                    Error = String.Format("Answer {0} was not found", id)
                });
            }

            return new JsonResult(
                answer.Adapt<AnswerViewModel>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        /// <summary>
        /// Edit an answer with the given ID
        /// </summary>
        /// <param name="m">The view model containing the data to insert</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody]AnswerViewModel model)
        {
            // Return 500 error if invalid payload.
            if (model == null)
            {
                return new StatusCodeResult(500);
            }

            // Retrieve the answer to edit
            var answer = DbContext.Answers.Where(i => i.Id == model.Id).FirstOrDefault();

            // If the answer doesn't exist
            if (answer == null)
            {
                return NotFound(new
                {
                    Error = String.Format("Answer {0} was not found", model.Id)
                });
            }

            // handle update by only accepting properties we want to
            answer.QuestionId = model.QuestionId;
            answer.Text = model.Text;
            answer.Value = model.Value;
            answer.Notes = model.Notes;

            // properties set server side
            answer.LastModifiedDate = DateTime.Now;

            // save changes
            DbContext.SaveChanges();

            return new JsonResult(answer.Adapt<AnswerViewModel>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        /// <summary>
        /// Adds a new answer to the database
        /// </summary>
        /// <param name="m">The view model containing the data to update</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody]AnswerViewModel model)
        {
            // Return 500 error if invalid payload.
            if (model == null)
            {
                return new StatusCodeResult(500);
            }

            // map the Viewmodel to the model
            var answer = model.Adapt<Answer>();

            // override properties that should be set on the server side only
            answer.QuestionId = model.QuestionId;
            answer.Text = model.Text;
            answer.Notes = model.Notes;

            // properties set Server side
            answer.CreatedDate = DateTime.Now;
            answer.LastModifiedDate = answer.CreatedDate;

            // add the new amswer
            DbContext.Answers.Add(answer);
            // save the changes
            DbContext.SaveChanges();

            // return newly created answer to the client
            return new JsonResult(answer.Adapt<AnswerViewModel>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        /// <summary>
        /// Deletes the answer with the given id from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Retrieve the answer to delete
            var answer = DbContext.Answers.Where(i => i.Id == id).FirstOrDefault();

            // If the answer doesn't exist
            if (answer == null)
            {
                return NotFound(new
                {
                    Error = String.Format("Answer {0} was not found", id)
                });
            }

            // remove from dbcontext
            DbContext.Answers.Remove(answer);
            // save changes
            DbContext.SaveChanges();

            // because front end expects json object in return
            return new JsonResult(answer.Adapt<AnswerViewModel>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        #endregion

        // GET api/answer/all
        [HttpGet("All/{questionId}")]
        public IActionResult All(int questionId)
        {
            var answers = DbContext.Answers
                .Where(q => q.QuestionId == questionId)
                .ToArray();

            // output result in JSON format
            return new JsonResult(
                answers.Adapt<AnswerViewModel[]>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
    }
}
