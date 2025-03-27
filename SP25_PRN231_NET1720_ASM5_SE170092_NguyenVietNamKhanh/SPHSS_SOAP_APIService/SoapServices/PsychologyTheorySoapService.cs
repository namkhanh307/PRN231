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
        public Task<int> CreatePsychologyTheory(PsychologyTheory psychologyTheory)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePsychologyTheory(int id)
        {
            throw new NotImplementedException();
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

        public Task<List<Topic>> GetTopics()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdatePsychologyTheory(PsychologyTheory psychologyTheory)
        {
            throw new NotImplementedException();
        }
    }
}
