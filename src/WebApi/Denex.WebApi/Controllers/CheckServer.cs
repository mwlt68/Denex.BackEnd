using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Denex.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckServer : ControllerBase
    {

        [HttpGet]
        public IActionResult GetMessage()
        {
            return Ok("Denex service is running");
        }
    }
}