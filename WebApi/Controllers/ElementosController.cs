using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Sevices;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElementosController : ControllerBase
    {
        public ElementoService _elementoService;
        public ElementosController(ElementoService elementoService)
        {
            _elementoService = elementoService;
        }

        [HttpGet]
        public ActionResult<List<Elementos>> Get()
        {            
            return _elementoService.Get();
        }

        [HttpPost]
        public ActionResult<Elementos> Create(Elementos element)
        {
            return _elementoService.Create(element);
        }

        [HttpPut]
        public ActionResult<Elementos> Update(Elementos element)
        {
             _elementoService.Update(element.Id, element);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Elementos> Delete(string id)
        {
            _elementoService.Delete(id);
            return Ok();
        }
    }
}
