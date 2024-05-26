using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Bee Movie" };
            List<Customer> customers = new List<Customer>
            {
                new Customer{Name = "Albert Abercromby"},
                new Customer{Name = "Bethany Bellatrix"}
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };
            return View(viewModel);

            //return HttpNotFound();
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home",new { page =1, sortBy ="name"});
        }
        public ActionResult Edit(int id)
        {
            return Content("id=" + id);
        }
        public ActionResult Index()
        {
            var movies = GetMovies();

            return View(movies);

        }
        private IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>
            {
            new Movie {ID = 1, Name = "Bee Movie" },
            new Movie {ID = 2, Name = "IronMan" }
            };

        }
        [Route("movies/released/{year}/{month:regex(\\d{4}):range(1,12)}")]
        public ActionResult ByReleaseDate( int year, int month)
        {
            return Content(year + "/" + month);
        }
    }
}