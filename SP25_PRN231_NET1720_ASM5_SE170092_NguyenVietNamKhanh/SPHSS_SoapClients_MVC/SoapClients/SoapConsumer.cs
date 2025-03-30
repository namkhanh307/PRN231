using SOAPServiceReference;
using System.Text.Json.Serialization;
using System.Text.Json;

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

            return [new PsychologyTheory()];
        }

        public async Task<PsychologyTheory> GetPsychologyTheory(int id)
        {
            try
            {
                var result = await _psychologyTheorySoapService.GetPsychologyTheoryAsync(id);
                return result;
            }
            catch (Exception ex)
            {
            }

            return new PsychologyTheory();
        }

        public async Task<int> CreatePsychologyTheory(PsychologyTheory psychologyTheory)
        {
            try
            {
                if (psychologyTheory == null)
                {
                    throw new ArgumentNullException(nameof(psychologyTheory));
                }

                // Call the service layer to add the new record
                return await _psychologyTheorySoapService.CreatePsychologyTheoryAsync(psychologyTheory);

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

                // Call the service layer to add the new record
                return await _psychologyTheorySoapService.DeletePsychologyTheoryAsync(id);

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

                // Call the service layer to add the new record
                return await _psychologyTheorySoapService.UpdatePsychologyTheoryAsync(psychologyTheory);

            }
            catch (Exception ex)
            {
                // Log the exception (Replace with proper logging)
                Console.WriteLine($"Error: {ex.Message}");

                // In case of failure, return -1 or another indicative value
                return -1;
            }
        }
        public async Task<Topic[]> GetTopics()
        {
            try
            {
                var result = await _psychologyTheorySoapService.GetTopicsAsync();
                return result;
            }
            catch (Exception ex)
            {
            }

            return [new Topic()];
        }
    }
}
