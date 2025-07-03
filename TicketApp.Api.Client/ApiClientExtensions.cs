using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using Refit;
using TicketApp.Api.Client.Config;
using TicketApp.Api.Client.Services;

namespace TicketApp.Api.Client;

public static class ApiClientExtensions
{
    public static IServiceCollection AddApiClient(this IServiceCollection services,
        IConfiguration configuration)
    {
        var configurationSection = configuration.GetSection(ApiClientOptions.DefaultSection);
        var userServiceClientOptions = configurationSection.Get<ApiClientOptions>();
        services.Configure<ApiClientOptions>(configurationSection);

        var pipeline = new ResiliencePipelineBuilder<HttpResponseMessage>()
            .AddRetry(new RetryStrategyOptions<HttpResponseMessage>
            {
                MaxRetryAttempts = userServiceClientOptions!.MaxRetryAttempts,
                BackoffType = DelayBackoffType.Exponential,
                Delay = TimeSpan.FromSeconds(userServiceClientOptions.RetryDelayInSeconds),
                ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
                    .Handle<ApplicationException>(),
                OnRetry = retryArguments =>
                {
                    return ValueTask.CompletedTask;
                }
            })
            .AddCircuitBreaker(new CircuitBreakerStrategyOptions<HttpResponseMessage>
            {
                ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
                    .Handle<ApplicationException>(),
                FailureRatio = userServiceClientOptions.FailureRatio,
                MinimumThroughput = userServiceClientOptions.MinimumThroughput,
                SamplingDuration = TimeSpan.FromSeconds(userServiceClientOptions.BreakDurationInSeconds),
                BreakDuration = TimeSpan.FromSeconds(userServiceClientOptions.BreakDurationInSeconds)
            })
            .Build();

        services.AddSingleton<TokenService>();
        services.AddTransient<AuthHeaderHandler>();

        var refitSettings = new RefitSettings
        {
            ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializerSettings())
        }; 
        
        services.AddRefitClient<IApiClient>(refitSettings).
            ConfigureHttpClient(c => c.BaseAddress = new Uri(userServiceClientOptions.BaseAddress))
            .AddHttpMessageHandler<AuthHeaderHandler>()
            .AddPolicyHandler(pipeline.AsAsyncPolicy());

        return services;
    }
}