using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Exapt;

[JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
public record SolutionData : IEquatable<SolutionData>
{
    public required string PuzzleId { get; set; }

    public bool Completed => Statistics is not null;

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public SolutionStatistics? Statistics { get; set; }
}

[JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
public record SolutionStatistics
{
    public required int Cycles { get; set; }

    public required int Size { get; set; }

    public required int Activity { get; set; }
}
