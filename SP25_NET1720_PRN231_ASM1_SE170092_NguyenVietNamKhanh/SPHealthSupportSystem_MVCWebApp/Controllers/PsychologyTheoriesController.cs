using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SPHealthSupportSystem_Repositories.DBContext;
using SPHealthSupportSystem_Repositories.Models;
using SPHealthSupportSystem_Services;
using static System.Net.WebRequestMethods;

namespace SPHealthSupportSystem_MVCWebApp.Controllers
{
    public class PsychologyTheoriesController : Controller
    {
        private string APIEndPoint = "https://localhost:7254/api/";

        // GET: PsychologyTheories
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request

                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion


                using (var response = await httpClient.GetAsync(APIEndPoint + "PsychologyTheories"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<PsychologyTheory>>(content);

                        if (result != null)
                        {
                            return View(result);
                        }
                    }
                }
            }

            return View(new List<PsychologyTheory>());
        }

        // GET: PsychologyTheories/Details/5
        public async Task<IActionResult> Details(int id)
        {
            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request

                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion


                using (var response = await httpClient.GetAsync(APIEndPoint + "PsychologyTheories/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<PsychologyTheory>(content);

                        if (result != null)
                        {
                            return View(result);
                        }
                    }
                }
            }

            return View(new PsychologyTheory());
        }

        public async Task<IActionResult> DeleteQuickly(string id)
        {
            bool deleteStatus = false;
            using (var httpClient = new HttpClient())
            {
                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);
                using (var response = await httpClient.DeleteAsync(APIEndPoint + "PsychologyTheories/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        deleteStatus = true;
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //get ref
            var topic = new List<Topic>();

            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request
                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion


                using (var response = await httpClient.GetAsync(APIEndPoint + "Topics/"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        topic = JsonConvert.DeserializeObject<List<Topic>>(content);
                    }
                }
            }
            //get main
            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request

                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion


                using (var response = await httpClient.GetAsync(APIEndPoint + "PsychologyTheories/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<PsychologyTheory>(content);

                        if (result != null)
                        {
                            ViewData["TopicId"] = new SelectList(topic, "Id", "Name", result.TopicId);
                            return View(result);
                        }
                    }
                }
            }
            ViewData["TopicId"] = new SelectList(topic, "TopicId", "Name");
            return View(new PsychologyTheory());
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, PsychologyTheory psychologyTheory)
        {
            bool saveStatus = false;
            var topic = await this.GetTopics();
            if (ModelState.IsValid)
            {
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        #region Add Token to header of Request

                        var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                        #endregion


                        using (var response = await httpClient.PutAsJsonAsync(APIEndPoint + "PsychologyTheories/" + id, psychologyTheory))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var content = await response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<int>(content);

                                if (result > 0)
                                {
                                    saveStatus = true;
                                }
                                else
                                {
                                    saveStatus = false;
                                }
                            }
                        }
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
                    ViewData["TopicId"] = new SelectList(topic, "Id", "Name", psychologyTheory.TopicId);
                    return View(psychologyTheory);
                }
            }
            var errors = string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

            return RedirectToAction(nameof(Index));
        }
        public async Task<List<Topic>?> GetTopics()
        {
            var topic = new List<Topic>();

            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request
                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion


                using (var response = await httpClient.GetAsync(APIEndPoint + "Topics/"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        topic = JsonConvert.DeserializeObject<List<Topic>>(content);
                    }
                }
            }
            return topic;
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["TopicId"] = new SelectList(await GetTopics(), "Id", "Name");
            return View();
        }
        public async Task<IActionResult> Create(PsychologyTheory psychologyTheory)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);
                        using (var response = await httpClient.PostAsJsonAsync(APIEndPoint + "PsychologyTheories/", psychologyTheory))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var content = await response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<int>(content);
                                if (result > 0)
                                {
                                    return RedirectToAction(nameof(Index));
                                }
                            }
                        }

                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            var errors = string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            ViewData["TopicId"] = new SelectList(await GetTopics(), "Id", "Name", psychologyTheory.TopicId);
            return View(psychologyTheory);
        }
        public async Task<IActionResult> Search(string name, string description, string author)
        {

            string baseUrl = APIEndPoint + "PsychologyTheories/";

            // If user entered a search query, add OData filter
            if (!string.IsNullOrWhiteSpace(name) || !string.IsNullOrWhiteSpace(description) || !string.IsNullOrWhiteSpace(author))
            {
                baseUrl += $"?$filter=contains(Name,'{name}') or contains(Description,'{description}') or contains(Author,'{author}')";
            }
            using (var httpClient = new HttpClient())
            {
                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);
                using (var response = await httpClient.GetAsync(baseUrl))
                {

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Failed to fetch data from API");
                    }

                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<PsychologyTheory>>(content);
                    if (result != null)
                    {
                        return View("Index", result);
                    }
                }
            }
            return View(new List<PsychologyTheory>());
        }

    }
}
