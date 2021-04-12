﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GpsNotebook.Helpers
{
    //rename
    public static class FieldHelper
    {
        //rename with verb
        public static bool IsAllFieldsIsNullOrEmpty(params string[] entryInputs)
        {
            var countFilledEntrys = 0;

            foreach (var item in entryInputs)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    countFilledEntrys++;
                }
            }

            return countFilledEntrys == entryInputs.Length;
        }
    }
}
