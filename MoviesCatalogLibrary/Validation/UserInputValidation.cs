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
    }
}
