﻿using banking.Account.Query.Domain;
using Banking.Account.Query.Application.Contracts.Persistence;
using Banking.Account.Query.Application.Models;
using Banking.CQRS.Core.Events;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;

namespace Banking.Account.Query.Infrastructure.Consumer
{
    public class BankAccountConsumerService : IHostedService
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        public KafkaSettings _kafkaSettings { get; }

        public BankAccountConsumerService(IServiceScopeFactory factory)
        {
            _bankAccountRepository = factory.CreateScope().ServiceProvider.GetRequiredService<IBankAccountRepository>();
            _kafkaSettings = (factory.CreateScope().ServiceProvider.GetRequiredService<IOptions<KafkaSettings>>()).Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Kafka default configuration
            var config = new ConsumerConfig
            {
                GroupId = _kafkaSettings.GroupId,
                BootstrapServers = $"{_kafkaSettings.Hostname}:{_kafkaSettings.Port}",
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };

            try
            {
                using (var consumerBuilder = new ConsumerBuilder<Ignore, string>(config).Build())
                {
                    var bankTopics = new string[]
                    {
                        typeof(AccountOpenedEvent).Name,
                        typeof(AccountClosedEvent).Name,
                        typeof(FundsDepositedEvent).Name,
                        typeof(FundsWithdrawnEvent).Name,
                    };

                    consumerBuilder.Subscribe(bankTopics);
                    var cancelToken = new CancellationTokenSource();

                    try
                    {
                        while (true)
                        {
                            var consumer = consumerBuilder.Consume(cancelToken.Token);

                            // AccountOpenedEvent topic
                            if (consumer.Topic == typeof(AccountOpenedEvent).Name)
                            {
                                var accountOpenedEvent = JsonConvert.DeserializeObject<AccountOpenedEvent>
                                    (consumer.Message.Value);

                                var bankAccount = new BankAccount
                                {
                                    Identifier = accountOpenedEvent!.Id,
                                    AccountHolder = accountOpenedEvent!.AccountHolder,
                                    AccountType = accountOpenedEvent!.AccountType,
                                    Balance = accountOpenedEvent!.OpeningBalance,
                                    CreationDate = accountOpenedEvent!.CreatedDate,
                                };

                                _bankAccountRepository.AddAsync(bankAccount).Wait();
                            }

                            // AccountClosedEvent topic
                            if (consumer.Topic == typeof(AccountClosedEvent).Name)
                            {
                                var accountClosedEvent = JsonConvert.DeserializeObject<AccountClosedEvent>
                                    (consumer.Message.Value);
                                _bankAccountRepository.DeleteByIdentifier(accountClosedEvent!.Id).Wait();
                            }

                            // FundsDepositedEvent topic
                            if (consumer.Topic == typeof(FundsDepositedEvent).Name)
                            {
                                var accountDepositedEvent = JsonConvert.DeserializeObject<FundsDepositedEvent>
                                    (consumer.Message.Value);

                                var bankAccount = new BankAccount
                                {
                                    Identifier = accountDepositedEvent!.Id,
                                    Balance = accountDepositedEvent!.Amount,
                                };

                                _bankAccountRepository.DepositBankAccountByIdentifier(bankAccount).Wait();
                            }


                            // FundsDepositedEvent topic
                            if (consumer.Topic == typeof(FundsWithdrawnEvent).Name)
                            {
                                var accountWithdrawnEvent = JsonConvert.DeserializeObject<FundsWithdrawnEvent>
                                    (consumer.Message.Value);

                                var bankAccount = new BankAccount
                                {
                                    Identifier = accountWithdrawnEvent!.Id,
                                    Balance = accountWithdrawnEvent!.Amount,
                                };

                                _bankAccountRepository.WithdrawnBankAccountByIdentifier(bankAccount).Wait();
                            }
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        consumerBuilder.Close(); ;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
