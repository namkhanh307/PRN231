using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPHSS_SoapClients_MVC.SoapClients;
using SPHSSSoapServiceReference;

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
                var result = await _soapConsumer.GetPsychologyTheories();
                return View(result);
            }
            catch (Exception ex)
            {
            }

            return View(new PsychologyTheory[] { new PsychologyTheory() });
        }

        //// GET: PsychologyTheoriesController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: PsychologyTheoriesController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: PsychologyTheoriesController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: PsychologyTheoriesController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: PsychologyTheoriesController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: PsychologyTheoriesController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: PsychologyTheoriesController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
