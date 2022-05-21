using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MidNite.CommonHelpers
{
    public class HelperFunction
    {
        public static bool GetImageExtension(string ex)
        {
            bool CheckExtension = false;
            string[] extension = { ".png", ".jpg", ".jpeg", ".jfif" };
            if (extension.Contains(ex))
            {
                CheckExtension = true;
            }
            return CheckExtension;
        }
    }
}
