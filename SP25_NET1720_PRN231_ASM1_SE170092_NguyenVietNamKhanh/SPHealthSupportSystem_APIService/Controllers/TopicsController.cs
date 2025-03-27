using Microsoft.AspNetCore.Mvc;
using SPHealthSupportSystem_Repositories.Models;
using SPHealthSupportSystem_Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPHealthSupportSystem_APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private ITopicService _topicService;
        public TopicsController(ITopicService topicService)
        {
            _topicService = topicService;
        }
        // GET: api/<TopicsController>
        [HttpGet]
        public async Task<IEnumerable<Topic>> Get()
        {
            return await _topicService.GetAll();
        }

        // GET api/<TopicsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TopicsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TopicsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TopicsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
