using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Data;
using DatingApp.API.Models;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext dataContext;

        public ValuesController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var values = await dataContext.Values.ToListAsync();
            return Ok(values);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var value = await dataContext.Values.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(value);
        }

        [HttpGet("byName")]
        public IActionResult GetByName(string name)
        {
            var value = dataContext.Values.FirstOrDefault(x => x.Name == name);
            return Ok(value);
        }

        // POST api/values
        [HttpPost]
        public IActionResult UpdateValue(int id, [FromQuery] string name)
        {
            if (id > 0)
            {
                var value = dataContext.Values.FirstOrDefault(x => x.Id == id);
                value.Name = name;
                dataContext.SaveChanges();
                return Ok(value);
            }
            return Ok("value not found");
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult NewValue([FromQuery] string name)
        {
            var newValue = new Value { Name = name };
            dataContext.Values.Add(newValue);
            dataContext.SaveChanges();

            return Ok(newValue);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
