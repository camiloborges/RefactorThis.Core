using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RefactorThis.Core.Application.Interfaces;
using RefactorThis.Core.Application.Services;
using RefactorThis.Core.Domain.CommandHandlers;
using RefactorThis.Core.Domain.Commands;
using RefactorThis.Core.Domain.Core.Bus;
using RefactorThis.Core.Domain.Core.Events;
using RefactorThis.Core.Domain.Core.Notifications;
using RefactorThis.Core.Domain.EventHandlers;
using RefactorThis.Core.Domain.Events;
using RefactorThis.Core.Domain.Interfaces;
using RefactorThis.Core.Infra.CrossCutting.Bus;

//using RefactorThis.Core.Infra.CrossCutting.Identity.Authorization;
using RefactorThis.Core.Infra.CrossCutting.Identity.Models;
using RefactorThis.Core.Infra.Data;
using RefactorThis.Core.Infra.Data.Context;
using RefactorThis.Core.Infra.Data.EventSourcing;
using RefactorThis.Core.Infra.Data.Repository;
using RefactorThis.Core.Infra.Data.Repository.EventSourcing;
using RefactorThis.Core.Models;

//using RefactorThis.Core.Infra.CrossCutting.Identity.Services;

namespace RefactorThis.Core.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // ASP.NET Authorization Polices
            //        services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();

            // Application
            services.AddScoped<IProductAppService, ProductAppService>();
            services.AddScoped<IProductsContext, ProductsContext>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<ProductCreatedEvent>, ProductEventHandler>();
            services.AddScoped<INotificationHandler<ProductUpdatedEvent>, ProductEventHandler>();
            services.AddScoped<INotificationHandler<ProductRemovedEvent>, ProductEventHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<CreateProductCommand, MediatR.Unit>, ProductCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateProductCommand, MediatR.Unit>, ProductCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveProductCommand, MediatR.Unit>, ProductCommandHandler>();
            services.AddScoped<IRequestHandler<CreateProductOptionCommand, MediatR.Unit>, ProductOptionCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateProductOptionCommand, MediatR.Unit>, ProductOptionCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveProductOptionCommand, MediatR.Unit>, ProductOptionCommandHandler>();

            // Infra - Data
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ProductsContext>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSQLContext>();

            // Infra - Identity Services
            //    services.AddTransient<IEmailSender, AuthEmailMessageSender>();
            //  services.AddTransient<ISmsSender, AuthSMSMessageSender>();

            // Infra - Identity
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}
