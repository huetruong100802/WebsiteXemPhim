using DataAccess.Repository;
using DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MovieWebsite.Controllers;

namespace MovieWebsite.Filters
{
    public class GlobalFilter : ActionFilterAttribute
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMovieRepository _movieRepository;
        public GlobalFilter()
        {
            _genreRepository = new GenreRepository();
            _movieRepository = new MovieRepository();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as Controller;
            if(controller != null)
            {
                var user = controller.HttpContext.User;
                controller.ViewData["GenreNavBar"] = _genreRepository.GetGenres().ToList();
                controller.ViewData["NationNavBar"]= _movieRepository.GetMovies().Where(m=>m.Nation!=null).Select(m=>m.Nation).Distinct().ToList();
                controller.ViewData["FollowedMovies"]=_movieRepository.GetMoviesByUserName(user.Identity.Name!).Distinct().ToList() ;
            }
            base.OnActionExecuting(context);
        }

    }
}
