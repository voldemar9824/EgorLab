using EgorLab.Models;
using EgorLab.Models.StorageModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgorLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PersonController : ControllerBase
    {
        private readonly IStorage<Person> storage;

        public PersonController(IStorage<Person> storage)
        {
            this.storage = storage;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get()
        {
            return Ok(storage.All);
        }

        [HttpGet("{id}")]
        public ActionResult<Person> Get(Guid id)
        {
            if (storage.Has(id))
            {
                return Ok(storage[id]);
            }
            return NotFound("No such");
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person personModel)
        {
            var validationResult = personModel.Validate();
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            storage.Add(personModel);

            return Ok($"{personModel.ToString()} has been added");
        }

        [HttpPut]
        public IActionResult Put([FromBody] Person personModel)
        {
            if (storage.Has(personModel.Id))
            {
                var validationResult = personModel.Validate();
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
              
                storage[personModel.Id] = personModel;

                return Ok($"Person has been updated to {personModel.ToString()}");
            }
            return NotFound("No such");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (storage.Has(id))
            {
                storage.RemoveAt(id);
                return Ok($"Person with id={id} has been removed");
            }
            return NotFound("No such");
        }
    }
}
