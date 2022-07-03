using MoviesCatalogLibrary.Models;

namespace MoviesCatalogLibrary.APIAccess
{
    public class ControllerOperations
    {
        private readonly string[] topTenMoviesNames = new string[]
        {
            "Interstellar","Pulp Fiction", "The Godfather", "12 Angry Men", "Edge of Tomorrow", "Memento",
            "Lord of the Rings: The Fellowship of the ring", "Gladiator", "There will be Blood", "The Dark Knight"
        };

        private APIOperations operations { get; set; }

        public ControllerOperations()
        {
            operations = new APIOperations();
        }

        public async Task<List<MovieDetails>> ShowTopTenMovies()
        {
            List<MovieDetails> movies = new();
            for (int i = 0; i < topTenMoviesNames.Length; i++)
            {
                var requestUrl = operations.GetRequestedApiUrl("find", topTenMoviesNames[i]);
                var response = await operations.GetRequestedItem<MovieDetails>(requestUrl);

                if (response is null)
                    continue;

                movies.Add(response);
            }

            return movies;
        }

        public async Task<MovieDetails> ShowClickedMovie(string title)
        {
            var requestUrl = operations.GetRequestedApiUrl("find", title);
            var response = await operations.GetRequestedItem<MovieDetails>(requestUrl);

            return response;
        }

        public async Task<MovieDetails> SearchForMovie(string value)
        {
            var requestUrl = operations.GetRequestedApiUrl("search", value);
            var response = await operations.GetRequestedSerach<MovieDetails>(requestUrl);

            return await ShowClickedMovie(response.Title);
        }
    }
}
