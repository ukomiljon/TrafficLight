using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrafficLightCentralSystem.Repositories;
using TrafficLightCentralSystem.Settings;

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
            services.AddSingleton<IEventRepository>(new InMemoryEventRepository());
        }
    }
}
