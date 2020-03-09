using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.Infrastructure.Ulitily.Security
{
    public class HashAlgorithm
    {
        public static string Sha256(string source)
        {
            var bytes = Encoding.UTF8.GetBytes(source);
            var hashstring = new SHA256Managed();
            var hash = hashstring.ComputeHash(bytes);
            return hash.Aggregate(string.Empty, (current, x) => current + string.Format("{0:x2}", x));
        }
    }
}
