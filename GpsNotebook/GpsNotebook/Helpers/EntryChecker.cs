using System;
using System.Collections.Generic;
using System.Text;

namespace GpsNotebook.Helpers
{
    public static class EntryChecker
    {
        public static bool EntryIsNullOrEmpty(params string[] entryInputs)
        {
            var countFilledEntrys = 0;

            foreach (var item in entryInputs)
            {
                if (!String.IsNullOrEmpty(item))
                {
                    countFilledEntrys++;
                }
            }

            return countFilledEntrys == entryInputs.Length;
        }
    }
}
