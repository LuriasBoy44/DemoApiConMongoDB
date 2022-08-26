using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Request;
using WebApi.Sevices;
using WebApi.Response;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userservice)
        {
            _userService = userservice;
        }
        [HttpPost("Login")]
        public IActionResult Autenticar([FromBody] AuthRequest model)
        {
            Respuesta res = new Respuesta();
            var userserponse = _userService.Auth(model);

            if (userserponse == null)
            {
                res.Exito = 0;
                res.Mensaje = "Usuario o password Incorrecta";
                return BadRequest(res);
            }
            else
            {
                res.Exito = 1;
                res.Data = userserponse;
                return Ok(res);

            }
        }
    }
}
