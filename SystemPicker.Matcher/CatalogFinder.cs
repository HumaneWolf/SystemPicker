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
                Regex = @"\bGliese [0-9]+(\.[0-9]+)?\b"
            },
            new StarCatalog
            {
                Name = "Bright Star Catalogue",
                Pattern = "HR 1234",
                Regex = @"\bHR [0-9]+\b"
            },
            new StarCatalog
            {
                Name = "Luyten Five-Tenths Catalogue",
                Pattern = "LFT 1234",
                Regex = @"\bLFT [0-9]+\b"
            },
            new StarCatalog
            {
                Name = "Luyten Half-Second Catalogue",
                Pattern = "LHS 1234",
                Regex = @"\bLHS [0-9]+\b"
            },
            new StarCatalog
            {
                Name = "Luyten Two-Tenths Catalogue",
                Pattern = "LTT 1234",
                Regex = @"\bLTT [0-9]+\b"
            },
            new StarCatalog
            {
                Name = "New Luyten Two-Tenths Catalogue",
                Pattern = "NLTT 1234",
                Regex = @"\bNLTT [0-9]+\b"
            },
            new StarCatalog
            {
                Name = "Histoire Céleste Française",
                Pattern = "Lalande 1234",
                Regex = @"\bLalande [0-9]+\b"
            },
            new StarCatalog
            {
                Name = "Bonner Durchmusterung",
                Pattern = "BD+01 1234 or BD-12 1234",
                Regex = @"\bBD[+-][0-9]+ [0-9]+[A-Z]?\b"
            },
            new StarCatalog
            {
                Name = "Gliese Catalogue of Nearby Stars",
                Pattern = "Gl 123.45",
                Regex = @"\bGl [0-9]+(\.[0-9]+)?\b"
            },
            new StarCatalog
            {
                Name = "Luyten Proper-Motion catalogue",
                Pattern = "LP 1234 or LPM 1234",
                Regex = @"\b(LP|LPM) [0-9]+-[0-9]+\b"
            },
            new StarCatalog
            {
                Name = "Cordoba Durchmusterung",
                Pattern = "CD-12 123",
                Regex = @"\bCD-[0-9]+ [0-9]+\b"
            },
            new StarCatalog
            {
                Name = "Two Micron All-Sky Survey",
                Pattern = "2MASS J07464256+2000321 A or 2MASS 1503+2525",
                Regex = @"\b2MASS ([A-Z][0-9]+[\+-][0-9]+(?: [A-Z])?|[0-9]+[-+][0-9]+)\b"
            },
            new StarCatalog
            {
                Name = "The WISE Catalog of Galactic HII Regions V2.2",
                Pattern = "WISE 1503+2525 or WISE A123",
                Regex = @"\bWISE ([0-9]+[-+][0-9]+|[A-Z][0-9]+)\b"
            },
            new StarCatalog
            {
                Name = "Wolf Catalogues (multiple by Max Wolf)",
                Pattern = "Wolf 1478",
                Regex = @"\bWolf [0-9]+\b"
            },
            new StarCatalog
            {
                Name = "Ross Catalogues (multiple by Frank Elmore Ross)",
                Pattern = "Ross 1478",
                Regex = @"\bRoss [0-9]+\b"
            },
            new StarCatalog
            {
                Name = "Smithsonian Astrophysical Observatory Star Catalog",
                Pattern = "SAO 1478",
                Regex = @"\bSAO [0-9]+\b"
            },
            new StarCatalog
            {
                Name = "LAWD (?)",
                Pattern = "LAWD 1234",
                Regex = @"\bLAWD [0-9]+\b"
            },
            new StarCatalog
            {
                Name = "Unknown (?)",
                Pattern = "L 123-123",
                Regex = @"\b[A-Z] [0-9]+-[0-9]+\b"
            },
            new StarCatalog
            {
                Name = "Unknown (?)",
                Pattern = "MCC 123",
                Regex = @"\bMCC [0-9]+\b"
            },
            new StarCatalog
            {
                Name = "Unknown (?)",
                Pattern = "WD 1207-032",
                Regex = @"\WD [0-9]+-[0-9]\b"
            },
            new StarCatalog
            {
                Name = "Unknown (?)",
                Pattern = "GCRV 1234",
                Regex = @"\bGCRV [0-9]+\b"
            },
            new StarCatalog
            {
                Name = "Unknown (?)",
                Pattern = "CPC 19 205",
                Regex = @"\bCPC [0-9]+ [0-9]+\b"
            },
            new StarCatalog
            {
                Name = "Unknown (?)",
                Pattern = "CDP-76 796",
                Regex = @"\bCDP-[0-9]+ [0-9]+\b"
            },
            new StarCatalog
            {
                Name = "Unknown (?)",
                Pattern = "CD-24 13832D",
                Regex = @"\bCD-[0-9]+ [0-9]+[A-Z]?\b"
            },
            new StarCatalog
            {
                Name = "Unknown (?)",
                Pattern = "BPM 1234",
                Regex = @"\bBPM [0-9]+\b"
            },
            new StarCatalog
            {
                Name = "Unknown (?)",
                Pattern = "Smethells 118",
                Regex = @"\bSmethells [0-9]+\b"
            },
            new StarCatalog
            {
                Name = "Unknown (?)",
                Pattern = "S171 32",
                Regex = @"\bS[0-9]+ [0-9]+\b"
            },
            new StarCatalog
            {
                Name = "Unknown (?)",
                Pattern = "GD 118",
                Regex = @"\bGD [0-9]+\b"
            },
            new StarCatalog
            {
                Name = "Unknown (?)",
                Pattern = "GAT 118",
                Regex = @"\bGAT [0-9]+\b"
            },
            new StarCatalog
            {
                Name = "Unknown (?)",
                Pattern = "LDS 118",
                Regex = @"\bLDS [0-9]+\b"
            },
            new StarCatalog
            {
                Name = "Unknown (?)",
                Pattern = "LPM 118",
                Regex = @"\bLPM [0-9]+\b"
            },
            new StarCatalog
            {
                Name = "Unknown (?)",
                Pattern = "PW2010 118",
                Regex = @"\bPW2010 [0-9]+\b"
            },
        };
        
        // Combined regex
        private static readonly string CombinedRegex = $"(?:{string.Join("|", Catalogs.Select(c => c.Regex))})";
        
        // Complete regex
        public static Regex CatalogRegex = new Regex($@"\b{CombinedRegex}\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public static Regex FullStringRegex = new Regex($@"^{CombinedRegex}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public bool IsCatalogSystem(string name)
        {
            return FullStringRegex.IsMatch(name);
        }
    }
}