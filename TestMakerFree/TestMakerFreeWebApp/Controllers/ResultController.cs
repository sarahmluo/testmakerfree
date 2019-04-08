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
    public class ResultController : BaseApiController
    {
        #region Constructor
        public ResultController(ApplicationDbContext context)
            : base(context) { }
        #endregion

        #region RESTful conventions methods
        /// <summary>
        /// Retrieves the result with the given {id}
        /// </summary>
        /// <param name="id">The {id} of the result to retrieve</param>
        /// <returns>The Result with the given {id}</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = DbContext.Results.Where(i => i.Id == id).FirstOrDefault();

            // handle request asking for non-existing results
            if (result == null)
            {
                return NotFound(new
                {
                    Error = String.Format("Result ID {0} was not found", id)
                });
            }

            return new JsonResult(
                result.Adapt<ResultViewModel>(),
                JsonSettings);
        }

        /// <summary>
        /// Edit result with given id
        /// </summary>
        /// <param name="m">The view model containing the data to insert</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody] ResultViewModel model)
        {
            if (model == null) return new StatusCodeResult(500);

            // retrieve the result to edit
            var result = DbContext.Results.Where(r => r.Id == model.Id).FirstOrDefault();

            if (result == null)
            {
                return NotFound(new
                {
                    Error = String.Format("Result ID {0} was not found", model.Id)
                });
            }

            // handle the update
            result.QuizId = model.QuizId;
            result.Text = model.Text;
            result.MinValue = model.MinValue;
            result.MaxValue = model.MaxValue;
            result.Notes = model.Notes;

            result.LastModifiedDate = DateTime.Now;

            // save changes
            DbContext.SaveChanges();

            return new JsonResult(result.Adapt<ResultViewModel>(),
                JsonSettings);
        }

        /// <summary>
        /// Add a new result to the database
        /// </summary>
        /// <param name="m">The view model containing the data to update</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] ResultViewModel model)
        {
            if (model == null) return new StatusCodeResult(500);

            // map the viewmodel to the model
            var result = model.Adapt<Result>();

            // override props that should be set server-side
            result.CreatedDate = DateTime.Now;
            result.LastModifiedDate = result.CreatedDate;

            // add the the new result
            DbContext.Results.Add(result);
            DbContext.SaveChanges();

            // return newly created result object
            return new JsonResult(result.Adapt<ResultViewModel>(),
                JsonSettings);
        }

        /// <summary>
        /// Deletes the answer with the given id from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = DbContext.Results.Where(r => r.Id == id).FirstOrDefault();

            if (result == null)
            {
                return NotFound(new
                {
                    Error = String.Format("Result {0} was not found", id)
                });
            }

            DbContext.Results.RemoveRange(result);
            DbContext.SaveChanges();

            return new JsonResult(result.Adapt<ResultViewModel>(),
               JsonSettings);
        }

        #endregion

        // GET api/result/all
        [HttpGet("All/{quizId}")]
        public IActionResult All(int quizId)
        {
            var results = DbContext.Results
                .Where(q => q.QuizId == quizId)
                .ToArray();

            // output the result in JSON format
            return new JsonResult(
                results.Adapt<ResultViewModel[]>(),
                JsonSettings);
        }
    }
}
