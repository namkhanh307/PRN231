using MassTransit.Transports;
using MassTransit;
using BusinessObject.Shared.Models;
using Common.Shared;

namespace Rates.Microservices.Consumers
{
    public class PsychologyTheoryConsumer : IConsumer<PsychologyTheory>
    {
            private readonly ILogger _logger;

            public PsychologyTheoryConsumer(ILogger<PsychologyTheoryConsumer> logger)
            {
                _logger = logger;
            }

            public async Task Consume(ConsumeContext<PsychologyTheory> context)
            {
                //https://localhost:7024/gateway/userProgress

                #region Receive data from Queue on RabbitMQ            

                var data = context.Message;

                #endregion

                #region Business rule process anh/or call other API Service

                //Validate the Data
                //Store to Database
                //Notify the user via Email / SMS

                #endregion

                #region Logger

                if (data != null)
                {
                    string messageLog = string.Format("[{0}] RECEIVE data from RabbitMQ.psychologyTheoryQueue: {1}", DateTime.Now.ToString(), Utilities.ConvertObjectToJSONString(data));

                    Utilities.WriteLoggerFile(messageLog);

                    _logger.LogInformation(messageLog);

                }

                #endregion

            }
        
    }
}
