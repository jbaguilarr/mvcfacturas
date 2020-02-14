using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebVenta.Models;

namespace WebVenta.Controllers
{
    public class PersonaController : Controller
    {
        string _url = "https://localhost:44383/api/";
        //get persona
        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<PersonaViewModel> _persona = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);
                //HTTP GET
                var responseTask = client.GetAsync("Persona");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<PersonaViewModel>>();
                    readTask.Wait();

                    _persona = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    _persona = Enumerable.Empty<PersonaViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(_persona);
        }
        // GET: Movies/Create
        [HttpGet]
        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(PersonaViewModel persona)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url + "Persona");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<PersonaViewModel>("Persona", persona);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(persona);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            PersonaViewModel persona = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);
                //HTTP GET
                var responseTask = client.GetAsync("Persona/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<PersonaViewModel>();
                    readTask.Wait();

                    persona = readTask.Result;
                }
            }
            return View(persona);
        }

        [HttpPost]
        public ActionResult Edit(PersonaViewModel persona)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url + "Persona");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<PersonaViewModel>("Persona", persona);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(persona);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("Persona/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            PersonaViewModel persona = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);
                //HTTP GET
                var responseTask = client.GetAsync("Persona/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<PersonaViewModel>();
                    readTask.Wait();

                    persona = readTask.Result;
                }
            }
            return View(persona);
        }
    }
}
