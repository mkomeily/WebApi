using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_01.Models;

namespace WebApi_01.Controllers
{
    [Produces("application/json")]
    [Route("api/ToDo")]
    public class ToDoController : Controller
    {
        public ToDoController(IToDoRepository todoitems)
        {
            ToDoItems = todoitems;
        }
        public IToDoRepository ToDoItems { get; set; }

        [HttpGet]
        public IEnumerable<ToDoItem> GetAll()
        {
            return ToDoItems.GetAll();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public ActionResult GetById(string id)
        {
            var item = ToDoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ToDoItem item)
        {
            if (item==null)
            {
                return BadRequest();
            }
            ToDoItems.Add(item);
            return CreatedAtRoute("GetToDo", new { id = item.Key }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] ToDoItem item)
        {
            if (item==null || item.Key != id)            
            {
                return BadRequest();
            }
            ToDoItems.Update(item);
            return new NoContentResult();
        }

        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] ToDoItem item,string id)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var todo = ToDoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }
            item.Key = todo.Key;
            ToDoItems.Update(item);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var todo = ToDoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }
            ToDoItems.Remove(todo.Key); 
            return new NoContentResult();
        }
    }
}