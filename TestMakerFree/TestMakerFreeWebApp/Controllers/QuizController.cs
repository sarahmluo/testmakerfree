using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Newtonsoft.Json;
using TestMakerFreeWebApp.ViewModels;
using TestMakerFreeWebApp.Data;
using Mapster;
using TestMakerFreeWebApp.Data.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestMakerFreeWebApp.Controllers
{
    [Route("api/[controller]")]
    public class QuizController : Controller
    {
        #region Private Fields
        private ApplicationDbContext DbContext;
        #endregion

        #region Constructor
        public QuizController(ApplicationDbContext context)
        {
            // Instantiate a DB context through DI
            DbContext = context;
        }
        #endregion

        #region RESTful conventions methods
        /// <summary>
        /// GET: api/quiz/{id}
        /// Retrieves the quiz with the given id
        /// </summary>
        /// <param name="id">The id of the quiz to retrieve</param>
        /// <returns>The quiz with the given id</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var quiz = DbContext.Quizzes.Where(i => i.Id == id).FirstOrDefault();

            // handle requests for non-existing quizzes
            if (quiz == null)
            {
                return NotFound(new
                {
                    Error = String.Format("Quiz ID {0} was not found", id)
                });
            }

            // Output result in JSON format
            return new JsonResult(
                quiz.Adapt<QuizViewModel>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        /// <summary>
        /// Edits the quiz with the given {id}
        /// </summary>
        /// <param name="model">The view model containing the data to insert</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody]QuizViewModel model)
        {
            // return generic 500 error  if payload is invalid
            if (model == null) { return new StatusCodeResult(500); }

            // retrieve quiz to edit
            var quizToEdit = DbContext.Quizzes.Where(u => u.Id == model.Id).FirstOrDefault();

            // handle requests asking for non-existing quizzes
            if (quizToEdit == null)
            {
                return NotFound(new
                {
                    Error = String.Format("Quiz ID {0} was not found", model.Id)
                });
            }

            // handle the update (without object-mapping)
            // Manually assigning properties we want to accept from the request
            quizToEdit.Title = model.Title;
            quizToEdit.Description = model.Description;
            quizToEdit.Text = model.Text;
            quizToEdit.Notes = model.Notes;

            quizToEdit.LastModifiedDate = DateTime.Now;

            // Save to DB
            DbContext.SaveChanges();

            // return updated quiz to the client
            return new JsonResult(quizToEdit.Adapt<QuizViewModel>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        /// <summary>
        /// Add a new quiz to the database.
        /// </summary>
        /// <param name="model">The view model containing the data to update</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody]QuizViewModel model)
        {
            // return generic 500 error  if payload is invalid
            if (model == null) { return new StatusCodeResult(500); }

            // handle the insert without object mapping
            var quiz = new Quiz();

            // properties taken from the request
            quiz.Title = model.Title;
            quiz.Description = model.Description;
            quiz.Text = model.Text;
            quiz.Notes = model.Notes;

            // properties set server-side
            quiz.CreatedDate = DateTime.Now;
            quiz.LastModifiedDate = quiz.CreatedDate;

            // set temporary author as admin id until login is set up
            quiz.UserId = DbContext.Users.Where(u => u.UserName == "Admin").FirstOrDefault().Id;

            // add the new quiz
            DbContext.Quizzes.Add(quiz);
            // persist changes in DB
            DbContext.SaveChanges();

            // return newly created quiz to client
            return new JsonResult(quiz.Adapt<QuizViewModel>(),
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });
        }

        /// <summary>
        /// Deletes the quiz with the given id from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // retrieve quiz from DB
            var quiz = DbContext.Quizzes.Where(i => i.Id == id).FirstOrDefault();

            // handle requests for non-existant quizzes
            if (quiz == null)
            {
                return new NotFoundObjectResult(new
                {
                    Error = String.Format("Quiz with ID {0} was not found", id)
                });
            }

            // Remove quiz from dbcontext
            DbContext.Quizzes.Remove(quiz);
            // persist changes to db
            DbContext.SaveChanges();

            // return http status ok
            return new OkResult();
        }

        #endregion

        #region Attribute-based routing methods
        /// <summary>
        /// GET api/quiz/latest
        /// Retrieves the {num} latest quizzes 
        /// </summary>
        /// <param name="num">The number of quizzes to retrieve</param>
        /// <returns>The {num} latest quizzes</returns>
        [HttpGet("Latest/{num?}")]
        public IActionResult Latest(int num = 10)
        {
            var latest = DbContext.Quizzes
                .OrderByDescending(q => q.CreatedDate)
                .Take(num)
                .ToArray();

            // output the result in JSON format
            return new JsonResult(
                latest.Adapt<QuizViewModel[]>(),
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
            var byTitle = DbContext.Quizzes
                .OrderBy(q => q.Title)
                .Take(num)
                .ToArray();

            return new JsonResult(
                byTitle.Adapt<QuizViewModel[]>(),
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
            var random = DbContext.Quizzes
                .OrderBy(q => Guid.NewGuid())
                .Take(num)
                .ToArray();

            return new JsonResult(
                random.Adapt<QuizViewModel[]>(),
                new JsonSerializerSettings() { Formatting = Formatting.Indented });
        }
        #endregion
    }
}
