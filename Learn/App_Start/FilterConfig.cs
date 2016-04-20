﻿using Learn.App_Start;
using Learn.Language;
using System.Web;
using System.Web.Mvc;

namespace Learn
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new GlobalizeFilterAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
