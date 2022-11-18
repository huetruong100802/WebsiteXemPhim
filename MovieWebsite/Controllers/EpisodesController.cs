using BusinessObject.Models;
using DataAccess.Repository;
using DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieWebsite.Models.Episodes;

namespace MovieWebsite.Controllers
{
    public class EpisodesController : BaseController
    {
        private IEpisodeRepository episodeRepository;
        private IWebHostEnvironment _environment;
        public EpisodesController(IWebHostEnvironment environment)
        {
            episodeRepository = new EpisodeRepository();
            _environment = environment;
        }

        // GET: EpisodesController
        public ActionResult Index(string id,string? message)
        {
            var episodeList=episodeRepository.GetEpisodes().Where(e=>e.MovieId==id);
            ViewBag.MovieId = id;
            ViewBag.Message = message;
            return View(episodeList);
        }

        // GET: EpisodesController/Details/5
        public async Task<IActionResult> DetailsAsync(string id)
        {
            var episode=await episodeRepository.GetEpisodeByIdAsync(id);
            if (episode == null)
            {
                return NotFound();
            }
            return View(episode);
        }

        // GET: EpisodesController/Create
        public ActionResult Create(string id)
        {
            var episodeInput = new EpisodeInputModel {
                MovieId=id,
            };
            return View(episodeInput);
        }

        // POST: EpisodesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(EpisodeInputModel episodeInput)
        {
            try
            {
                string epId=episodeInput.MovieId+"_"+episodeInput.Title;
                Episode ep = new()
                {
                    Id = epId,
                    MovieId=episodeInput.MovieId,
                    Title=episodeInput.Title,
                };
                if (episodeInput.VideoFile != null)
                {
                    ep.VideoName = await GetFileNameAsync(episodeInput.VideoFile, ep);
                }
                else
                {
                    ModelState.AddModelError("VideoFile", "This field must not be leave empty!");
                    throw new Exception();
                }
                await episodeRepository.AddEpisode(ep);
                return RedirectToAction(nameof(Index), new {id=ep.MovieId, message = "Create successfully" });
            }
            catch(FormatException fe)
            {
                ModelState.AddModelError("VideoFile", fe.Message);
                return View(episodeInput);
            }
            catch
            {
                return View(episodeInput);
            }
        }

        // GET: EpisodesController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var episode = await episodeRepository.GetEpisodeByIdAsync(id);
            if (episode == null)
            {
                return NotFound();
            }
            var episodeInput = new EpisodeInputModel
            {
                Id=episode.Id,
                MovieId=episode.MovieId,
                Title=episode.Title,
                VideoName=episode.VideoName!,
            };
            return View(episodeInput);
        }

        // POST: EpisodesController/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(EpisodeInputModel episodeInput)
        {
            try
            {
                Episode ep =await episodeRepository.GetEpisodeByIdAsync(episodeInput.Id!);
                ep.Title = episodeInput.Title;
                if (episodeInput.VideoFile != null)
                {
                    RemoveVideoFile(episodeInput.VideoName, ep);
                    ep.VideoName = await GetFileNameAsync(episodeInput.VideoFile, ep);
                }
                else
                {
                    ep.VideoName = episodeInput.VideoName;
                }
                await episodeRepository.Update(ep);
                return RedirectToAction(nameof(Index), new { id = ep.MovieId, message = "Edit successfully" });
            }
            catch (FormatException fe)
            {
                ModelState.AddModelError("VideoFile", fe.Message);
                return View(episodeInput);
            }
            catch
            {
                return View(episodeInput);
            }
        }

        // GET: EpisodesController/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var episode = await episodeRepository.GetEpisodeByIdAsync(id);
            if (episode == null)
            {
                return NotFound();
            }
            return View(episode);
        }

        // POST: EpisodesController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(string id,string movieId)
        {
            try
            {
                var episode = await episodeRepository.GetEpisodeByIdAsync(id);
                if (episode == null)
                {
                    return NotFound();
                }
                RemoveVideoFile(episode.VideoName, episode);
                await episodeRepository.Delete(episode);
                return RedirectToAction(nameof(Index), new { id = movieId, message = "Delete successfully" });
            }
            catch
            {
                return RedirectToAction(nameof(Index), new { id = movieId, message = "Delete Fail" });
            }
        }
        
        //--------------------------------------------------------------------------------------
        private async Task<string> GetFileNameAsync(IFormFile file, Episode episode)
        {
            string wwwPath = _environment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
            string path = wwwPath + "/video/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path += $"/{episode.MovieId}/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string extension = Path.GetExtension(file.FileName);
            if (!extension.Equals(".mp4"))
            {
                throw new FormatException("Can only accept .mp4 file at the momment");
            }
            fileName =episode.Id+ fileName + DateTime.Now.ToString("yymmssfff") + extension;
            path = Path.Combine(path, fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return fileName;
        }
        private void RemoveVideoFile(string? videoName, Episode episode)
        {
            if (!String.IsNullOrEmpty(videoName))
            {
                string path = Path.Combine(_environment.WebRootPath, "video",episode.MovieId, videoName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
        }
    }
}
