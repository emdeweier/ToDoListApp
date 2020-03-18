using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoListAppAPI.Services.Interfaces;
using ToDoListAppData.ViewModel;

namespace ToDoListAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListsController : ControllerBase
    {
        private readonly IToDoListService _toDoListService;
        public ToDoListsController(IToDoListService toDoListService)
        {
            _toDoListService = toDoListService;
        }

        // GET: api/ToDoLists
        [HttpGet]
        public IActionResult Get()
        {
            var getall = _toDoListService.Get();
            if (getall == null)
            {
                return NotFound();
            }
            return Ok(getall);
        }

        // GET: api/ToDoLists
        [HttpGet("Users/{iduser}")]
        public IActionResult Get(string iduser)
        {
            var getall = _toDoListService.Get(iduser);
            if (getall == null)
            {
                return NotFound();
            }
            return Ok(getall);
        }

        // GET: api/ToDoLists/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var getbyid = _toDoListService.Get(id);
            if (getbyid == null)
            {
                return NotFound();
            }
            return Ok(getbyid);
        }

        // POST: api/ToDoLists
        [HttpPost]
        public ActionResult Post(ToDoListVM toDoListVM)
        {
            var create = _toDoListService.Add(toDoListVM);
            if(create > 0)
            {
                return Ok(toDoListVM);
            }
            return BadRequest();
        }

        // PUT: api/ToDoLists/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, ToDoListVM toDoListVM)
        {
            var put = _toDoListService.Edit(id, toDoListVM);
            if (put > 0)
            {
                return Ok(toDoListVM);
            }
            return BadRequest();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var delete = _toDoListService.Delete(id);
            if (delete == true)
            {
                return Ok(delete);
            }
            return BadRequest();
        }

        [HttpPut("Status/{id}")]
        public ActionResult UpdateStatus(int id, ToDoListVM toDoListVM)
        {
            var updatestatus = _toDoListService.UpdateStatus(id, toDoListVM);
            if (updatestatus == true)
            {
                return Ok(updatestatus);
            }
            return BadRequest();
        }
    }
}
