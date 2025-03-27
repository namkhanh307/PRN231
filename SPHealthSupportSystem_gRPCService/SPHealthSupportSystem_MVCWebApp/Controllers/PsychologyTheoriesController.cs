using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SPHealthSupportSystem_gRPCService.Protos;
using SPHealthSupportSystem_MVCWebApp.Protos;

namespace SPHealthSupportSystem_MVCWebApp.Controllers
{
    public class PsychologyTheoriesController : Controller
    {

        private readonly GrpcClient _grpcClient;

        public PsychologyTheoriesController(GrpcClient grpcClient)
        {
            _grpcClient = grpcClient;
        }

        // GET: /PsychologyTheory
        public async Task<IActionResult> Index()
        {
            // Call the gRPC GetAll method.
            var response = await _grpcClient.Client.GetAllAsync(new Empty());
            return View(response.Theories);
        }
        public async Task<IActionResult> Details(int id)
        {
            var response = await _grpcClient.Client.GetByIdAsync(new PsychologyTheoryRequest { Id = id });
            if (response == null || response.Id == 0)
            {
                return NotFound();
            }
            return View(response);
        }

        // GET: /PsychologyTheory/Create
        public IActionResult Create()
        {
            List<int> topics = new List<int> { 1, 2 };
            ViewData["TopicId"] = new SelectList(topics);
            return View();
        }


        // POST: /PsychologyTheory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PsychologyTheory theory)
        {
            if (ModelState.IsValid)
            {
                var result = await _grpcClient.Client.AddAsync(theory);
                if (result.Status == 1)
                {
                    return View("Index", result.Data.Theories);
                }
                ModelState.AddModelError(string.Empty, result.Message);
            }
            List<int> topics = new List<int> { 1, 2 };
            ViewData["TopicId"] = new SelectList(topics);
            return View(theory);
        }

        // GET: /PsychologyTheory/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _grpcClient.Client.GetByIdAsync(new PsychologyTheoryRequest { Id = id });
            if (response == null || response.Id == 0)
            {
                return NotFound();
            }
            List<int> topics = new List<int> { 1, 2 };
            ViewData["TopicId"] = new SelectList(topics);
            return View(response);
        }

        // POST: /PsychologyTheory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PsychologyTheory theory)
        {
            if (id != theory.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var result = await _grpcClient.Client.UpdateAsync(theory);
                if (result.Status == 1)
                {
                    return View("Index", result.Data.Theories);
                }
                ModelState.AddModelError(string.Empty, result.Message);
            }
            List<int> topics = new List<int> { 1, 2 };
            ViewData["TopicId"] = new SelectList(topics);
            return View(theory);
        }

        // GET: /PsychologyTheory/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _grpcClient.Client.GetByIdAsync(new PsychologyTheoryRequest { Id = id });
            if (response == null || response.Id == 0)
            {
                return NotFound();
            }
            return View(response);
        }

        // POST: /PsychologyTheory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _grpcClient.Client.DeleteAsync(new PsychologyTheoryRequest { Id = id });
            if (result.Status == 1)
            {
                return View("Index", result.Data.Theories);
            }
            // Optionally add error handling.
            ModelState.AddModelError(string.Empty, result.Message);
            return RedirectToAction(nameof(Index));
        }
    }
}
