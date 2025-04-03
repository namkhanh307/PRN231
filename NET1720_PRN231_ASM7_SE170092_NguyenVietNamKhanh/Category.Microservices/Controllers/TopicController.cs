using BusinessObject.Shared.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Topics.Microservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private List<Topic> Topic;

        public TopicController()
        {
            Topic = new List<Topic>()
            {
                   new Topic()
                {
                    Id = 1,
                    Name = "Good", 
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                },
                   new Topic()
                {
                    Id = 2,
                    Name = "Avg",
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                },
                   new Topic()
                {
                    Id = 3,
                    Name = "Bad",
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                },
            };
        }

        // GET: api/<TopicsController>
        [HttpGet]
        public IEnumerable<Topic> Get()
        {
            return Topic;
        }

        // GET api/<TopicsController>/5
        [HttpGet("{id}")]
        public Topic Get(int id)
        {
            return Topic.Find(x => x.Id == id);
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
