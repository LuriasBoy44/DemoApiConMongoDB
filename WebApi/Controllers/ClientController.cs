using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]// se agrega para utilizar el JWT
    public class ClientController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            List<string> list = new List<string>();

            list.Add("Claudio");
            list.Add("Luna");
            list.Add("Azoka");
            list.Add("Leia");

            return Ok(list);
        }
    }
}
