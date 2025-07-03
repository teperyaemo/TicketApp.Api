namespace TicketApp.Api.Client.Config;

public class ApiClientOptions
{
    public const string DefaultSection = "ApiClientExtensions";
    public required string BaseAddress { get; set; }
    public int MaxRetryAttempts { get; set; }
    public int RetryDelayInSeconds { get; set; }
    public float FailureRatio { get; set; }
    public int MinimumThroughput { get; set; }
    public int SamplingDurationInSeconds { get; set; }
    public int BreakDurationInSeconds { get; set; }
}