using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SOAPServiceReference;
using SPHSS_SoapClients_MVC.SoapClients;

namespace SPHSS_SoapClients_MVC.Controllers
{
    public class PsychologyTheoriesController : Controller
    {
        private readonly SoapConsumer _soapConsumer;

        public PsychologyTheoriesController(SoapConsumer soapConsumer) => _soapConsumer = soapConsumer;
        // GET: PsychologyTheoriesController
        public async Task<IActionResult> Index()
        {
            try
            {
                PsychologyTheory[] result = await _soapConsumer.GetPsychologyTheories();
                return View(result);
            }
            catch (Exception ex)
            {
            }

            return View(new PsychologyTheory[] { new PsychologyTheory() });
        }

        //// GET: PsychologyTheoriesController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                PsychologyTheory result = await _soapConsumer.GetPsychologyTheory(id);
                return View(result);
            }
            catch (Exception ex)
            {
            }

            return View(new PsychologyTheory());
        }

        //// GET: PsychologyTheoriesController/Create
        public async Task<IActionResult> Create()
        {
            Topic[] topic = await GetTopics();
            ViewBag.Topics = new SelectList(topic, "Id", "Name");
            return View();
        }

        //// POST: PsychologyTheoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PsychologyTheory psychologyTheory)
        {
            try
            {
                var result = await _soapConsumer.CreatePsychologyTheory(psychologyTheory);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //// GET: PsychologyTheoriesController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            //get ref
            Topic[] topic = await GetTopics();
            //get main
            var result = await _soapConsumer.GetPsychologyTheory(id);
            if (result != null)
            {
                ViewBag.Topics = topic.Any()
                       ? new SelectList(topic, "Id", "Name", result.TopicId)
                       : new SelectList(new List<Topic>(), "Id", "Name");
                return View(result);
            }
            ViewBag.Topics = new SelectList(new List<Topic>(), "Id", "Name"); 
            return View(new PsychologyTheory());
        }
        [HttpPost]
        public async Task<IActionResult> Edit(PsychologyTheory psychologyTheory)
        {
            bool saveStatus = false;
            var topic = await this.GetTopics();
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _soapConsumer.UpdatePsychologyTheory(psychologyTheory);

                    if (result > 0)
                    {
                        saveStatus = true;
                    }
                    else
                    {
                        saveStatus = false;
                    }                                                               
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;

                }
                if (saveStatus)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Topics = new SelectList(new List<Topic>(), "Id", "Name"); 
                    return View(psychologyTheory);
                }
            }
            var errors = string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

            return RedirectToAction(nameof(Index));
        }

        // POST: PsychologyTheoriesController/Delete/5
        public async Task<IActionResult> DeleteQuickly(int id)
        {
            var result = await _soapConsumer.DeletePsychologyTheory(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<Topic[]> GetTopics()
        {
           
            return await _soapConsumer.GetTopics();
        }
    }
}
