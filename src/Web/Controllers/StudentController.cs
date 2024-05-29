using ConsultaAlumnos.Application.Interfaces;
using ConsultaAlumnos.Application.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsultaAlumnos.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult GetAll()
        { 
            return Ok(_studentService.GetAll());
        }

        [HttpGet("[action]")]
        public IActionResult GetFullData()
        {
            return Ok(_studentService.GetAllFullData());
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        { 
            try
            {
                return Ok(_studentService.GetById(id));
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] StudentCreateRequest studentCreateRequest)
        {
            var newObj = _studentService.Create(studentCreateRequest);
            return CreatedAtAction(nameof(GetById), new { id = newObj.Id }, newObj);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] StudentUpdateRequest studentUpdateRequest)
        {
            try
            {
                _studentService.Update(id, studentUpdateRequest);
                return NoContent();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _studentService.Delete(id);
                return NoContent();
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
