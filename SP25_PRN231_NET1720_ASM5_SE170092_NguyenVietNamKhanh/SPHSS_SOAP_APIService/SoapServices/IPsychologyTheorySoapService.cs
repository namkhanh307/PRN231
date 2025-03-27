using SPHSS_SOAP_APIService.SoapModels;
using System.ServiceModel;

namespace SPHSS_SOAP_APIService.SoapServices
{
    [ServiceContract]
    public interface IPsychologyTheorySoapService
    {
        [OperationContract]
        Task<List<PsychologyTheory>> GetPsychologyTheories();
        [OperationContract]
        PsychologyTheory GetPsychologyTheory(int id);
        [OperationContract]
        Task<int> CreatePsychologyTheory(PsychologyTheory psychologyTheory);
        [OperationContract]
        Task<int> UpdatePsychologyTheory(PsychologyTheory psychologyTheory);
        [OperationContract]
        Task<bool> DeletePsychologyTheory(int id);
        [OperationContract]
        Task<List<Topic>> GetTopics();
    }
}
