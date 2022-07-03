using JsonSerializer = System.Text.Json.JsonSerializer;
using Newtonsoft.Json.Linq;

namespace MoviesCatalogLibrary.APIAccess
{
    public class APIOperations
    {
        private readonly string apiUrl = @"https://omdbapi.com/?apikey=67de68cc&type=movie&";

        private readonly HttpClient client;


        public APIOperations()
        {
            client = new HttpClient();
        }


        public async Task<T> GetRequestedItem<T>(string url)
        {
            var json = await GetResultFromAPI(url);

            var result = await JsonSerializer.DeserializeAsync<T>(json);

            return result;
        }

        private async Task<Stream> GetResultFromAPI(string apiUrl)
        {
            //url can be the title or ImdbId or a searched value
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);

                //Read the Response
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode == true)
                {
                    var content = await response.Content.ReadAsStreamAsync();

                    return content;
                }

                throw new Exception();
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<T> GetRequestedSerach<T>(string apiUrl)
        {
            var jsonString = await GetSearchResultsFromAPI(apiUrl);

            if (jsonString.Contains("Movie not found!") == true)
                throw new Exception();

            JObject converter = JObject.Parse(jsonString);

            var result = converter["Search"][0].ToObject<T>();

            //var results = JsonConvert.DeserializeObject<T[]>(jsonString);

            return result;
        }

        private async Task<string> GetSearchResultsFromAPI(string apiUrl)
        {
            //url can be the title or ImdbId or a searched value
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);

                //Read the Response
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode == true)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    return content;
                }

                throw new Exception();
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GetRequestedApiUrl(string choice, string value)
        {
            switch (choice)
            {
                case "find":
                    return $"{apiUrl}t={value}";

                case "search":
                    return $"{apiUrl}s={value}&page=1";

                default: 
                    return string.Empty;
            }
        }
    }
}