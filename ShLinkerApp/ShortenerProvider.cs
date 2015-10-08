using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShLinkerApp
{
    public abstract class ShortenerProvider
    {
        public abstract string GetShortUrl(string longUrl, string userName="", string apiKey="");
    }
}
