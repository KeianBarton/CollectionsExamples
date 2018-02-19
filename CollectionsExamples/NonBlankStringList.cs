using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CollectionsExamples
{
    public class NonBlankStringList : Collection<string>
    {
        protected override void InsertItem(int index, string item)
        {
            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentException();
            base.InsertItem(index, item);
        }

        protected override void SetItem(int index, string item)
        {
            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentException();
            base.SetItem(index, item);
        }
    }
}
