using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestMakerFreeWebApp.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestMakerFreeWebApp.Controllers
{
    [Route("api/[controller]")]
    public class BaseApiController : Controller
    {
        #region constructor
        public BaseApiController(ApplicationDbContext context)
        {
            DbContext = context;

            JsonSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            };
        }
        #endregion

        #region Shared properties
        protected ApplicationDbContext DbContext { get; private set; }
        protected JsonSerializerSettings JsonSettings { get; private set; }
        #endregion
    }
}
