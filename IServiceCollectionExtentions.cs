using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Multigate.Titanic.MediatR.Shared.Behaviours;
using Multigate.Titanic.MediatR.Shared.Caching;
using Multigate.Titanic.MediatR.Shared.Validation;
using System.Reflection;

namespace Multigate.Titanic.MediatR.Shared
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
