﻿using System.Web.Mvc;

namespace HemenBiletProje.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Flight", action = "FlightSearchPage", id = UrlParameter.Optional }
            );
        }
    }

}