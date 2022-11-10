﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Devsharp.Core.Extensions
{
    public static class PersianDateExtention
    {
        public static string ToPersian(this DateTime dateTime)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            try
            {
             return persianCalendar.GetYear(dateTime) + "/" + persianCalendar.GetMonth(dateTime) + "/" + persianCalendar.GetDayOfMonth(dateTime);

            }
            catch(Exception ex)
            {
                return "";
            }
        }

        public static DateTime PersianToDateTime(this string date)
        {
            if(date.Length!=10)
            {
                throw new ArgumentException(nameof(date));
            }
            PersianCalendar persian = new PersianCalendar();
            int year = Convert.ToInt32(date.Substring(0, 4));
            var convertDate = persian.ToDateTime(year, Convert.ToInt32(date.Substring(5, 2)), Convert.ToInt32(date.Substring(8, 2)), 0, 0, 0, 0); ;

            return convertDate;
        }
    }
}
