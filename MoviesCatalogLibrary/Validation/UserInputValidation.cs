using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesCatalogLibrary.Validation
{
    public static class UserInputValidation
    {
        public static bool SearchStringValidate(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString) == true)
                return false;

            // check for the name in the api

            return true;
        }

        public static async Task<bool> CheckForSearchResults(string apiUrl, HttpResponseMessage message, HttpClient client)
        {
            //url can be the title or ImdbId or a searched value
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);

                //Read the Response
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode == true)
                    return true;
            }

            catch
            {
                
            }

            return false;
        }
    }
}
