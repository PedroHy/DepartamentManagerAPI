using DepartamentManager.Context;
using DepartamentManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DepartamentManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColaboratorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ColaboratorController(AppDbContext context)
        {
            _context = context;
        }

        //get all colaborators
        [HttpGet]
        public IActionResult GetAll() { 
            var colaborators = _context.Colaborator.ToList();
            return Ok(colaborators);
        }

        //get colaborators of a departament
        [HttpGet("departament/{departamentId}")]
        public IActionResult GetbyDepartament(int departamentId)
        {
            var colaborators = _context.Colaborator.Where((c)=>c.departamentId == departamentId).ToList();
            return Ok(colaborators);
        }

        //get one colaborator by Id
        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            var colaborator = _context.Colaborator.SingleOrDefault(d => d.id == id);
            
            if(colaborator == null) { return NotFound(); }

            
            return Ok(colaborator);
        }

        //create a colaborator on database
        [HttpPost]
        public IActionResult Create([FromBody] Colaborator colaborator){
            var hasColaborator = _context.Colaborator.Where((c) => c.id == colaborator.id).ToList();

            if (hasColaborator.Count() > 0) { return BadRequest("Colaborator already exists"); }
            
            _context.Colaborator.Add(colaborator);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = colaborator.id }, colaborator);
        }

        //update colaborator´s data
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Colaborator input) {
            var colaborator = _context. Colaborator.SingleOrDefault(colaborator => colaborator.id == id);

            if (colaborator == null) { return NotFound(); }

            colaborator.Update(input.id, input.name, input.picture, input.rg, input.departamentId);
            _context.SaveChanges();
            return NoContent();
        }

        //delete a colaborator from database
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var colaborator = _context.Colaborator.SingleOrDefault(colaborator => colaborator.id == id);

            if (colaborator == null){ return NotFound(); }

            _context.Colaborator.Remove(colaborator);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
