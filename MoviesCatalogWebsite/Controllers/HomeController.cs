using Microsoft.AspNetCore.Mvc;
using MoviesCatalogLibrary.APIAccess;
using MoviesCatalogLibrary.Models;
using MoviesCatalogLibrary.Validation;
using MoviesCatalogWebsite.Models;
using System.Diagnostics;

namespace MoviesCatalogWebsite.Controllers
{
    public class HomeController : Controller
    {
        private ControllerOperations operations;

        public HomeController()
        {
            operations = new ControllerOperations();
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> MoviesIndex()
        {
            var moviesList = await operations.ShowTopTenMovies();

            return View(moviesList);
        }

        [HttpGet("Home/FullDetailsMovie/{title}")]

        public async Task<IActionResult> FullDetailsMovie(string title)
        {
            var movieDetails = await operations.ShowClickedMovie(title);

            return View(movieDetails);
        }

        public async Task<IActionResult> SearchMovie(string searchString)
        {
            if (UserInputValidation.SearchStringValidate(searchString) == false)
                return View("Error");

            var result = await operations.SearchForMovie(searchString);

            return View("FullDetailsMovie", result);
        }
    }
}