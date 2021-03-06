﻿using System.Web.Optimization;

namespace Foillan.WebService
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/nest/taxon").Include(
                      "~/Scripts/External/knockout-3.1.0.js",
                      "~/Scripts/Nest/taxon.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      "~/Scripts/External/jQuery/jquery-2.1.1.min.js",
                      "~/Scripts/External/jQuery/Plugins/jquery.velocity.min.js"));

            bundles.Add(new StyleBundle("~/Content/Common").Include(
                "~/Content/Stylesheets/Common.min.css"));
                
            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
