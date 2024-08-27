using System.Text.Json.Serialization;

namespace Exapt;

public record SolutionData : IEquatable<SolutionData>
{
    [JsonPropertyName("puzzleId")]
    public required string PuzzleId { get; set; }

    [JsonPropertyName("completed")]
    public bool Completed => Statistics is not null;

    [JsonPropertyName("statistics")]
    public SolutionStatistics? Statistics { get; set; }
}

public record SolutionStatistics
{
    [JsonPropertyName("cycles")]
    public required int Cycles { get; set; }

    [JsonPropertyName("size")]
    public required int Size { get; set; }

    [JsonPropertyName("activity")]
    public required int Activity { get; set; }
}
