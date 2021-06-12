using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TrafficLightCentralSystem.Repositories;
using TrafficLightCentralSystem.Settings;
using MassTransit;
using Microsoft.Extensions.Configuration;
using TrafficLightCentralSystem.Usecases;

namespace TrafficLightCentralSystem
{
    public static class ServiceExtensions
    {
        public static void AddInjection(this IServiceCollection services, IConfiguration configuration)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSingleton<IEventRepository>(new InMemoryEventRepository());           

            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration["EventBusSettings:HostAddress"]);
                    //cfg.UseHealthCheck(ctx);
                });
            });
            services.AddMassTransitHostedService();
        }
    }
}
