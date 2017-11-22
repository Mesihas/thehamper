using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HamperWeb.Services
{
    public static class FileNameHelper
    {
        public static string GetNameFormat(string str)
        {
            //remove all spaces
            var tokens = str.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tokens.Length; i++)
            {
                var token = tokens[i];
                tokens[i] = token.Substring(0, 1).ToUpper() + token.Substring(1).ToLower();
            }
            return string.Join("", tokens);
        }
    }
}
