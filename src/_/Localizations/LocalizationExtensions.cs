using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace $safeprojectname$._.Localizations
{
    public static class LocalizationExtensions
    {
        readonly static List<CultureInfo> _supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("en")
        };

        public static void AddCustomLocalization(this IServiceCollection services)
        {
            services.AddLocalization(o =>
            {
                // We will put our translations in a folder called Resources
                o.ResourcesPath = "Resources";
            });

            //Configure resources location
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            // Configure Asp.Net Core Localization extension
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = _supportedCultures;
                options.SupportedUICultures = _supportedCultures;
            });
        }

        public static void UseCustomLocalization(this IApplicationBuilder app)
        {
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en"),
                SupportedCultures = _supportedCultures,
                SupportedUICultures = _supportedCultures
            });
        }
    }
}
