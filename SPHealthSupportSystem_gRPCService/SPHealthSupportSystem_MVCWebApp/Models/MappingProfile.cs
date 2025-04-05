using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SPHealthSupportSystem_MVCWebApp.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Ánh xạ từ UserProgress trong C# sang UserProgress trong gRPC
            CreateMap<PsychologyTheory, SPHealthSupportSystem_gRPCService.Protos.PsychologyTheory>()
                       .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreateAt.HasValue ? src.CreateAt.Value.ToString("yyyy-MM-dd HH:mm:ss") : ""))
                       .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdateAt.HasValue ? src.UpdateAt.Value.ToString("yyyy-MM-dd HH:mm:ss") : ""));

            // Ánh xạ ngược từ UserProgress trong gRPC về UserProgress trong C#
            CreateMap<SPHealthSupportSystem_gRPCService.Protos.PsychologyTheory, PsychologyTheory>()
            .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => ParseDateTime(src.CreatedAt)))
            .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => ParseDateTime(src.UpdatedAt)));

        }

        private DateTime? ParseDateTime(string dateTimeString)
        {
            DateTime date;
            if (DateTime.TryParse(dateTimeString, out date))
            {
                return date;
            }
            else
            {
                return null;
            }
        }
    }
}
