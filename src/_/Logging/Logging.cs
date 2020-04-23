using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;

namespace $safeprojectname$._.Logging
{
    public static class Logging
    {
        public static void Configure(WebHostBuilderContext hostContext, ILoggingBuilder configLogging)
        {
            configLogging.ClearProviders();
            configLogging.AddConfiguration(hostContext.Configuration.GetSection("Logging"));
            configLogging.AddSerilog(new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(hostContext.Configuration["ElasticSearch:Uri"]))
                {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                    IndexFormat = "log-"+ hostContext.Configuration["Logging:AppName"] + "-" + hostContext.HostingEnvironment.EnvironmentName + "-"+ hostContext.Configuration["Logging:HostName"] + "-{0:yyyy.MM.dd}",
                })
            .CreateLogger());
        }
    }
}
