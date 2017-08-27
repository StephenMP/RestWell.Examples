using System.Collections.Generic;

namespace DependencyInjection.Models
{
    public class HomeModel
    {
        public IEnumerable<string> ValuesFromInjectedProxy { get; set; }
        public IEnumerable<string> ValuesFromInjectedProxyConfiguration { get; set; }
    }
}