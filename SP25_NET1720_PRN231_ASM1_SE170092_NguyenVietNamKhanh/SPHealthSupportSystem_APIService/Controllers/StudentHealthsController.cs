using Microsoft.AspNetCore.Mvc;
using SPHealthSupportSystem_Repositories.Models;
using SPHealthSupportSystem_Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPHealthSupportSystem_APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentHealthsController : ControllerBase
    {
        private readonly IStudentHealthService _studentHealthService;
        public StudentHealthsController(IStudentHealthService studentHealthService)
        {
            _studentHealthService = studentHealthService;
        }
        // GET: api/<StudentHealthsController>
        [HttpGet]
        public async Task<IEnumerable<StudentHealth>> Get()
        {
            return await _studentHealthService.GetAll();
        }

        // GET api/<StudentHealthsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StudentHealthsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StudentHealthsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StudentHealthsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
