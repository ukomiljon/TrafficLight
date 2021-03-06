using MassTransit;
using MessageSenderHub;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SouthTrafficLight.EventBusConsumer;

namespace SouthTrafficLight
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // MassTransit-RabbitMQ Configuration
            services.AddMassTransit(config =>
            {
                config.AddConsumer<TrafficLightStateConsumer>();

                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(Configuration["EventBusSettings:HostAddress"]);

                    cfg.ReceiveEndpoint("South", c =>
                    {
                        c.ConfigureConsumer<TrafficLightStateConsumer>(ctx);
                    });
                });
            });
            services.AddMassTransitHostedService();
            services.AddScoped<TrafficLightStateConsumer>();

            services.AddSignalR();
            services.AddScoped<CentralHub>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SouthTrafficLight", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SouthTrafficLight v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<CentralHub>("/south");
            });
        }
    }
}
