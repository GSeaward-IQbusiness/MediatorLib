using MediatR.Shared.Behaviours;
using MediatR.Shared.Validation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MediatR.Shared
{
    public static class IServiceCollectionExtentions
    {
        public static IServiceCollection AddInProcessCqrs(this IServiceCollection services, Assembly[] assemblies)
        {
            AddValidators(services, assemblies);
            AddPipelineBehaviours(services);
            services.AddMediatR(assemblies);

            return services;
        }

        private static void AddValidators(IServiceCollection services, Assembly[] assemblies)
        {
            services.Scan(scan => scan
                .FromAssemblies(assemblies).AddClasses(classes => classes
                    .AssignableTo<IValidationHandler>())
                .AsImplementedInterfaces()
                .WithTransientLifetime());
        }


        private static void AddPipelineBehaviours(IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LongTermCacheBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ShortTermCacheBahaviour<,>));
        }
    }
}
