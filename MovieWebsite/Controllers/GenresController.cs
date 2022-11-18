using BusinessObject.Models;
using DataAccess.Repository;
using DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieWebsite.Controllers
{

    public class GenresController : BaseController
    {

        IGenreRepository genreRepository = null!;
        private static readonly string SUCCESS="Action complete successfully!";

        public GenresController()
        {
            genreRepository = new GenreRepository();
        }

        // GET: GenresController
        public ActionResult Index(string? message)
        {
            var genres= genreRepository.GetGenres();
            ViewBag.Message = message;
            return View(genres);
        }

        // GET: GenresController/Details/5
        public ActionResult Details(int id)
        {
            return RedirectToAction(nameof(Index));
        }

        // POST: GenresController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string genreName)
        {
            try
            {
                if (String.IsNullOrEmpty(genreName))
                {
                    throw new Exception("Genre name can not be empty");
                }
                var genre = new Genre {
                    Id=genreName.Trim().ToLower(),
                    Name = genreName,
                };
                var genreExisting = await genreRepository.GetGenreByNameAsync(genreName);
                if (genreExisting != null)
                {
                    throw new Exception("This genre has already existed!");
                }
                else
                {
                    await genreRepository.AddGenre(genre);
                }
                return RedirectToAction(nameof(Index), new {message= SUCCESS });
            }
            catch(Exception ex)
            {
                return RedirectToAction(nameof(Index), new {message= ex.Message });
            }
        }

        // GET: GenresController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            try
            {
                var genre = await genreRepository.GetGenreByIdAsync(id);
                return View(genre);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            
        }

        // POST: GenresController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, Genre genreInput)
        {
            try
            {
                var genre = await genreRepository.GetGenreByNameAsync(genreInput.Name);
                if (genre != null)
                {
                    throw new Exception("This genre has already existed!");
                }
                await genreRepository.Update(genreInput);
                return RedirectToAction(nameof(Index), new {message= SUCCESS });
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Name", ex.Message);
                return View(genreInput);
            }
        }

        // GET: GenresController/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var genre = await genreRepository.GetGenreByIdAsync(id);
                return View(genre);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        // POST: GenresController/Delete/5
        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(string id, Genre genreInput)
        {
            try
            {
                var genre = await genreRepository.GetGenreByIdAsync(id);
                await genreRepository.Delete(genre);
                return RedirectToAction(nameof(Index), new { message = SUCCESS });
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(genreInput);
            }
        }
    }
}
