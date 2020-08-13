using System.Net.Mime;

using ConvertThis.Infrastructure;
using ConvertThis.Infrastructure.Services;
using ConvertThis.Infrastructure.Services.Converters;
using ConvertThis.WebApi.Infrastructure;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConvertThis.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                 .ConfigureApiBehaviorOptions(options =>
                 {
                     options.InvalidModelStateResponseFactory = context =>
                     {
                         var result = new BadRequestObjectResult(context.ModelState);
                         // TODO: add `using System.Net.Mime;` to resolve MediaTypeNames
                         result.ContentTypes.Add(MediaTypeNames.Application.Json);
                         result.ContentTypes.Add(MediaTypeNames.Application.Xml);

                         return result;
                     };
                 });

            services.AddScoped<IConverterFactory, ConverterFactory>();
            services.AddScoped<Base64Converter>().AddScoped<IConverter, Base64Converter>(s => s.GetService<Base64Converter>());
            services.AddScoped<Base32Converter>().AddScoped<IConverter, Base32Converter>(s => s.GetService<Base32Converter>());
            services.AddScoped<IInputToByteArrayConverter, InputToByteArrayConverter>();
            services.AddTransient<IConverterSelector, Base32ConverterSelector>();
            services.AddTransient<IConverterSelector, Base64ConverterSelector>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(policy =>
                policy.WithOrigins("http://localhost:44332", "https://localhost:44332")
                .AllowAnyMethod()
                .AllowAnyHeader());


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
