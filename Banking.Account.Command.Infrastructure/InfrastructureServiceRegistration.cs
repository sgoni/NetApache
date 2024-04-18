using Banking.Account.Command.Application.Aggregates;
using Banking.Account.Command.Application.Contracts.Persistence;
using Banking.Account.Command.Infrastructure.KafkaEvents;
using Banking.Account.Command.Infrastructure.Repositories;
using Banking.CQRS.Core.Events;
using Banking.CQRS.Core.Handlers;
using Banking.CQRS.Core.Infrastructure;
using Banking.CQRS.Core.Producers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;

namespace Banking.Account.Command.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Registro de eventos como un tipo de dato dentro de MongoDB
            BsonClassMap.RegisterClassMap<BaseEvent>();
            BsonClassMap.RegisterClassMap<AccountOpenedEvent>();
            BsonClassMap.RegisterClassMap<AccountClosedEvent>();
            BsonClassMap.RegisterClassMap<FundsDepositedEvent>();
            BsonClassMap.RegisterClassMap<FundsWithdrawnEvent>();

            // Register repository services
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            services.AddScoped<EventProducer, AccountEventProducer>();
            services.AddTransient<IEventStoreRepository, EventStoreRepository>();
            services.AddTransient<EventStore, AccountEventStore>();
            services.AddTransient<EventSourcingHandler<AccountAggregate>, AccountEventSourcingHandler>();

            return services;
        }
    }
}
