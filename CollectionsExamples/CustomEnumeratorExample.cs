using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CollectionsExamples
{
    public class CustomEnumeratorExample : IEnumerable<string>
    {
        public IEnumerator<string> GetEnumerator()
        {
            Console.WriteLine("Before enumeration");
            // if(someCondition and we want to cancel iteration)
            // yield break;
            yield return "Monday";
            yield return "Tuesday";
            yield return "Wednesday";
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            // Obsolete for all intents and purposes
            return GetEnumerator();
        }
    }
}
