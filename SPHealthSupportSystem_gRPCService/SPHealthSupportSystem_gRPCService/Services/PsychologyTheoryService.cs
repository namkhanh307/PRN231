using AutoMapper;
using Grpc.Core;
using SPHealthSupportSystem_gRPCService.Protos;
using SPHealthSupportSystem_Services;

namespace SPHealthSupportSystem_gRPCService.Services
{
    public class PsychologyTheoryService : Protos.PsychologyTheoryService.PsychologyTheoryServiceBase
    {
        private readonly ILogger<PsychologyTheoryService> _logger;
        private readonly IPsychologyTheoryService _service;
        private readonly IMapper _mapper;

        public PsychologyTheoryService(IPsychologyTheoryService service, ILogger<PsychologyTheoryService> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _service = service;
        }

        public override async Task<PsychologyTheoryList> GetAll(Empty request, ServerCallContext context)
        {
            var item = await _service.GetAll();
            var list = new PsychologyTheoryList();
                list.Theories.AddRange(_mapper.Map<List<PsychologyTheory>>(item));
            return list;
        }
        public override async Task<PsychologyTheory> GetById(PsychologyTheoryRequest request, ServerCallContext context)
        {
            var item = _service.GetById(request.Id);
            if (item != null)
            {
                var itemReturn = _mapper.Map<PsychologyTheory>(item);
                // Trả về UserProgress nếu tìm thấy
                return itemReturn;
            }
            // Trả về UserProgress rỗng nếu không tìm thấy
            return new PsychologyTheory();
        }
        public override async Task<ActionResult> Add(PsychologyTheory request, ServerCallContext context)
        {
            if (request != null)
            {
                var item = _mapper.Map<SPHealthSupportSystem_Repositories.Models.PsychologyTheory>(request);
                await _service.Create(item);

                return new ActionResult()
                {
                    Status = 1,
                    Message = "Success"
                };
            }

            return new ActionResult()
            {
                Status = -1,
                Message = "Fail"
            };
        }

        public override async Task<ActionResult> Update(PsychologyTheory request, ServerCallContext context)
        {
            if (request != null)
            {

                await _service.Update(_mapper.Map<SPHealthSupportSystem_Repositories.Models.PsychologyTheory>(request));

                return new ActionResult()
                {
                    Status = 1,
                    Message = "Success"
                };
            }

            return new ActionResult()
            {
                Status = -1,
                Message = "Fail"
            };
        }

        public override async Task<ActionResult> Delete(PsychologyTheoryRequest request, ServerCallContext context)
        {
            if (request != null)
            {

                await _service.Delete(request.Id);

                return new ActionResult()
                {
                    Status = 1,
                    Message = "Success"
                };
            }

            return new ActionResult()
            {
                Status = -1,
                Message = "Fail"
            };
        }

    }
}
