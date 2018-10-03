using System;
using System.Linq;
using System.Reflection;

namespace RefactorThis.Core.Infra.CrossCutting.IoC
{
    public static class MediatorExtensions
    {
        public static IServiceCollection AddMediatorHandlers(this IServiceCollection services, Assembly assembly)
        {
            var classTypes = assembly.ExportedTypes.Select(t => t.GetTypeInfo()).Where(t => t.IsClass && !t.IsAbstract);

            foreach (var type in classTypes)
            {
                var interfaces = type.ImplementedInterfaces.Select(i => i.GetTypeInfo());

                foreach (var handlerType in interfaces.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
                {
                    bool containsGenericParameters = ContainsGenericParameters(handlerType.AsType().GenericTypeArguments, type);

                    if (containsGenericParameters)
                    {
                        foreach (Type n in Assembly.GetExecutingAssembly().GetTypes().Where(n => typeof(CommandHandler).IsAssignableFrom(n) && !n.IsInterface))
                        {
                            Type responseService = CloseTypeArgument(handlerType.GenericTypeArguments[1], n);
                            Type queryService = CloseTypeArgument(handlerType.GenericTypeArguments[0], n);
                            Type hendlerService = CloseTypeArgument(type, n);

                            Type typeIHandlerService = typeof(IRequestHandler<,>).MakeGenericType(queryService, responseService);
                            services.AddTransient(typeIHandlerService, hendlerService);
                        }
                    }
                    else
                    {
                        services.AddTransient(handlerType.AsType(), type.AsType());
                    }
                }
            }
            return services;
        }
        private static bool ContainsGenericParameters(Type[] genericTypeArguments, Type typeHandler = null)
        {
            bool argumentsGeneric = (genericTypeArguments.Where(x => x.ContainsGenericParameters).Count() == 0) ? false : true;

            return (typeHandler == null) ? argumentsGeneric : (argumentsGeneric) ? true : typeHandler.ContainsGenericParameters;
        }

        private static Type CloseTypeArgument(Type argument, Type closeType)
        {
            Type result = null;

            bool containsGenericParametersResponse = ContainsGenericParameters(argument.GenericTypeArguments);
            if (containsGenericParametersResponse && argument.GenericTypeArguments[0].GenericTypeArguments.Count() > 0)
            {
                Type innerGenericResponse = argument.GenericTypeArguments[0].GetGenericTypeDefinition().MakeGenericType(closeType);
                result = argument.GetGenericTypeDefinition().MakeGenericType(innerGenericResponse);
            }
            else
            {
                result = (argument.ContainsGenericParameters) ? argument.GetGenericTypeDefinition().MakeGenericType(closeType) : argument;
            }

            return result;
        }
    }
}