using DepartamentManager.Context;
using DepartamentManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DepartamentManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DepartamentController(AppDbContext context) {
            _context = context;
        }

        //get all departaments
        [HttpGet]
        public ActionResult GetAll()
        {
            var departaments = _context.Departament.ToList();
            return Ok(departaments);
        }

        //get departament by id
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var departament = _context.Departament.SingleOrDefault(d => d.id == id);
            if(departament == null) { return NotFound(); }

            return Ok(departament);
        }

        //creata a departament on database
        [HttpPost]
        public ActionResult Create([FromBody] Departament departament)
        {
            try
            {
                _context.Departament.Add(departament);
                _context.SaveChanges(); 
                return CreatedAtAction(nameof(GetById), new { id = departament.id }, departament);
            }catch(Exception ex) {
                return StatusCode(500, "Departamento duplicado!");
            }
        }

        //update departament´s data
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Departament input)
        {
            var departament = _context.Departament.SingleOrDefault(departament => departament.id == id);

            if (departament == null) { return NotFound(); }

            departament.Update(input.id, input.name, input.acronym);
            _context.SaveChanges();
            return NoContent();
        }

        //delete a departament
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var departament = _context.Departament.SingleOrDefault(departament => departament.id == id);

            if (departament == null) { return NotFound(); }
            _context.Departament.Remove(departament);

            var colaborators = _context.Colaborator.Where((c) => c.departamentId == departament.id).ToList();
            _context.Colaborator.RemoveRange(colaborators);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
