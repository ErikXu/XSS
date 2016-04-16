﻿using System.Web.Mvc;
using XSS.Web.Common.Filters;

namespace XSS.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleAjaxErrorAttribute());
        }
    }
}
