using System;
using System.Collections.Generic;
using System.Text;

namespace CollectionsExamples
{
    public class UncasedStringComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return StringComparer.InvariantCultureIgnoreCase
                .Compare(x, y);
        }
    }
}
