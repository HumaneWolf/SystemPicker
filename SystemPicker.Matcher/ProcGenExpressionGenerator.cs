﻿using System.Collections.Generic;

namespace SystemPicker.Matcher
{
    public class ProcGenExpressionGenerator
    {
        // Based on info from http://disc.thargoid.space/Sector_Naming
        
        // Components.
        private static readonly List<string> Prefixes = new()
        {
            "Th", "Eo", "Oo", "Eu", "Tr", "Sly", "Dry", "Ou", "Tz", "Phl", "Ae", "Sch", "Hyp", "Syst", "Ai", "Kyl",
            "Phr", "Eae", "Ph", "Fl", "Ao", "Scr", "Shr", "Fly", "Pl", "Fr", "Au", "Pry", "Pr", "Hyph", "Py", "Chr",
            "Phyl", "Tyr", "Bl", "Cry", "Gl", "Br", "Gr", "By", "Aae", "Myc", "Gyr", "Ly", "Myl", "Lych", "Myn", "Ch",
            "Myr", "Cl", "Rh", "Wh", "Pyr", "Cr", "Syn", "Str", "Syr", "Cy", "Wr", "Hy", "My", "Sty", "Sc", "Sph",
            "Spl", "A", "Sh", "B", "C", "D", "Sk", "Io", "Dr", "E", "Sl", "F", "Sm", "G", "H", "I", "Sp", "J", "Sq",
            "K", "L", "Pyth", "M", "St", "N", "O", "Ny", "Lyr", "P", "Sw", "Thr", "Lys", "Q", "R", "S", "T", "Ea", "U",
            "V", "W", "Schr", "X", "Ee", "Y", "Z", "Ei", "Oe",
        };

        private static readonly List<string> Infixes1 = new()
        {
            "o", "ai", "a", "oi", "ea", "ie", "u", "e", "ee", "oo", "ue", "i", "oa", "au", "ae", "oe"
        };

        private static readonly List<string> Infixes2 = new()
        {
            "ll", "ss", "b", "c", "d", "f", "dg", "g", "ng", "h", "j", "k", "l", "m", "n", "mb", "p", "q", "gn", "th",
            "r", "s", "t", "ch", "tch", "v", "w", "wh", "ck", "x", "y", "z", "ph", "sh", "ct", "wr"
        };

        private static readonly List<string> Suffixes1 = new()
        {
            "oe", "io", "oea", "oi", "aa", "ua", "eia", "ae", "ooe", "oo", "a", "ue", "ai", "e", "iae", "oae", "ou",
            "uae", "i", "ao", "au", "o", "eae", "u", "aea", "ia", "ie", "eou", "aei", "ea", "uia", "oa", "aae", "eau",
            "ee"
        };

        private static readonly List<string> Suffixes2 = new()
        {
            "b", "scs", "wsy", "c", "d", "vsky", "f", "sms", "dst", "g", "rb", "h", "nts", "ch", "rd", "rld", "k",
            "lls", "ck", "rgh", "l", "rg", "m", "n", "hm", "p", "hn", "rk", "q", "rl", "r", "rm", "s", "cs", "wyg",
            "rn", "ct", "t", "hs", "rbs", "rp", "tts", "v", "wn", "ms", "w", "rr", "mt", "x", "rs", "cy", "y", "rt",
            "z", "ws", "lch", "my", "ry", "nks", "nd", "sc", "ng", "sh", "nk", "sk", "nn", "ds", "sm", "sp", "ns", "nt",
            "dy", "ss", "st", "rrs", "xt", "nz", "sy", "xy", "rsch", "rphs", "sts", "sys", "sty", "th", "tl", "tls",
            "rds", "nch", "rns", "ts", "wls", "rnt", "tt", "rdy", "rst", "pps", "tz", "tch", "sks", "ppy", "ff", "sps",
            "kh", "sky", "ph", "lts", "wnst", "rth", "ths", "fs", "pp", "ft", "ks", "pr", "ps", "pt", "fy", "rts", "ky",
            "rshch", "mly", "py", "bb", "nds", "wry", "zz", "nns", "ld", "lf", "gh", "lks", "sly", "lk", "ll", "rph",
            "ln", "bs", "rsts", "gs", "ls", "vvy", "lt", "rks", "qs", "rps", "gy", "wns", "lz", "nth", "phs"
        };
        
        // Regex matching groups
        private static string PrefixGroup => $"(?:{string.Join("|", Prefixes)})";
        private static string Infix1Group => $"(?:{string.Join("|", Infixes1)})";
        private static string Infix2Group => $"(?:{string.Join("|", Infixes2)})";
        private static string Suffix1Group => $"(?:{string.Join("|", Suffixes1)})";
        private static string Suffix2Group => $"(?:{string.Join("|", Suffixes2)})";

        // Sector name regex
        private static string Class1ShortRegex => $"{PrefixGroup}{Infix1Group}{Suffix2Group}";
        private static string Class1LongRegex => $"{PrefixGroup}{Infix1Group}{Infix2Group}{Suffix1Group}";
        private static string Class2Regex => $"{PrefixGroup}{Suffix1Group} {PrefixGroup}{Suffix2Group}";
        
        // System identifier regex
        private static readonly string SystemIdRegex = "[A-Za-z][A-Za-z]-[A-Za-z] [A-Ha-h][0-9]+(?:-[0-9]+)?";

        // Make the set of regex to find entire system names
        public List<string> GenerateProcGenRegex()
        {
            return new()
            {
                $@"\b{Class1ShortRegex} {SystemIdRegex}\b",
                $@"\b{Class1LongRegex} {SystemIdRegex}\b",
                $@"\b{Class2Regex} {SystemIdRegex}\b",
            };
        }
    }
}