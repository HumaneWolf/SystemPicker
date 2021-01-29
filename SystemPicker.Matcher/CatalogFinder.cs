using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SystemPicker.Matcher.Models;

namespace SystemPicker.Matcher
{
    public class CatalogFinder
    {
        private static readonly List<StarCatalog> Catalogs = new()
        {
            new StarCatalog
            {
                Name = "Henry Draper Catalogue",
                Pattern = "HD 1234",
                Regex = @"\bHD [0-9]+\b"
            },
            new StarCatalog
            {
                Name = "Hipparcos Catalogue",
                Pattern = "HIP 1234",
                Regex = @"\bHIP [0-9]+\b"
            },
            new StarCatalog
            {
                Name = "Gliese Catalogue of Nearby Stars",
                Pattern = "Gliese 110 or Gliese 105.2",
                Regex = @"\bGliese [0-9]+(.[0-9]+)?\b"
            }
        };
        
        public List<string> GenerateCatalogRegex()
        {
            return Catalogs.Select(x => x.Regex).ToList();
        }

        public bool IsCatalogSystem(string name)
        {
            return GenerateCatalogRegex()
                .Select(r => new Regex($@"^{r}$", RegexOptions.Compiled | RegexOptions.IgnoreCase))
                .Any(x => x.IsMatch(name));
        }
    }
}