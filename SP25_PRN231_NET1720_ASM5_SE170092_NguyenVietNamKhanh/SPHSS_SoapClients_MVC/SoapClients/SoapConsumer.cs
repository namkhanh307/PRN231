using SPHSSSoapServiceReference;

namespace SPHSS_SoapClients_MVC.SoapClients
{
    public class SoapConsumer
    {
        private IPsychologyTheorySoapService _psychologyTheorySoapService;
        public SoapConsumer()
        {
            _psychologyTheorySoapService = new PsychologyTheorySoapServiceClient(PsychologyTheorySoapServiceClient.EndpointConfiguration.BasicHttpBinding_IPsychologyTheorySoapService);
        }
        public async Task<PsychologyTheory[]> GetPsychologyTheories()
        {
            try
            {
                var result = await _psychologyTheorySoapService.GetPsychologyTheoriesAsync();
                return result;
            }
            catch (Exception ex)
            {
            }

            return new PsychologyTheory[] { new PsychologyTheory() };
        }
    }
}
