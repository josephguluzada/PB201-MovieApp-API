using Microsoft.AspNetCore.Mvc;
using PB201MovieApp.MVC.ApiResponseMessages;
using PB201MovieApp.MVC.Services.Interfaces;
using PB201MovieApp.MVC.UIExceptions.Common;
using PB201MovieApp.MVC.ViewModels.GenreVMs;
using RestSharp;

namespace PB201MovieApp.MVC.Controllers
{
    public class GenreController : Controller
    {
        private readonly ICrudService _crudService;

        public GenreController(ICrudService crudService)
        {
            _crudService = crudService;
        }

        public async Task<IActionResult> Index()
        {
            if (Request.Cookies["token"] == null)
            {
                return RedirectToAction("login", "auth");
            }
            var datas = await _crudService.GetAllAsync<List<GenreGetVM>>("/genres");

            return View(datas);
        }

        public async Task<IActionResult> Detail(int id)
        {
            GenreGetVM data = null;
            try
            {
                data = await _crudService.GetByIdAsync<GenreGetVM>($"/genres/{id}", id);
            }
            catch (BadrequestException ex)
            {
                TempData["Err"] = ex.Message;
                return View("Error");
            }catch(ModelNotFoundException ex)
            {
                TempData["Err"] = ex.Message;
                return View("Error");
            }catch(Exception ex)
            {
                TempData["Err"] = ex.Message;
                return View("Error");
            }

            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GenreCreateVM vm)
        {
            await _crudService.Create("/genres", vm);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _crudService.Delete<object>($"/genres/{id}", id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var data = await _crudService.GetByIdAsync<GenreUpdateVM>($"/genres/{id}", id);

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, GenreUpdateVM vm)
        {
            try
            {
                await _crudService.Update($"/genres/{id}", vm);
            }
            catch(ModelStateException ex)
            {
                ModelState.AddModelError(ex.PropertyName,ex.Message);
                return View();
            }
            catch (BadrequestException ex)
            {
                TempData["Err"] = ex.Message;
                return View("Error");
            }
            catch (ModelNotFoundException ex)
            {
                TempData["Err"] = ex.Message;
                return View("Error");
            }
            catch (Exception ex)
            {
                TempData["Err"] = ex.Message;
                return View("Error");
            }

            return RedirectToAction("Index");
        }
    }
}
