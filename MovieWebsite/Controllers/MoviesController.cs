using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Repository;
using MovieWebsite.Models.Movies;
using DataAccess.Repository.Interface;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.Extensions;
using MovieWebsite.Enums;

namespace MovieWebsite.Controllers
{
    public class MoviesController : BaseController
    {
        private readonly IMovieRepository movieRepository=null!;
        private readonly IGenreRepository genreRepository = null!;
        private readonly IMovieGenreRepository movieGenreRepository = null!;
        private readonly IApplicationUserRepository applicationUserRepository = null!;
        private readonly ICommentRepository commentRepository = null!;
        private readonly IEpisodeRepository episodeRepository;
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRoleRepository roleRepository;
        private readonly IPeopleRepository peopleRepository;
        private readonly IMoviePeopleRepository moviePeopleRepository;
        private readonly IMovieStatusRepository movieStatusRepository;
        private readonly IStatusRepository statusRepository;
        private readonly IRateRepository rateRepository;
        public MoviesController(IWebHostEnvironment environment, UserManager<ApplicationUser> userManager)
        {
            movieRepository = new MovieRepository();
            genreRepository = new GenreRepository();
            movieGenreRepository = new MovieGenreRepository();
            applicationUserRepository = new ApplicationUserRepository();
            commentRepository = new CommentRepository();
            episodeRepository = new EpisodeRepository();
            moviePeopleRepository= new MoviePeopleRepository();
            peopleRepository = new PeopleRepository();
            roleRepository = new RoleRepository();
            movieStatusRepository= new MovieStatusRepository();
            statusRepository= new StatusRepository();
            rateRepository= new RateRepository();
            _environment = environment;
            _userManager = userManager;
        }
        [AllowAnonymous]
        // GET: MoviesController1
        public async Task<IActionResult> Index(string? searchString, string? genreName, string? message, string? nation)
        {
            List<MovieViewModel> model = new();
            try
            {
                IEnumerable<Movie> movies=movieRepository.GetMovies().ToList();
                IEnumerable<Movie> searchResult;
                IEnumerable<Movie> filterResult;
                IEnumerable<Movie> nationResult;
                if (searchString != null)
                {
                    searchResult = movieRepository.GetMoviesByKeyWord(searchString).ToList();
                    ViewBag.SearchString = searchString;
                    movies=movies.IntersectBy(second:searchResult.Select(m=>m.Id), m => m.Id);
                }
                if (genreName != null)
                {
                    filterResult = movieRepository.GetMoviesByGenre(genreName).ToList();
                    movies = movies.IntersectBy(second: filterResult.Select(m => m.Id), m => m.Id);
                }
                if (nation != null)
                {
                    nationResult = movieRepository.GetMovies().Where(m => m.Nation == nation).ToList();
                    movies = movies.IntersectBy(second: nationResult.Select(m => m.Id), m => m.Id);
                }
                if (movies.Any())
                {
                    foreach (var movie in movies)
                    {
                        var viewModel =GenerateMovieViewModel(movie);
                        model.Add(viewModel);
                    }
                }
                else
                {
                    throw new Exception("Sorry, we could not find your movie!");
                }
                ViewBag.Message = message;
            }
            catch (Exception ex)
            {
                ViewBag.Message=ex.Message;
            }
            return View(model);
        }
        [AllowAnonymous]
        // GET: MoviesController1/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (movieRepository.GetMovies() == null)
            {
                return View(null);
            }
            var movie = await movieRepository.GetMovieByIdAsync(id);
            var count = movie.Rates.Count;
            if (count > 0)
            {
                movie.Rating = (float)movie.Rates.Select(m => m.Rating).Sum()! /count;
                await movieRepository.Update(movie);
            }
            var movieView =GenerateMovieViewModel(movie);
            ViewBag.MovieRoles = roleRepository.GetRoles();
            return View(movieView);
        }

        // GET: MoviesController1/Create
        public IActionResult Create()
        {
            var movieInput = GenerateMovieInputModel();
            return View(movieInput);
        }

        // POST: MoviesController1/Create
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(MovieInputModel movieInput)
        {
            try
            {
                movieInput.Id = movieInput.Title.Trim().ToLower();
                if (await ExistedTitleAsync(movieInput.Id))
                {
                    var movie = new Movie
                    {
                        Title = movieInput.Title,
                        Id = movieInput.Id,
                        Description = movieInput.Description,
                        ReleaseYear = movieInput.ReleaseYear,
                        Duration = movieInput.Duration,
                        Nation = movieInput.Nation,
                    };
                    if (movieInput.ImageFile != null)
                    {
                        movie.ImageName = await GetFileNameAsync(movieInput.ImageFile);
                    }
                    else
                    {
                        ModelState.AddModelError("ImageFile", "This field must not be leave empty!");
                        throw new Exception();
                    }
                    await movieRepository.AddMovie(movie);
                    await AddSelectedAsync(movieInput);
                }
                return RedirectToAction(nameof(Index), new { message = "Create successfully" });
            }
            catch
            {
                movieInput =GenerateMovieInputModel();
                return View(movieInput);
            }
        }

        // GET: MoviesController1/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            //find movie
            var movie = await movieRepository.GetMovieByIdAsync(id);
            MovieInputModel inputModel;
            var genres = new List<ManageMovieGenreViewModel>();
            var moviePeople = new List<ManageMoviePeopleModel>();
            var movieStatus = new List<ManageMovieStatusModel>();
            if (movie == null)
            {
                return NotFound();
            }
            inputModel = GenerateMovieInputModel(movie);
            ViewBag.MovieRoles=roleRepository.GetRoles();
            //set datalist
            return View(inputModel);
        }

        // POST: MoviesController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MovieInputModel movieInput)
        {
            try
            {
                var movie = await movieRepository.GetMovieByIdAsync(movieInput.Id);
                if (movie == null)
                {
                    return NotFound();
                }
                //update movie----------------------------
                movie.Title = movieInput.Title;
                movie.Description = movieInput.Description;
                movie.ReleaseYear = movieInput.ReleaseYear;
                movie.Duration=movieInput.Duration;
                movie.Nation = movieInput.Nation;
                if (movieInput.ImageFile != null)
                {
                    RemoveImageFile(movie.ImageName);
                    movie.ImageName = await GetFileNameAsync(movieInput.ImageFile);
                }
                await movieRepository.Update(movie);
                //add selected genre-----------------------
                var movieGenres = movieGenreRepository.GetMovieGenresByMovieId(movieInput.Id);
                await movieGenreRepository.DeleteRange(movieGenres);
                var genres = movieInput.Genres.Where(m => m.Selected).Select(m => m.Name);
                foreach (var genreName in genres)
                {
                    var genre = await genreRepository.GetGenreByNameAsync(genreName);
                    var movieGenre = new MovieGenre
                    {
                        GenreId = genre.Id,
                        MovieId = movie.Id,
                        Id = movie.Id + genre.Id,
                    };
                    await movieGenreRepository.AddMovieGenre(movieGenre);
                }
                //add selected people----------------------
                var removedPeople= moviePeopleRepository.GetMoviePeople().Where(m=>m.MovieId==movieInput.Id).ToList();
                await moviePeopleRepository.DeleteRange(removedPeople);
                var peopleList = movieInput.MoviePeople.Where(m => m.Selected);
                foreach(var p in peopleList)
                {
                    var people = await peopleRepository.GetPeopleByIdAsync(p.Name);
                    var moviePeople = new MoviePeople
                    {
                        Id = movie.Id + people.Id+ p.RoleId,
                        MovieId = movie.Id,
                        PeopleId = people.Id,
                        RoleId = p.RoleId,
                    };
                    await moviePeopleRepository.AddMoviePeople(moviePeople);
                }
                //add selected status-----------------------
                var removedStatus = movieStatusRepository.GetMovieStatusesByMovieId(movieInput.Id).ToList();
                await movieStatusRepository.DeleteRange(removedStatus);
                var statuses = movieInput.MovieStatuses.Where(m => m.Selected).Select(m => m.Name);
                foreach (var statusName in statuses)
                {
                    var status = await statusRepository.GetStatusByNameAsync(statusName);
                    var movieStatus = new MovieStatus
                    {
                        MovieId=movieInput.Id,
                        StatusId=status.Id,
                        Id = movie.Id + status.Id,
                    };
                    await movieStatusRepository.AddMovieStatus(movieStatus);
                }
                return RedirectToAction(nameof(Index), new { message = "Edit successfully" });
            }
            catch
            {
                SetDatalistForMovieInput();
                return View(movieInput);
            }
        }

        // GET: MoviesController1/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (movieRepository.GetMovies() == null)
            {
                return View(null);
            }
            var movie = await movieRepository.GetMovieByIdAsync(id);
            var movieView = GenerateMovieViewModel(movie);
            ViewBag.MovieRoles = roleRepository.GetRoles();
            return View(movieView);
        }

        // POST: MoviesController1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(string Id)
        {
            try
            {
                var movie = await movieRepository.GetMovieByIdAsync(Id);
                if(movie == null)
                {
                    return NotFound();
                }
                RemoveVideoDirectory(movie.Id);
                RemoveImageFile(movie.ImageName);
                await movieRepository.Delete(movie);
                return RedirectToAction(nameof(Index), new { message = "Delete successfully" });
            }
            catch
            {
                return RedirectToAction(nameof(Index), new { message = "Delete fail" });
            }
        }
        // POST: MoviesController1/Remove
        public async Task<IActionResult> Remove(string id)
        {
            try
            {
                var movie = await movieRepository.GetMovieByIdAsync(id);
                if (movie == null)
                {
                    return NotFound();
                }
                //RemoveImageFile(movieInput.ImageName);
                if (movie.Deleted)
                {
                    _ = movieRepository.Unhide(movie);
                }
                else
                {
                    _ = movieRepository.Hide(movie);
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> AddComment(string movieId, string commentDetail, string currentUrl)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                Comment comment = new()
                {
                    CommentDetail = commentDetail,
                    CreatedDate = DateTime.Now,
                    MovieId = movieId,
                    Id = GenerateID(10),
                };
                var appUser = await applicationUserRepository.GetApplicationUserByIdAsync(user.Id);
                if (appUser != null)
                {
                    comment.UserId = user.Id;
                }
                else
                {
                    comment.User = user;
                }
                await commentRepository.AddComment(comment);
                return Redirect(currentUrl);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [AllowAnonymous]
        public async Task<IActionResult> ViewMovie(string id) {
            if (movieRepository.GetMovies() == null)
            {
                return View(null);
            }
            var episode = await episodeRepository.GetEpisodeByIdAsync(id);
            if (episode == null)
            {
                return NotFound();
            }
            var movie = await movieRepository.GetMovieByIdAsync(episode.MovieId);
            var movieView = GenerateMovieViewModel(movie);
            movieView.EpisodePath= episode.VideoName;
            //
            return View(movieView);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> CountView()
        {
            string id = Request.Form["id"];
            var movie = await movieRepository.GetMovieByIdAsync(id);
            movie.Views++;
            await movieRepository.Update(movie);
            string msg = "Thank you for watching";
            return Json(msg);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Rating()
        {
            try
            {
                string movieId = Request.Form["id"];
                int rating = Int32.Parse(Request.Form["rating"]);
                var movie = await movieRepository.GetMovieByIdAsync(movieId);
                var user = await _userManager.GetUserAsync(User);
                string id = movie.Id + user.Id;
                Rate rate = await rateRepository.GetRateByIdAsync(id);
                if (rate != null)
                {
                    await rateRepository.Delete(rate);
                }
                rate = new()
                {
                    Id = id,
                    Rating = rating,
                    MovieId = movieId,
                };
                var appUser = await applicationUserRepository.GetApplicationUserByIdAsync(user.Id);
                if (appUser != null)
                {
                    rate.UserId = user.Id;
                }
                else
                {
                    rate.User = user;
                }
                await rateRepository.AddRate(rate);
            }
            catch
            {
                throw new Exception("Weird exception");
            }
            return Json("Thanks");
        }
        //--------------------------------------------------------------------------------------
        private async Task<string> GetFileNameAsync(IFormFile file)
        {
            string wwwPath = _environment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
            string path = wwwPath+ "/image/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string extension = Path.GetExtension(file.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            path = Path.Combine(path, fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return fileName;
        }
        private void RemoveImageFile(string? imageName)
        {
            if (!String.IsNullOrEmpty(imageName))
            {
                string path =Path.Combine(_environment.WebRootPath,"image",imageName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
        }
        private void RemoveVideoDirectory(string movieId)
        {
            string path = Path.Combine(_environment.WebRootPath, "video", movieId);
            if (System.IO.Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                foreach (string file in files)
                {
                    System.IO.File.Delete(file);
                }
                System.IO.Directory.Delete(path);
            }
        }
        private async Task<bool> ExistedTitleAsync(string title)
        {
            var movie = await movieRepository.GetMovieByIdAsync(title);
            if (movie != null)
            {
                ModelState.AddModelError("Title", "This title has alreay existed!");
                throw new Exception();
            }
            return true;
        }
        private MovieViewModel GenerateMovieViewModel(Movie movie)
        {
            return new MovieViewModel
            {
                Id = movie.Id,
                ImageName = movie.ImageName,
                Title = movie.Title,
                Rates= movie.Rates,
                Rating= movie.Rating,
                Deleted = movie.Deleted,
                Description = movie.Description,
                ReleaseYear = movie.ReleaseYear,
                Duration = movie.Duration,
                Nation= movie.Nation,
                Episodes= movie.Episodes,
                MoviePeople=moviePeopleRepository.GetMoviePeoplesByMovieId(movie.Id),
                Comments = commentRepository.GetCommentsByMovieId(movie.Id).OrderByDescending(m=>m.CreatedDate),
                Genres = genreRepository.GetGenresByMovieId(movie.Id).Select(m => m.Name),
                Statuses= statusRepository.GetStatusesByMovieId(movie.Id).Select(m=>m.Name),
                Views=movie.Views,
                movieSuggests= MovieSuggest(movie),
             };
        }
        private MovieInputModel GenerateMovieInputModel(Movie movie)
        {
            var genres = new List<ManageMovieGenreViewModel>();
            var moviePeople = new List<ManageMoviePeopleModel>();
            var movieStatus = new List<ManageMovieStatusModel>();
            //genre list
            foreach (var genre in genreRepository.GetGenres())
            {
                var manageMovieGenreViewModel = new ManageMovieGenreViewModel
                {
                    Name = genre.Name,
                    Selected = false,
                };
                if (movieRepository.MovieIsInGenre(movie, genre.Name))
                {
                    manageMovieGenreViewModel.Selected = true;
                }
                genres.Add(manageMovieGenreViewModel);
            }
            //MoviePeople list
            foreach (var people in peopleRepository.GetPeoples())
            {
                foreach (var role in roleRepository.GetRoles())
                {
                    var manageMoviePeopleModel = new ManageMoviePeopleModel
                    {
                        Name = people.Name,
                        Selected = false,
                        RoleId = role.Id,
                    };
                    if (peopleRepository.PeopleIsInMovie(movie, people.Name) && peopleRepository.PeopleIsInRole(role, people.Name))
                    {
                        manageMoviePeopleModel.Selected = true;
                    }
                    moviePeople.Add(manageMoviePeopleModel);
                }
            }
            //MovieStatus list
            foreach (var status in statusRepository.GetStatuses())
            {
                var manageMovieStatusModel = new ManageMovieStatusModel
                {
                    Name = status.Name,
                    Selected = false,
                };
                if (movieRepository.MovieIsInStatus(movie, status.Name))
                {
                    manageMovieStatusModel.Selected = true;
                }
                movieStatus.Add(manageMovieStatusModel);
            }
            SetDatalistForMovieInput();
            ViewBag.MovieRoles = roleRepository.GetRoles();
            if(movie is null)
            {
                return new MovieInputModel
                {
                    Genres = genres,
                    MoviePeople = moviePeople,
                    MovieStatuses = movieStatus,
                };
            }
            else
            {
                return new MovieInputModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    ImageName = movie.ImageName,
                    Genres = genres,
                    Description = movie.Description,
                    ReleaseYear = movie.ReleaseYear,
                    Duration = movie.Duration,
                    Nation = movie.Nation,
                    MovieStatuses = movieStatus,
                    MoviePeople = moviePeople
                };
            }
        }
        private MovieInputModel GenerateMovieInputModel() => GenerateMovieInputModel(null!);
        private List<MovieSuggestModel> MovieSuggest(Movie movieInput)
        {
            //get movies by the same genres
            var genres = genreRepository.GetGenresByMovieId(movieInput.Id);
            List<Movie> genreMovies = new();
            foreach(var genre in genres)
            {
                var movies = movieRepository.GetMoviesByGenre(genre.Name).Where(m=>m.Id!=movieInput.Id);
                genreMovies.AddRange(movies);
            }
            //get movies by the same people involve
            var peopleInvovle = peopleRepository.GetPeopleByMovieId(movieInput.Id);
            List<Movie> peopleMovies = new();
            foreach(var person in peopleInvovle)
            {
                var movies = moviePeopleRepository.GetMoviePeople()
                    .Where(m => m.MovieId != movieInput.Id)
                    .Where(m => m.PeopleId == person.Id).Select(m=>m.Movie);
                peopleMovies.AddRange(movies);
            }

            return new()
            {
                new MovieSuggestModel
                {
                    Title="Phim cùng thể loại",
                    MoviesSuggestion=genreMovies.DistinctBy(m=>m.Id).OrderBy(p => Guid.NewGuid()).Take(5).ToList(),
                },
                new MovieSuggestModel{
                    Title="Phim cùng diễn viên, đạo diễn",
                    MoviesSuggestion=peopleMovies.DistinctBy(m=>m.Id).OrderBy(p => Guid.NewGuid()).Take(5).ToList(),
                }
            };
        }
        private async Task AddSelectedAsync(MovieInputModel movieInput)
        {
            //add selected genre
            var genres = movieInput.Genres.Where(m => m.Selected).Select(m => m.Name);
            foreach (var genreName in genres)
            {
                var genre = await genreRepository.GetGenreByNameAsync(genreName);
                var movieGenre = new MovieGenre
                {
                    GenreId = genre.Id,
                    MovieId = movieInput.Id,
                    Id = movieInput.Id + genre.Id,
                };
                await movieGenreRepository.AddMovieGenre(movieGenre);
            }
            //add people involved
            var peopleList = movieInput.MoviePeople.Where(m => m.Selected);
            foreach (var p in peopleList)
            {
                var people = await peopleRepository.GetPeopleByIdAsync(p.Name);
                var moviePeople = new MoviePeople
                {
                    Id = movieInput.Id + people.Id + p.RoleId,
                    MovieId = movieInput.Id,
                    PeopleId = people.Id,
                    RoleId = p.RoleId,
                };
                await moviePeopleRepository.AddMoviePeople(moviePeople);
            }
            //add movie status
            var statuses = movieInput.MovieStatuses.Where(m => m.Selected).Select(m => m.Name);
            foreach (var statusName in statuses)
            {
                var status = await statusRepository.GetStatusByNameAsync(statusName);
                var movieStatus = new MovieStatus
                {
                    MovieId = movieInput.Id,
                    StatusId = status.Id,
                    Id = movieInput.Id + status.Id,
                };
                await movieStatusRepository.AddMovieStatus(movieStatus);
            }
        }
        private void SetDatalistForMovieInput()
        {
            var releaseYear=movieRepository.GetMovies().Select(m => m.ReleaseYear).ToList();
            var nation = movieRepository.GetMovies().Select(m => m.Nation).Distinct().ToList();
            ViewBag.ReleaseYear=releaseYear;
            ViewBag.Nation=nation;
        }
        protected string GenerateID(int length)
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = numbers;
            characters += alphabets + small_alphabets + numbers;
            string id = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (id.IndexOf(character) != -1);
                id += character;
            }
            return id;
        }
    }
}
