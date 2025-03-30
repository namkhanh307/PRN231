using SPHealthSupportSystem_Services;
using SPHSS_SOAP_APIService.SoapModels;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SPHSS_SOAP_APIService.SoapServices
{
    public class PsychologyTheorySoapService : IPsychologyTheorySoapService
    {
        private readonly IPsychologyTheoryService _psychologyTheoryService;
        private readonly ITopicService _topicService;
        public PsychologyTheorySoapService(IPsychologyTheoryService psychologyTheoryService, ITopicService topicService)
        {
            _psychologyTheoryService = psychologyTheoryService;
            _topicService = topicService;
        }


        public async Task<List<PsychologyTheory>> GetPsychologyTheories()
        {
            try
            {
                var items = await _psychologyTheoryService.GetAll();

                var opt = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
                var itemsString = JsonSerializer.Serialize(items, opt);
                var result = JsonSerializer.Deserialize<List<PsychologyTheory>>(itemsString, opt);

                return result;
            }
            catch (Exception ex)
            {
                return new List<PsychologyTheory>();
            }
        }

        public PsychologyTheory GetPsychologyTheory(int id)
        {
            try
            {
                var items = _psychologyTheoryService.GetById(id);

                var opt = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
                var itemsString = JsonSerializer.Serialize(items, opt);
                var result = JsonSerializer.Deserialize<PsychologyTheory>(itemsString, opt);

                return result;
            }
            catch (Exception ex)
            {
                return new PsychologyTheory();
            }
        }

        public async Task<int> CreatePsychologyTheory(PsychologyTheory psychologyTheory)
        {
            try
            {
                if (psychologyTheory == null)
                {
                    throw new ArgumentNullException(nameof(psychologyTheory));
                }

                // Convert SOAP model to database model
                var model = MapToModel(psychologyTheory);

                // Call the service layer to add the new record
                return await _psychologyTheoryService.Create(model);

            }
            catch (Exception ex)
            {
                // Log the exception (Replace with proper logging)
                Console.WriteLine($"Error: {ex.Message}");

                // In case of failure, return -1 or another indicative value
                return -1;
            }
        }


        public async Task<bool> DeletePsychologyTheory(int id)
        {
            try
            {              
                return await _psychologyTheoryService.Delete(id);

            }
            catch (Exception ex)
            {
                // Log the exception (Replace with proper logging)
                Console.WriteLine($"Error: {ex.Message}");

                // In case of failure, return -1 or another indicative value
                return false;
            }
        }
        public async Task<int> UpdatePsychologyTheory(PsychologyTheory psychologyTheory)
        {
            try
            {
                if (psychologyTheory == null)
                {
                    throw new ArgumentNullException(nameof(psychologyTheory));
                }

                // Convert SOAP model to database model
                var model = MapToModel(psychologyTheory);

                // Call the service layer to add the new record
                return await _psychologyTheoryService.Update(model);

            }
            catch (Exception ex)
            {
                // Log the exception (Replace with proper logging)
                Console.WriteLine($"Error: {ex.Message}");

                // In case of failure, return -1 or another indicative value
                return -1;
            }
        }
        public async Task<List<Topic>> GetTopics()
        {
            try
            {
                List<Topic> topicList = new List<Topic>();
                var items = await _topicService.GetAll();
                foreach (var item in items)
                {
                    topicList.Add(MapTopic(item));
                }
                foreach (var item in topicList)
                {
                    Console.WriteLine(item.Id + item.Name);
                }
                return topicList;
            }
            catch (Exception ex)
            {
                return new List<Topic>();
            }
        }

        private SPHealthSupportSystem_Repositories.Models.PsychologyTheory MapToModel(SPHSS_SOAP_APIService.SoapModels.PsychologyTheory soapModel)
        {
            return new SPHealthSupportSystem_Repositories.Models.PsychologyTheory
            {
                Id = soapModel.Id,
                Name = soapModel.Name,
                Description = soapModel.Description,
                TopicId = soapModel.TopicId,
                Author = soapModel.Author,
                YearPublished = soapModel.YearPublished,
                TheoryType = soapModel.TheoryType,
                Principle = soapModel.Principle,
                Application = soapModel.Application,
                RelatedTheory = soapModel.RelatedTheory,
                Criticism = soapModel.Criticism,
                CreateAt = soapModel.CreateAt ?? DateTime.UtcNow, 
                UpdateAt = soapModel.UpdateAt ?? DateTime.UtcNow
            };
        }

        private SPHSS_SOAP_APIService.SoapModels.Topic MapTopic(SPHealthSupportSystem_Repositories.Models.Topic soapModel)
        {
            return new SPHSS_SOAP_APIService.SoapModels.Topic
            {
                Id = soapModel.Id,
                Name = soapModel.Name,
                Description = soapModel.Description,          
                CreateAt = soapModel.CreateAt ?? DateTime.UtcNow,
                UpdateAt = soapModel.UpdateAt ?? DateTime.UtcNow
            };
        }

    }
}
