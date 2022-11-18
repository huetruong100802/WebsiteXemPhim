using BusinessObject.Models;
using DataAccess.Repository;
using DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieWebsite.Controllers
{

    public class PeopleController : BaseController
    {

        IPeopleRepository PeopleRepository = null!;
        private static readonly string SUCCESS="Action complete successfully!";

        public PeopleController()
        {
            PeopleRepository = new PeopleRepository();
        }

        // GET: PeoplesController
        public ActionResult Index(string? message)
        {
            var Peoples= PeopleRepository.GetPeoples();
            ViewBag.Message = message;
            return View(Peoples);
        }

        // GET: PeoplesController/Details/5
        public ActionResult Details(int id)
        {
            return RedirectToAction(nameof(Index));
        }

        // POST: PeoplesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string PeopleName)
        {
            try
            {
                if (String.IsNullOrEmpty(PeopleName))
                {
                    throw new Exception("People name can not be empty");
                }
                var People = new People {
                    Id=PeopleName.Trim().ToLower(),
                    Name = PeopleName,
                };
                var PeopleExisting = await PeopleRepository.GetPeopleByIdAsync(PeopleName);
                if (PeopleExisting != null)
                {
                    throw new Exception("This People has already existed!");
                }
                else
                {
                    await PeopleRepository.AddPeople(People);
                }
                return RedirectToAction(nameof(Index), new {message= SUCCESS });
            }
            catch(Exception ex)
            {
                return RedirectToAction(nameof(Index), new {message= ex.Message });
            }
        }

        // GET: PeoplesController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            try
            {
                var People = await PeopleRepository.GetPeopleByIdAsync(id);
                return View(People);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            
        }

        // POST: PeoplesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, People PeopleInput)
        {
            try
            {
                var People = await PeopleRepository.GetPeopleByIdAsync(PeopleInput.Name);
                if (People != null)
                {
                    throw new Exception("This People has already existed!");
                }
                await PeopleRepository.Update(PeopleInput);
                return RedirectToAction(nameof(Index), new {message= SUCCESS });
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Name", ex.Message);
                return View(PeopleInput);
            }
        }

        // GET: PeoplesController/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var People = await PeopleRepository.GetPeopleByIdAsync(id);
                return View(People);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        // POST: PeoplesController/Delete/5
        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(string id, People PeopleInput)
        {
            try
            {
                var People = await PeopleRepository.GetPeopleByIdAsync(id);
                await PeopleRepository.Delete(People);
                return RedirectToAction(nameof(Index), new { message = SUCCESS });
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(PeopleInput);
            }
        }
    }
}
