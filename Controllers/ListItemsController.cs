using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListItemsController : ControllerBase
    {
        private readonly ToDoListContext _context;

        public ListItemsController(ToDoListContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListItem>>> ListItems()
        {
            return await _context.ListItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ListItem>> GetListItem(int id)
        {
            var listItem = await _context.ListItems.FindAsync(id);

            if (listItem == null)
            {
                return NotFound();
            }

            return listItem;
        }

        [HttpGet("{id}/taskitems")]
        public async Task<List<TaskItem>> GetTaskItem(int id)
        {
            var listItem = await _context.TaskItems.Where(x => x.ListId == id).ToListAsync();
            return listItem;
        }


        [HttpPost]
        public async Task<ActionResult<ListItem>> PostListItem(ListItem listItem)
        {
            _context.ListItems.Add(listItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetListItem", new { id = listItem.Id }, listItem);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ListItem>> DeleteListItem(int id)
        {
            var listItem = await _context.ListItems.FindAsync(id);
            _context.ListItems.Remove(listItem);
            await _context.SaveChangesAsync();

            return listItem;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskItem(int id, ListItem listItem)
        {
            if (id != listItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(listItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
