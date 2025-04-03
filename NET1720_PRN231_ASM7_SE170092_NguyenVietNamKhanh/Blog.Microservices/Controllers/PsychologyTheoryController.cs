using BusinessObject.Shared.Models;
using Common.Shared;
using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PsychologyTheoryes.Microservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PsychologyTheoryController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IBus _bus;

        private List<PsychologyTheory> PsychologyTheory;
        public PsychologyTheoryController(IBus bus, ILogger<PsychologyTheoryController> logger)
        {
            _bus = bus;
            _logger = logger;

            PsychologyTheory = new List<PsychologyTheory>()
            {
new PsychologyTheory
    {
        Id = 1,
        Name = "Thuyết Phát Triển Nhận Thức",
        Description = "Lý thuyết về sự phát triển trí tuệ của con người qua các giai đoạn.",
        TopicId = 101,
        Author = "Jean Piaget",
        YearPublished = 1936,
        TheoryType = "Nhận thức",
        Principle = "Trẻ em trải qua 4 giai đoạn phát triển nhận thức: Cảm giác - Vận động, Tiền thao tác, Thao tác cụ thể, và Thao tác hình thức.",
        Application = "Ứng dụng trong giáo dục trẻ em, giúp xây dựng phương pháp giảng dạy phù hợp với từng giai đoạn phát triển.",
        RelatedTheory = "Thuyết học tập xã hội của Vygotsky",
        Criticism = "Một số nhà nghiên cứu cho rằng Piaget đánh giá thấp vai trò của môi trường xã hội.",
        CreateAt = DateTime.Now,
        UpdateAt = DateTime.Now
    },
    new PsychologyTheory
    {
        Id = 2,
        Name = "Thuyết Học Tập Xã Hội",
        Description = "Lý thuyết về cách con người học hỏi thông qua quan sát và mô phỏng hành vi của người khác.",
        TopicId = 102,
        Author = "Albert Bandura",
        YearPublished = 1961,
        TheoryType = "Hành vi - Xã hội",
        Principle = "Con người học qua quan sát, mô phỏng và củng cố từ xã hội.",
        Application = "Ứng dụng trong giáo dục, truyền thông và quản lý nhân sự.",
        RelatedTheory = "Thuyết điều kiện hóa của Skinner",
        Criticism = "Chưa đề cập đầy đủ đến ảnh hưởng của yếu tố sinh học đối với hành vi.",
        CreateAt = DateTime.Now,
        UpdateAt = DateTime.Now
    },
    new PsychologyTheory
    {
        Id = 3,
        Name = "Thuyết Nhu Cầu Maslow",
        Description = "Mô hình phân cấp nhu cầu của con người từ cơ bản đến cao cấp.",
        TopicId = 103,
        Author = "Abraham Maslow",
        YearPublished = 1943,
        TheoryType = "Động lực",
        Principle = "Nhu cầu của con người được phân cấp từ sinh lý, an toàn, xã hội, tôn trọng đến tự thể hiện.",
        Application = "Ứng dụng trong quản lý nhân sự, tâm lý trị liệu và marketing.",
        RelatedTheory = "Thuyết hai nhân tố của Herzberg",
        Criticism = "Không phải mọi người đều đi theo thứ tự cấp bậc nhu cầu này.",
        CreateAt = DateTime.Now,
        UpdateAt = DateTime.Now
    }
            };
        }

        // GET: api/<PsychologyTheoryController>
        [HttpGet]
        public IEnumerable<PsychologyTheory> Get()
        {
            //return new string[] { "value1", "value2" };
            return PsychologyTheory;
        }

        // GET api/<PsychologyTheoryController>/5
        [HttpGet("{id}")]
        public PsychologyTheory Get(int id)
        {
            //return "value";
            return PsychologyTheory.Find(x => x.Id == id);
        }

        // POST api/<PsychologyTheoryController>
        [HttpPost]
        public async Task<IActionResult> Post(PsychologyTheory item)
        {
            if (item != null)
            {
                #region Business rule process anh/or call other API Service

                #endregion

                #region Publish data to Queue on RabbitMQ

                //Lets Queue as itemQueue.
                //Create a new URL ‘rabbitmq://localhost/itemQueue’
                //If itemQueue does not exist, RabbitMQ creates one
                Uri uri = new Uri("rabbitmq://localhost/psychologyTheoryQueue");
                //Gets an endpoint to send the shared model object
                var endPoint = await _bus.GetSendEndpoint(uri);
                //Push the message to the queue

                await endPoint.Send(item);

                #endregion

                #region Logger

                string messageLog = string.Format("[{0}] PUBLISH data to RabbitMQ.psychologyTheoryQueue: {1}", DateTime.Now, Utilities.ConvertObjectToJSONString(item));
                Utilities.WriteLoggerFile(messageLog);
                _logger.LogInformation(messageLog);

                #endregion


                return Ok();
            }
            return BadRequest();
        }

        // PUT api/<PsychologyTheoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PsychologyTheoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
