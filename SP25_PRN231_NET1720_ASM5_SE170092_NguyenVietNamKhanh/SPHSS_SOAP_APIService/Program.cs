using SoapCore;
using SPHealthSupportSystem_Services;
using SPHSS_SOAP_APIService.SoapServices;

namespace SPHSS_SOAP_APIService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IPsychologyTheorySoapService, PsychologyTheorySoapService>();
            builder.Services.AddScoped<IPsychologyTheoryService, PsychologyTheoryService>();
            builder.Services.AddScoped<ITopicService, TopicService>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSoapEndpoint<IPsychologyTheorySoapService>("/SPHSSSoapService.asmx", new SoapEncoderOptions());

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
