using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leanPhotos.Logic.Helpers
{
    public static class InputValidation
    {
        public static bool IsAlbumQueryInputValid(string input)
        {
            // validate that the user is only inputting valid album integers
            int albumNumber = -1;
            if (int.TryParse(input, out albumNumber) && albumNumber > 0 && albumNumber <= 100)
            {
                return true;
            }

            return false;
        }
    }
}
