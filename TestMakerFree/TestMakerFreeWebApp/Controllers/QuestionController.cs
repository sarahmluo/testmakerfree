using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMakerFreeWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestMakerFreeWebApp.Data;
using Mapster;
using TestMakerFreeWebApp.Data.Models;

namespace TestMakerFreeWebApp.Controllers
{
    public class QuestionController : BaseApiController
    {
        #region Constructor
        public QuestionController(ApplicationDbContext context)
         : base(context) { }
        #endregion

        #region RESTful conventions methods
        /// <summary>
        /// Retrieves the question with the given {id}
        /// </summary>
        /// <param name="id">The {id} of the question to retrieve</param>
        /// <returns>The Question with the given {id}</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var question = DbContext.Questions.Where(i => i.Id == id).FirstOrDefault();

            // if the question doesn't exist...
            if (question == null)
            {
                return NotFound(new
                {
                    Error = String.Format("Question ID {0} was not found", id)
                });
            }

            return new JsonResult(
                question.Adapt<QuestionViewModel>(),
                JsonSettings);
        }

        /// <summary>
        /// Edit a new question to the database.
        /// </summary>
        /// <param name="m">The view model containing the data to insert</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody] QuestionViewModel model)
        {
            // return 500 error if payload is invalid
            if (model == null) return new StatusCodeResult(500);

            // retrieve question to edit.
            var question = DbContext.Questions.Where(q => q.Id == model.Id).FirstOrDefault();

            // handle requests asking for non-existing questions
            if (question == null)
            {
                return NotFound(new
                {
                    Error = String.Format("Question ID {0} was not found.", model.Id)
                });
            }

            // handle the update by manually assigning properties we want 
            // to accept from the front end
            question.QuizId = model.QuizId;
            question.Text = model.Text;
            question.Notes = model.Notes;

            // properties set from server side
            question.LastModifiedDate = DateTime.Now;

            // persist changes
            DbContext.SaveChanges();

            // return updated quiz to the client
            return new JsonResult(question.Adapt<QuestionViewModel>(),
                JsonSettings);
        }

        /// <summary>
        /// Add the question with the given {id}
        /// </summary>
        /// <param name="m">The view model containing the data to update</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody]QuestionViewModel model)
        {
            // return 500 error if payload is invalid
            if (model == null) return new StatusCodeResult(500);

            // map the ViewModel to the model
            var question = model.Adapt<Question>();

            // set values from front end
            question.QuizId = model.QuizId;
            question.Text = model.Text;
            question.Notes = model.Notes;

            // properties set from server-side
            question.CreatedDate = DateTime.Now;
            question.LastModifiedDate = question.CreatedDate;

            // add the new question
            DbContext.Questions.Add(question);
            // persist changes into the db
            DbContext.SaveChanges();

            // return newly created question to the client
            return new JsonResult(question.Adapt<QuestionViewModel>(),
                JsonSettings);
        }

        /// <summary>
        /// Deletes the question with the given id from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // retrieve the question from the database
            var question = DbContext.Questions.Where(i => i.Id == id).FirstOrDefault();

            // handle requests asking for non-existing questions
            if (question == null)
            {
                return NotFound(new
                {
                    Error = String.Format("Question ID {0} was not found", id)
                });
            }

            // Remove the question from the DbCOntext
            DbContext.Questions.Remove(question);

            // Delete all answers associated with this question
            var answers = DbContext.Answers.Where(a => a.QuestionId == id).ToArray();
            foreach (var answer in answers)
            {
                DbContext.Answers.Remove(answer);
            }

            // persist changes in the DB
            DbContext.SaveChanges();

            // return deleted question since client expects a JSON Result
            return new JsonResult(question.Adapt<QuestionViewModel>(),
                JsonSettings);
        }

        #endregion

        // GET api/question/all
        [HttpGet("All/{quizId}")]
       public IActionResult All(int quizId)
        {
            var questions = DbContext.Questions.Where(q => q.QuizId == quizId).ToArray();

            // output the result in JSON format
            return new JsonResult(
                questions.Adapt<QuestionViewModel[]>(),
                JsonSettings);
        }
    }
}
