using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSystem.Security.Cryptography;

namespace Infrastructure
{
    public static class GeneratorHash
    {
        public static string GetHash(string text)
        {
            SHA512Managed algorithm = new SHA512Managed();
            algorithm.ComputeHash(Encoding.UTF8.GetBytes(text));
            var resultBytes = algorithm.Hash;
            var resultString = string.Join(
                            string.Empty,
                            resultBytes.Select(x => x.ToString("x2")));
            return resultString;
        }
    }
}
