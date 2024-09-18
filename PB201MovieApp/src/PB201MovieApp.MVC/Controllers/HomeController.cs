using Microsoft.AspNetCore.Mvc;
using PB201MovieApp.MVC.ApiResponseMessages;
using PB201MovieApp.MVC.Models;
using PB201MovieApp.MVC.ViewModels.GenreVMs;
using RestSharp;
using System.Diagnostics;
using System.Text.Json;

namespace PB201MovieApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly RestClient _restClient;
        public HomeController()
        {
            _restClient = new RestClient("https://localhost:7205/api/");
        }

        public async Task<IActionResult> Index()
        {
            var request = new RestRequest("Genres", Method.Get);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<List<GenreGetVM>>>(request);

            if (!response.IsSuccessful)
            {
                ViewBag.Err = response.ErrorMessage;

                return View();
            }
            //List<GenreGetVM> vm = JsonSerializer.Deserialize<List<GenreGetVM>>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});

            return View(response.Data.Data);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var request = new RestRequest($"Genres/{id}", Method.Get);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<GenreGetVM>>(request);

            if (!response.IsSuccessful)
            {
                ViewBag.Err = response.Data.ErrorMessage;
                return View();
            }

            return View(response.Data.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GenreCreateVM vm)
        {
            var request = new RestRequest("Genres", Method.Post);
            request.AddJsonBody(vm);

            var response = await _restClient.ExecuteAsync<ApiResponseMessage<GenreCreateVM>>(request);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("Name", response.Data.ErrorMessage);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var request = new RestRequest($"genres/{id}", Method.Delete);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<object>>(request);

            if (!response.IsSuccessful)
            {
                TempData["Err"] = response.Data.ErrorMessage;
                return RedirectToAction("Index");
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var request = new RestRequest($"genres/{id}", Method.Get);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<GenreGetVM>>(request);

            if (!response.IsSuccessful)
            {
                TempData["Err"] = response.Data.ErrorMessage;
                return RedirectToAction("Index");
            }

            GenreUpdateVM vm = new GenreUpdateVM(response.Data.Data.Name,response.Data.Data.IsDeleted);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, GenreUpdateVM vm)
        {
            var request = new RestRequest($"genres/{id}", Method.Put);
            request.AddJsonBody(vm);

            var response = await _restClient.ExecuteAsync<ApiResponseMessage<object>>(request);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError(response.Data.PropertyName, response.Data.ErrorMessage);
                return View();
            }

            return RedirectToAction("Index");
        }
    }
}
