using Grpc.Core;
using SPHealthSupportSystem_gRPCService.Protos;

namespace SPHealthSupportSystem_gRPCService.Services
{
    public class PsychologyTheoryService : Protos.PsychologyTheoryService.PsychologyTheoryServiceBase
    {
        private readonly ILogger<PsychologyTheoryService> _logger;

        public PsychologyTheoryService(ILogger<PsychologyTheoryService> logger)
        {
            _logger = logger;
        }
        private readonly PsychologyTheoryList items = new PsychologyTheoryList
        {
            Theories =
            {
                new PsychologyTheory
                {
                    Id = 1,
                    Name = "Theory 1",
                    Description = "Description of Theory 1",
                    Author = "Author 1",
                    YearPublished = 2025,
                    TheoryType = "Type 1",
                    Principle = "Principle 1",
                    Application = "Application 1",
                    RelatedTheory = "Related Theory 1",
                    Criticism = "Criticism 1",
                    CreatedAt = DateTime.UtcNow.ToString("o"),
                    UpdatedAt = DateTime.UtcNow.ToString("o"),
                    TopicId = 1
                },
                new PsychologyTheory
                {
                    Id = 2,
                    Name = "Theory 2",
                    Description = "Description of Theory 2",
                    Author = "Author 2",
                    YearPublished = 2025,
                    TheoryType = "Type 2",
                    Principle = "Principle 2",
                    Application = "Application 2",
                    RelatedTheory = "Related Theory 2",
                    Criticism = "Criticism 2",
                    CreatedAt = DateTime.UtcNow.ToString("o"),
                    UpdatedAt = DateTime.UtcNow.ToString("o"),
                    TopicId = 2
                }
            }
        };

        public override async Task<PsychologyTheoryList> GetAll(Empty request, ServerCallContext context)
        {
            return await Task.FromResult(items);
        }
        public override async Task<PsychologyTheory> GetById(PsychologyTheoryRequest request, ServerCallContext context)
        {
            var item = items.Theories.FirstOrDefault(b => b.Id == request.Id);
            if (item != null)
            {
                return await Task.FromResult(item);
            }
            return await Task.FromResult(new PsychologyTheory());
        }
        public override async Task<ActionResult> Add(PsychologyTheory request, ServerCallContext context)
        {
            if (request != null)
            {
                items.Theories.Add(request);
                return await Task.FromResult(new ActionResult() { Status = 1, Message = "Success", Data = items });
            }

            return await Task.FromResult(new ActionResult() { Status = -1, Message = "Fail", Data = null });
        }

        public override async Task<ActionResult> Update(PsychologyTheory request, ServerCallContext context)
        {
            if (request != null)
            {
                var item = items.Theories.FirstOrDefault(b => b.Id == request.Id);
                bool result = items.Theories.Remove(item);
                items.Theories.Add(request);
                return await Task.FromResult(new ActionResult() { Status = 1, Message = "Success", Data = items });
            }

            return await Task.FromResult(new ActionResult() { Status = -1, Message = "Fail", Data = null });
        }

        public override Task<ActionResult> Delete(PsychologyTheoryRequest request, ServerCallContext context)
        {
            var item = items.Theories.FirstOrDefault(b => b.Id == request.Id);
            if (item != null)
            {
                bool result = items.Theories.Remove(item);
                return Task.FromResult(new ActionResult() { Status = 1, Message = "Delete Success", Data = items });

            }
            return Task.FromResult(new ActionResult() { Status = -1, Message = "Delete Fail", });
        }

    }
}
