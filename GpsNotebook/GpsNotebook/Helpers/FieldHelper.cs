using System;
using System.Collections.Generic;
using System.Text;

namespace GpsNotebook.Helpers
{
    public static class FieldHelper
    {
        public static bool IsAllFieldsIsNullOrEmpty(params string[] entryInputs)
        {
            var countFilledEntrys = 0;

            foreach (var item in entryInputs)
            {
                if (!String.IsNullOrWhiteSpace(item))
                {
                    countFilledEntrys++;
                }
            }

            return countFilledEntrys == entryInputs.Length;
        }
    }
}
