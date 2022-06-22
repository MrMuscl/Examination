using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.WEB.Utils
{
    public class Helper
    {
        public static bool GetFormIntValue(IFormCollection form, string key, out int value) 
        {
            bool res = true;
            StringValues svValue;

            res = form.TryGetValue(key, out svValue);
            res = int.TryParse(svValue.ToString(), out value);

            return res;
        }
    }
}
